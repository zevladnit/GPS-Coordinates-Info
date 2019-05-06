using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GPSCoordinateInfo.SQLiteModel
{
    public class ModelContext : DbContext
    {
        public DbSet<Coordinate> Coordinates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=blogging.db");
        }
       
    }

    public class Coordinate
    {
        [Key]
        public int IDCoordinate { get; set; }
        public float CoordinateX { get; set; }
        public float CoordinateY { get; set; }
        public string AddressContent { get; set; }

        public override string ToString()
        {
            return $"{IDCoordinate},{CoordinateX},{CoordinateY},{AddressContent}";
        }
    }
}
