using System;
using System.Collections.Generic;
using System.Windows;
using Init.Tools;

namespace Maps
{
    using Maps.Base;

    public class WebClient
    {
        static MapSettings Settings { get { return MapDesigner.MapSettings; } }

        /// <summary>
        /// Получить данные по географическому объекту по префиксу
        /// </summary>
        /// <param name="text"> Префикс с названием объекта </param>
        /// <returns> Ответ геокодера </returns>
        public static GeoResult GetGeoInfo(string text)
        {
            return YandexWeb.GetGeoInfo(text);
        }

        /// <summary>
        /// Получить координаты дома по улице и номеру дома
        /// </summary>
        /// <param name="street">Название улицы</param>
        /// <param name="house">Номер дома</param>
        /// <returns>Координаты дома</returns>
        public static Point? GetHouseCoordinaes(string street, string house)
        {
            try
            {
                switch (Settings.CalculateCoordinatesSource)
                {
                    case 1: return YandexWeb.GetHouseCoordinaes(street, house);
                    case 2: return OSMWeb.GetHouseCoordinaes(street, house);
                }
                return null;
            }
            catch (Exception ex)
            {
                var exception = new Exception(string.Format("Ошибка вычисления координат дома: {0}, на улице: {1}", house, street), ex);
                Log.AddException(exception);
                return null;
            }
        }

        /// <summary>
        /// Обертка для получения координат дома в виде объекта класса Coordinates 
        /// </summary>
        /// <param name="street">
        /// Название улицы
        /// </param>
        /// <param name="house">
        /// Номер дома
        /// </param>
        /// <returns>
        /// Координаты дома
        /// </returns>
        public static Coordinates GetHouseCoordinatesWrap(string street, string house)
        {
            var pointResult = GetHouseCoordinaes(street, house);
            if (pointResult == null)
                return null;

            var coordinates = new Coordinates { Longitude = pointResult.Value.X, Latitude = pointResult.Value.Y };
            return coordinates;
        }

        /// <summary>
        /// Получить координаты улицы по названию
        /// </summary>
        /// <param name="streetName">Название улицы</param>
        /// <returns>Координаты улицы</returns>
        public static Point? GetStreetCoordinates(string streetName)
        {
            if (string.IsNullOrWhiteSpace(streetName))
                throw new ArgumentException(string.Format("Название улицы не может быть пустым или null"), "streetName");

            Point? result = null;

            switch (Settings.CalculateCoordinatesSource)
            {
                case 1:
                    result = YandexWeb.GetStreetCoordinates(streetName);
                    break;
                case 2:
                    result = OSMWeb.GetStreetCoordinates(streetName);
                    break;
                default: throw new Exception(string.Format("Неизвестный тип сервиса вычисления координат (CalculateCoordinatesSource : {0}) ",
                                    Settings.CalculateCoordinatesSource));
            }
            return result;
        }

        /// <summary>
        /// Метод, получающий координаты по адресам начальной и конечной точки
        /// </summary>
        /// <param name="from">Начальная точка</param>
        /// <param name="to">Конечная точка</param>
        /// <returns>Координаты (TraceInfo)</returns>
        public static TraceInfo GetTraceInfo(string from, string to)
        {
            switch (Settings.CalculateDistanceSource)
            {
                case 1: return YandexWeb.GetTraceInfo(from, to);
                case 2: return OSMWeb.GetTraceInfo(from, to);
            }
            return null;
        }


        public static TraceInfo GetTraceInfo(Point from, Point to)
        {
            try
            {
                switch (Settings.CalculateDistanceSource)
                {
                    case 1: return YandexWeb.GetTraceInfo(from, to);
                    case 2: return OSMWeb.GetTraceInfo(from, to);
                    default: return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Получение списка адресов по запросу
        /// </summary>
        /// <param name="searchPrefix"> Строка запроса к web сервису </param>
        /// <returns> Список адресов </returns>
        public static List<string> GetAddresList(string searchPrefix)
        {
            if (searchPrefix == null)
                throw new ArgumentNullException("searchPrefix");

            try
            {
                // Рассчитываем только по Яндексу, потому что в OSM этот метод не реализован
                return YandexWeb.GetAddresList(searchPrefix);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ошибка получения списка адресов по префиксу: {0}", searchPrefix), ex);
            }
        }

        /// <summary>
        /// Получить координаты удаленной зоны по названию
        /// </summary>
        /// <param name="remoteZoneName">Название удаленной зоны</param>
        /// <returns>Координаты удаленной зоны</returns>
        public static Point? GetRemoteZoneCoordinates(string remoteZoneName)
        {
            if (string.IsNullOrWhiteSpace(remoteZoneName))
                throw new ArgumentException(string.Format("Название удаленной зоны не может быть пустым или null"), "remoteZoneName");

            Point? result = YandexWeb.GetRemoteZoneCoordinates(remoteZoneName);
            return result;
        }
    }
}
