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
              /*return _context.Move != null ? 
                          View(await _context.Move.ToListAsync()) :
                          Problem("Entity set 'MvCBornToMoveContext.Move' is null.");*/
        }

        public async Task<List<MoveAverageRating>?> ReadAllMovesWithAverageRatingsAsync()
        {
            try
            {
                var movesWithAverageRatings = await _context.Move
                .Select(m => new MoveAverageRating()
                 {
                     Move = m,
                     AverageRating = m.Ratings.Select(mr => (double?)mr.Rating).Average() ?? 0.0
                 })
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

            var moveWithAverages = await ReadMoveWithAveragesAsync(id);

            if (moveWithAverages == null)
            {
                return NotFound();
            }

            return View(moveWithAverages);
        }

        public async Task<MoveAverageRating?> ReadMoveWithAveragesAsync(int? id)
        {
            try
            {
                var moveWithAverages = await _context.Move
                    .Where(m => m.Id == id)
                    .Select(m => new MoveAverageRating()
                    {
                        Move = m,
                        AverageRating = Math.Round(m.Ratings.Select(mr => (double?)mr.Rating).Average() ?? 0.0, 1),
                        AverageIntensity = Math.Round(m.Ratings.Select(mr => (double?)mr.Intensity).Average() ?? 0.0, 1)
                    })
                    .FirstOrDefaultAsync();

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
        public async Task<IActionResult> Details(int moveId, double reviewRating, double reviewIntensity)
        {//add modelstate is valid for range and stuff to work
        
                MoveRating moveRating = new MoveRating
                {
                    Move = _context.Move.Find(moveId),
                    Rating = reviewRating,
                    Intensity = reviewIntensity
                };

                _context.MoveRating.Add(moveRating);
                await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
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
