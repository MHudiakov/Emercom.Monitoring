using System;
using System.Linq;

namespace GpsTest
{
    using System.Threading;

    using Init.Tools.GPS;

    using ReaderUtility.Client;
    using ReaderUtility.Dal;
    using ReaderUtility.Dal.ServerService;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Стартуем...");
                var context = new WcfServerContext(Settings.Default.ServiceOperationAddress);
                var dataManager = new WcfDataManager(context);
                DalContainer.RegisterWcfDataMager(dataManager);


                var store = DalContainer.WcfDataManager.ServiceOperationClient.GetAllStore().First();


                var testDist = 1000 * GlobePointTools.GetDistanceBetweenPoints(
                    store.Longitude,
                    store.Latitude,
                    32.06847,
                    54.78227);

                var startLongitude = store.Longitude;
                var delta = 0.0005;
                var unitId = 3;

                // Право
                var point = new GeoPoints();
                for (int i = 0; i < 300; i++)
                {

                    point.Latitude = store.Latitude;
                    point.Longitude = store.Longitude + delta;
                    point.UnitId = unitId;
                    point.Time = DateTime.Now;
                    delta += 0.0005;
                    DalContainer.WcfDataManager.ServiceOperationClient.AddGeoPoint(point);
                    Thread.Sleep(1000);
                }

                store.Latitude = point.Latitude;
                store.Longitude = point.Longitude;


                // Верх
                delta = 0.0005;
                for (int i = 0; i < 30; i++)
                {
                    point.Latitude = store.Latitude + delta;
                    point.Longitude = store.Longitude;
                    point.UnitId = unitId;
                    point.Time = DateTime.Now;
                    delta += 0.0005;
                    DalContainer.WcfDataManager.ServiceOperationClient.AddGeoPoint(point);
                    Thread.Sleep(1000);
                }

                store.Latitude = point.Latitude;
                store.Longitude = point.Longitude;

                // Влево
                delta = 0.0005;
                for (int i = 0; i < 300; i++)
                {
                    point.Latitude = store.Latitude;
                    point.Longitude = store.Longitude - delta;
                    point.UnitId = unitId; 
                    point.Time = DateTime.Now;
                    delta += 0.0005;
                    DalContainer.WcfDataManager.ServiceOperationClient.AddGeoPoint(point);
                    Thread.Sleep(1000);
                }

                store.Latitude = point.Latitude;
                store.Longitude = point.Longitude;

                // Вниз
                delta = 0.0005;
                for (int i = 0; i < 30; i++)
                {
                    point.Latitude = store.Latitude - delta;
                    point.Longitude = store.Longitude;
                    point.UnitId = unitId;
                    point.Time = DateTime.Now;
                    delta += 0.0005;
                    DalContainer.WcfDataManager.ServiceOperationClient.AddGeoPoint(point);
                    Thread.Sleep(1000);
                }

                Console.WriteLine(testDist);
                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при подключении к серверу" + ex.Message);
            }
        }
    }
}