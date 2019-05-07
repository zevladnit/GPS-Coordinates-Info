namespace GPSCIService.Services
{
    using Models;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MoreLinq;
    using YandexAPI.Maps;
    public class fonService : BackgroundService
    {
        public fonService(ModelContext db) => Db = db;

        private static ModelContext Db { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"Service started");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(2000, stoppingToken);
                var result = Db.Coordinates.Where(x => x.status == false);
                if (!result.Any())
                    continue;
                try
                {
                    AddDBInfo(result);
                    JasonToZip();
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                };
            }
        }

        public static void JasonToZip()
        {
            var json = JsonConvert.SerializeObject(Db.TwoCoordinates
                .Select(x => new { x.distance, x.OnePos, x.TwoPos }).ToList());

            if (File.Exists(@"./Json/json.json"))
                File.Delete(@"./Json/json.json");
            if (Directory.Exists("Json"))
                Directory.Delete("Json");

            Directory.CreateDirectory("Json");
            File.WriteAllText(@"./Json/json.json", json);
            ZipFile.ExtractToDirectory("json.zip", "./Json");
        }

        public static void AddDBInfo(IQueryable<Coordinate> query)
        {
            var list = new List<TwoCoordinate>();
            foreach (var e in query.Batch(2))
            {
                var pos1 = e.First();
                var pos2 = e.Last();

                void parse(Coordinate c1)
                {
                    var geoCode = new GeoCode();
                    var infoXML = geoCode.SearchObject(c1.x, c1.y);
                    var Adress = geoCode.GetAddress(infoXML);
                    c1.AddressCounry = Adress.Split(",").First();
                    c1.AddressCity = Adress.Split(",").ToList()[1];
                    c1.AddressRegiin = Adress.Split(",").Last();
                    c1.status = true;
                    Db.SaveChanges();
                }
                parse(pos1);
                parse(pos2);
                var distance = GetDistance(pos1.y,pos1.x, pos2.y, pos2.x);
                list.Add(new TwoCoordinate { OneID = pos1.IDCoordinate, distance = distance, TwoID = pos2.IDCoordinate });


            }
            Db.TwoCoordinates.AddRange(list);
            Db.SaveChanges();
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
