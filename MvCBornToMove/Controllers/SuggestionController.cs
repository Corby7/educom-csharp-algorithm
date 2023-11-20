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
    public class SuggestionController : Controller
    {
        private readonly MvCBornToMoveContext _context;

        public SuggestionController(MvCBornToMoveContext context)
        {
            _context = context;
        }

        // GET: Suggestion
        public async Task<IActionResult> Index()
        {
            var randomMoveId = await GenerateRandomSuggestionAsync();

            if (randomMoveId.HasValue)
            {
                return RedirectToAction("Details", "Moves", new { id = randomMoveId.Value });
            }
            else
            {
                return NotFound();
            }
        }


        public async Task<int?>GenerateRandomSuggestionAsync()
        {
            var moveIds = await _context.Move
                .Select(move => move.Id)
                .ToListAsync();

            if (moveIds.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(moveIds.Count);
                int randomId = moveIds[index];

                return randomId;
            }
            else
            {
                return null;
            }

        }

    }
}
