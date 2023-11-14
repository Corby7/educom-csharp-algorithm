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

        public DbSet<MvCBornToMove.Models.Move> Move { get; set; } = default!;
    }
}
