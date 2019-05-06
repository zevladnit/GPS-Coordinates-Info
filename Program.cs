namespace GPSCoordinateInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using YandexAPI.Maps;
    using SQLiteModel;

    class Program
    {
        static void Main(string[] args)
        {
            ModelContext db = new ModelContext();
            db.Database.EnsureCreated();
            db.Coordinates.AddRange(new List<Coordinate>
            {
                new Coordinate{ CoordinateX = 30.315868f, CoordinateY= 59.939095f },
                new Coordinate{ CoordinateX = 37.617635f, CoordinateY = 55.755814f }
            });
            db.SaveChanges();
            var kek = db.Coordinates.ToList().Select(z => z.ToString());
            Console.WriteLine(string.Join('\n', kek));

            GeoCode geoCode = new GeoCode();
            string infoXML = geoCode.SearchObject(30.315868, 59.939095);
            string Adress = geoCode.GetAddress(infoXML);
            Console.WriteLine(Adress);
            Console.ReadKey();
        }
    }
}
