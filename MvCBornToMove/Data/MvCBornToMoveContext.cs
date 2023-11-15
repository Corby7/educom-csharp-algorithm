using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvCBornToMove.Models;

namespace MvCBornToMove.Data
{
    public class MvCBornToMoveContext : DbContext
    {
        public MvCBornToMoveContext (DbContextOptions<MvCBornToMoveContext> options)
            : base(options)
        {
        }

        public DbSet<Move> Move { get; set; } = default!;
        public DbSet<MoveRating> MoveRating { get; set; } = default!;
    }
}
