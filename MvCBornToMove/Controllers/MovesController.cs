using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvCBornToMove.Data;
using MvCBornToMove.Models;

namespace MvCBornToMove.Controllers
{
    public class MovesController : Controller
    {
        private readonly MvCBornToMoveContext _context;

        public MovesController(MvCBornToMoveContext context)
        {
            _context = context;
        }

        // GET: Moves
        public async Task<IActionResult> Index()
        {
            try
            {
                var movesWithAverageRatings = await ReadAllMovesWithAverageRatingsAsync();

                if (movesWithAverageRatings != null)
                {
                    return View(movesWithAverageRatings);
                }
                else
                {
                    return Problem("Error while reading all moves.");
                }
            }
            catch (Exception ex)
            {
                return Problem("Error while processig request: " + ex.Message);
            }
        }

        public async Task<List<Move>?> ReadAllMovesWithAverageRatingsAsync()
        {
            try
            {
                var movesWithAverageRatings = await _context.Move
                .Include(m => m.AverageRatings)
                .ToListAsync();

                return movesWithAverageRatings;
            }
            catch (Exception ex)
            {
                Problem("Error while reading all moves: " + ex.Message);
                return null;
            }
        }

        // GET: Moves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Move == null)
            {
                return NotFound();
            }

            var move = await ReadMoveWithAveragesAsync(id);

            if (move == null)
            {
                return NotFound();
            }

            return View(move);
        }

            public async Task<MoveRating> ReadMoveWithAveragesAsync(int? id)
            {
                try
                {
                    var moveWithAverages = await _context.MoveRating
                        .Where(m => m.Move.Id == id)
                        .Include(mr => mr.Move)
                            .ThenInclude(move => move.AverageRatings)
                        .FirstOrDefaultAsync();

                    if (moveWithAverages == null)
                    {
                        var moveWithoutAverages = await _context.Move.FindAsync(id);
                        return new MoveRating
                        {
                            Move = moveWithoutAverages,
                            Rating = 0,
                            Intensity = 0
                        };
                    }

                    return moveWithAverages;
                }
                catch (Exception ex)
                {
                    Problem("Error while reading all moves: " + ex.Message);
                    return null;
                }
            }

        // POST: Moves/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details([Bind("Move, Rating, Intensity")] MoveRating moveRating)
        {//add modelstate is valid for range and stuff to work

            ModelState.Remove("Move.AverageRatings.AverageRating");
            ModelState.Remove("Move.AverageRatings.AverageIntensity");

            if (ModelState.IsValid)
            {
                Move move = await _context.Move.FindAsync(moveRating.Move.Id); //kan move van moveaveragerating dus niet gebruiken om dat efcore die niet kent?

                if (move !=null)
                {
                    MoveRating newMoveRating = new MoveRating
                    {
                        Move = move,
                        Rating = moveRating.Rating,
                        Intensity = moveRating.Intensity
                    };

                    _context.MoveRating.Add(newMoveRating);
                    await _context.SaveChangesAsync();

                    var moveRatingWithAverages = await _context.MoveRating
                        .Where(mr => mr.Move.Id == move.Id)
                        .Include(mr => mr.Move)
                            .ThenInclude(moveRating => moveRating.AverageRatings)
                        .FirstOrDefaultAsync();

                    return View(moveRatingWithAverages);
                }
                else
                {
                    return NotFound();
                }

            }

            return View(moveRating);
        }

        // GET: Moves/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Moves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,SweatRate")] Move move)
        {
            if (ModelState.IsValid)
            {
                _context.Add(move);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(move);
        }

        // GET: Moves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Move == null)
            {
                return NotFound();
            }

            var move = await _context.Move.FindAsync(id);
            if (move == null)
            {
                return NotFound();
            }
            return View(move);
        }

        // POST: Moves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,SweatRate")] Move move)
        {
            if (id != move.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(move);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoveExists(move.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(move);
        }

        // GET: Moves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Move == null)
            {
                return NotFound();
            }

            var move = await _context.Move
                .FirstOrDefaultAsync(m => m.Id == id);
            if (move == null)
            {
                return NotFound();
            }

            return View(move);
        }

        // POST: Moves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Move == null)
            {
                return Problem("Entity set 'MvCBornToMoveContext.Move'  is null.");
            }
            var move = await _context.Move.FindAsync(id);
            if (move != null)
            {
                _context.Move.Remove(move);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoveExists(int id)
        {
          return (_context.Move?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
