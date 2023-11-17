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

        public DbSet<MoveAverageRating> MoveAverageRating { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoveAverageRating>()
                .HasKey(mar => mar.Id); // Assuming Id is the primary key property of MoveAverageRating

            modelBuilder.Entity<MoveAverageRating>()
                .HasOne(mar => mar.Move)
                .WithOne(move => move.AverageRatings)
                .HasForeignKey<MoveAverageRating>(mar => mar.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
