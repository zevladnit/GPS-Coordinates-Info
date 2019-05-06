using System;
using YandexAPI.Maps;
namespace GPSCoordinateInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            GeoCode geoCode = new GeoCode();
            string infoXML = geoCode.SearchObject(30.315868, 59.939095);
            string Adress = geoCode.GetAddress(infoXML);
            Console.WriteLine(Adress);
            Console.ReadKey();
        }
    }
}
