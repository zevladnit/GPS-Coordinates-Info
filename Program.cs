using YandexAPI;

namespace GPSCoordinateInfo
{
    using System;
    using System.Linq;
    using YandexAPI.Maps;
    using SQLiteModel;

    class Program
    {

        static void Main(string[] args)
        {
            AddDBcolumn();
            AddDistance();
            Console.ReadKey();
        }

        public static void AddDBcolumn()
        {
            int i = 1;
            ModelContext db = new ModelContext();
            db.Database.EnsureCreated();

            foreach (var e in db.Coordinates)
            {
                GeoCode geoCode = new GeoCode();
                string infoXML = geoCode.SearchObject(e.x, e.y);
                string Adress = geoCode.GetAddress(infoXML);
                e.AddressCounry = Adress.Split(",").First();
                e.AddressCity = Adress.Split(",").ToList()[1];
                e.AddressRegiin = Adress.Split(",").Last();
                Console.WriteLine(Adress);
                if (i % 2 == 0)
                    db.TwoCoordinates.AddRange(new TwoCoordinate {OneID = i - 1, TwoID = i});
                i++;
            }
            db.SaveChanges();
        }

        public static void AddDistance()
        {
            ModelContext db = new ModelContext();
            db.Database.EnsureCreated();
            foreach (var e in db.TwoCoordinates)
            {
                e.distance = GetDistance(e.OnePos.y,e.OnePos.x,e.TwoPos.y,e.TwoPos.x);
            }

            db.SaveChanges();
        }

        public static float GetDistance(double l1, double lo1, double l2, double lo2)
        {
            // радиус сферы (Земли)
            var rad = 6372795;

                        // координаты двух точек
            var llat1 = l1;
            var llong1 = lo1;
            var llat2 = l2;
            var llong2 = lo2;
            // в радианах
            var lat1 = llat1 * Math.PI / 180;
            var lat2 = llat2 * Math.PI / 180;
            var long1 = llong1 * Math.PI / 180;
            var long2 = llong2 * Math.PI / 180;

            // косинусы и синусы широт и разницы долгот
            var cl1 = Math.Cos(lat1);
            var cl2 = Math.Cos(lat2);
            var sl1 = Math.Sin(lat1);
            var sl2 = Math.Sin(lat2);
            var delta = long2 - long1;
            var cdelta = Math.Cos(delta);
            var sdelta = Math.Sin(delta);

            // вычисления длины большого круга
            var y = Math.Sqrt(Math.Pow(cl2 * sdelta, y: 2) + Math.Pow(cl1 * sl2 - sl1 * cl2 * cdelta, y: 2));
            var x = sl1 * sl2 + cl1 * cl2 * cdelta;
            var ad = Math.Atan2(y, x);
            var dist = ad * rad;
            return (float)dist; // в метрах
        }
    }
}
