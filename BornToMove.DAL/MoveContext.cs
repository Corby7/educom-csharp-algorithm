using Microsoft.EntityFrameworkCore;

namespace BornToMove.DAL
{
    public class MoveContext : DbContext
    {
        public DbSet<Move> Move { get; set; }
        public DbSet<MoveRating> MoveRating { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=born2move;Trusted_Connection=true;TrustServerCertificate=true;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
