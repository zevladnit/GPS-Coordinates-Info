using Microsoft.EntityFrameworkCore;

namespace GPSCIService.Models
{
    public class ModelContext : DbContext
    {
        public DbSet<Coordinate> Coordinates { get; set; }
        public DbSet<TwoCoordinate> TwoCoordinates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("coordinate.db");
        }
        public ModelContext()
        {
        }
    }
}
