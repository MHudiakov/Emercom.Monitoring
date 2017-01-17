using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Net;
using System.Web;
using System.IO;
using Init.Tools;

namespace Maps
{
    using Maps.Base;

    public static class OSMWeb
    {
        static MapSettings Settings { get { return MapDesigner.MapSettings; } }

        /// <summary>
        /// Получить координаты улицы по названию.
        /// Метод не реализован! При вызове будет сгенерирован NotImplementedException()!
        /// </summary>
        /// <param name="streetName">Название улицы</param>
        /// <returns>Координаты улицы</returns>
        public static Point? GetStreetCoordinates(string streetName)
        {
            return GetHouseCoordinaes(streetName, string.Empty);
        }

        /// <summary>
        /// Получить координаты дома
        /// </summary>
        /// <param name="street">Название улицы на которой расположен дом</param>
        /// <param name="house">Номер здания</param>
        /// <returns>Координаты дома</returns>
        public static Point? GetHouseCoordinaes(string street, string house)
        {
            try
            {

                var address = Settings.RusCityName + "+" + street + " " + house;

                var text = System.Web.HttpUtility.UrlEncode(address);
                var url = "http://openstreetmap.ru/api/search?q=" + text +
                    "&accuracy=1&lat=" + Settings.CityCenterLat.ToString("0.00000000").Replace(',', '.') + "&lon=" + Settings.CityCenterLon.ToString("0.00000000").Replace(',', '.');

                var guid = Guid.NewGuid();
                var startTime = DateTime.Now;
                Log.Add("HttpRequest", string.Format("OSM GetHouseCoordinaes. street: {1}; house: {2}; url: {0}; guid: {3}", url, street, house, guid));

                var wr = WebRequest.Create(url);
                var response = wr.GetResponse();

                StreamReader objReader = new StreamReader(response.GetResponseStream());
                var str = objReader.ReadToEnd();

                var endTime = DateTime.Now;
                var delta = (endTime - startTime).TotalSeconds;
                Log.Add("HttpRequest", string.Format("OSM GetHouseCoordinaes response guid: {0}, deltaTime: {1}", guid, delta));

                var ind = str.IndexOf("lat");
                if (ind < 0)
                    return null;
                var str2 = str.Remove(0, ind + 6);
                ind = str2.IndexOf(",");
                str2 = str2.Remove(ind);
                var lat = str2.ToDouble();

                ind = str.IndexOf("lon");
                if (ind < 0)
                    return null;
                str2 = str.Remove(0, ind + 6);
                ind = str2.IndexOf(",");
                str2 = str2.Remove(ind);
                var lon = str2.ToDouble();

                return new Point(lon, lat);
            }
            catch (Exception ex)
            {
                Log.AddException("HttpRequest", ex);
                return null;
            }
        }

        public static TraceInfo GetTraceInfo(Point from, Point to)
        {
            TraceInfo result = new TraceInfo();

            var url = "http://www.yournavigation.org/api/dev/route.php?flat={0}&flon={1}&tlat={2}&tlon={3}&v=motorcar&fast=0&layer=mapnik&instructions=1";
            url = string.Format(url, from.Y.ToString().Replace(',', '.'), from.X.ToString().Replace(',', '.'),
                to.Y.ToString().Replace(',', '.'), to.X.ToString().Replace(',', '.'));


            var guid = Guid.NewGuid();
            var startTime = DateTime.Now;
            Log.Add("HttpRequest", string.Format("OSM GetTraceInfo request guid: {1} : {0}", url, guid));

            var wr = WebRequest.Create(url);
            var response = wr.GetResponse();

            var objReader = new StreamReader(response.GetResponseStream());
            var str = objReader.ReadToEnd();

            var endTime = DateTime.Now;
            var delta = (endTime - startTime).TotalSeconds;
            Log.Add("HttpRequest", string.Format("OSM GetTraceInfo response guid: {0}, deltaTime: {1}", guid, delta));

            result.Length = GetLen(str)*1000;

            result.FromPoint = from;
            result.ToPoint = to;
            result.Points = GetPoints(str);

            if (result.FromPoint.HasValue)
                result.Points.Insert(0, result.FromPoint.Value);

            if (result.ToPoint.HasValue)
                result.Points.Add(result.ToPoint.Value);

            return result;
        }

        private static Point? GetFromPoint(string str)
        {
            var ind = str.IndexOf("coordinates");
            str = str.Remove(0, ind + 14);
            ind = str.IndexOf("]");
            if (ind > 0)
            {
                str = str.Remove(ind);
                var c = str.Split(',');
                return new Point(c[0].ToDouble(0), c[1].ToDouble(0));
            }
            return null;
        }

        private static Point? GetToPoint(string str)
        {
            var ind = str.IndexOf("coordinates");
            str = str.Remove(0, ind + 14);

            ind = str.IndexOf("coordinates");
            str = str.Remove(0, ind + 14);

            ind = str.IndexOf("]");
            if (ind > 0)
            {
                str = str.Remove(ind);
                var c = str.Split(',');
                return new Point(c[0].ToDouble(0), c[1].ToDouble(0));
            }
            return null;
        }

        private static List<Point> GetPoints(string str)
        {
            List<Point> Points = new List<Point>();
            var ind = str.IndexOf("<coordinates>");
            if (ind < 0)
                return Points;

            str = str.Remove(0, ind + 14);

            ind = str.IndexOf("<");
            if (ind > 0)
            {
                str = "\n" + str.Remove(ind);
                ind = str.IndexOf("\n");
                while (true)
                {
                    bool isLast = false;
                    str = str.Remove(0, ind + 1);
                    ind = str.IndexOf("\n");
                    if (ind <= 0)
                    {
                        isLast = true;
                        ind = str.IndexOf(" ");
                        if (ind <= 0)
                            break;
                    }
                    var strPoint = str.Remove(ind);
                    var c = strPoint.Split(',');
                    var point = new Point(c[0].ToDouble(0), c[1].ToDouble(0));
                    Points.Add(point);
                    if (isLast)
                        break;
                }
            }
            return Points;
        }

        private static double GetLen(string str)
        {
            double len = 0;
            var resStr = str;
            var ind = resStr.IndexOf("<distance>");
            if (ind > 0)
            {
                resStr = resStr.Remove(0, ind + 10);
                ind = resStr.IndexOf("<");
                if (ind > 0)
                {
                    resStr = resStr.Remove(ind - 1);
                    len = resStr.Replace('.', ',').ToDouble(0);
                }
            }
            return len;
        }

        public static TraceInfo GetTraceInfo(string from, string to)
        {
            var p_from = GetHouseCoordinaes("", from);
            var p_to = GetHouseCoordinaes("", to);
            if (p_from.HasValue && p_to.HasValue)
                return GetTraceInfo(p_from.Value, p_to.Value);
            else
                return null;
        }

        /// <summary>
        /// Получение списка адресов по запросу
        /// </summary>
        /// <param name="searchPrefix"> Строка запроса для web сервиса </param>
        /// <returns> Список адресов </returns>
        public static List<string> GetAddresList(string searchPrefix)
        {
            throw new NotImplementedException();
        }
    }
}
