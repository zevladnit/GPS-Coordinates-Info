using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;
using System.Linq;

namespace GPSCoordinateInfo.SQLiteModel
{
    public class ModelContext : DbContext
    {
        public DbSet<Coordinate> Coordinates { get; set; }
        public DbSet<TwoCoordinate> TwoCoordinates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=blogging.db");
        }
    }

    public class Coordinate : Point
    {
        [Key]
        public int IDCoordinate { get; set; }
        public string AddressCounry { get; set; }
        public string AddressCity { get; set; }
        public string AddressRegiin { get; set; }

        public override string ToString()
        {
            return $"{IDCoordinate},{x},{y},{AddressCounry},{AddressCity},{AddressRegiin}";
        }
    }

    public abstract class Point
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    public class TwoCoordinate
    {
        [Key]
        public int Id { get; set; }
        public int OneID { get; set; }
        public int TwoID { get; set; }
        public float distance { get; set; }

        [NotMapped]
        public Coordinate OnePos => new ModelContext().Coordinates.FirstOrDefault(x => x.IDCoordinate == OneID);
        [NotMapped]
        public Coordinate TwoPos => new ModelContext().Coordinates.FirstOrDefault(x => x.IDCoordinate == TwoID);
    }
}
