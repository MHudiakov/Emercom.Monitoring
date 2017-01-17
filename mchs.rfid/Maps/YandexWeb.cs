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
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    using Maps.Base;
    using DAL.WCF.Enums;

    public static class YandexWeb
    {

        static MapSettings Settings { get { return MapDesigner.MapSettings; } }

        public static GeoResult GetGeoInfo(string text, Point? center = null)
        {
            var res = new GeoResult();
            try
            {
                res.StartTime = DateTime.Now;
                var requestString = HttpUtility.UrlEncode(text);

                // Для получения координаты необходимо использовать метод, который отдает координаты после непосредственно запроса в яндекс 
                var url = "http://geocode-maps.yandex.ru/1.x/?&geocode=" + requestString;

                var guid = Guid.NewGuid();
                var startTime = DateTime.Now;
                Log.Add("HttpRequest", string.Format("Yandex GetGeoInfo. url: {0}; text: {2}; guid: {1}", url, guid, text));


                var wr = WebRequest.Create(url);
                var response = wr.GetResponse();

                var objReader = new StreamReader(response.GetResponseStream());
                var str = objReader.ReadToEnd();

                var endTime = DateTime.Now;
                var delta = (endTime - startTime).TotalSeconds;
                Log.Add("HttpRequest", string.Format("Yandex GetGeoInfo response guid: {0}, deltaTime: {1}", guid, delta));

                var xml = XDocument.Parse(str);

                var collection = xml.Root.GetElement("GeoObjectCollection");

                var meta = collection.GetElement("metaDataProperty").GetElement("GeocoderResponseMetaData");

                res.Request = GetPropValue(meta, "request");

                res.Suggest = GetPropValue(meta, "suggest");
                if (string.IsNullOrEmpty(res.Suggest))
                    res.Suggest = res.Request;

                res.List = new List<GeoObject>();
                foreach (var element in collection.Elements())
                {
                    if (element.Name.LocalName.ToLower() != "featureMember".ToLower())
                        continue;

                    var geoObject = new GeoObject();
                    try
                    {
                        var geo = element.GetElement("GeoObject");
                        if (geo == null)
                            continue;

                        ParseGeoObject(geo, geoObject);

                        res.List.Add(geoObject);
                    }
                    catch (Exception ex)
                    {
                        Log.AddException(ex);
                    }
                }
                res.IsSuccess = true;
                res.EndTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.ErrorInfo = "Произошла ошибка получения запроса: " + ex.Message;
                res.Error = ex;
            }
            return res;
        }

        private static XElement GetElement(this XElement parent, string findElementName)
        {
            foreach (var element in parent.Elements())
            {
                if (element.Name.LocalName.ToLower() == findElementName.ToLower())
                {
                    return element;
                }
            }
            return null;
        }

        private static string GetPropValue(XElement meta, string propName)
        {
            var e = meta.GetElement(propName);
            if (e == null)
                return null;

            return e.Value;
        }

        /// <summary>
        /// Разобрать XML документ и записать данные в объект geoObject
        /// </summary>
        /// <param name="geo"> XML документ  </param>
        /// <param name="geoObject"> Объект geoObject для заполнения  </param>
        private static void ParseGeoObject(XElement geo, GeoObject geoObject)
        {
            var pos = geo.GetElement("Point").GetElement("pos").Value.Split(' ');

            // долгота
            geoObject.Lon = pos[0].ToDouble();

            // широта
            geoObject.Lat = pos[1].ToDouble();

            var metaGeo = geo.GetElement("metaDataProperty").GetElement("GeocoderMetaData");

            // тип объекта
            var kind = GetPropValue(metaGeo, "kind");
            switch (kind)
            {
                case "store": 
                    geoObject.Kind = UnitTypeEnum.store;
                    break;
                case "unit":
                    geoObject.Kind = UnitTypeEnum.unit;
                    break;
                default:
                    geoObject.Kind = UnitTypeEnum.other;
                    break;
            }

            var name = GetPropValue(metaGeo, "text");
            
            // Точновть определения объекта
            var precision = GetPropValue(metaGeo, "precision");

            geoObject.Name = name;
            geoObject.PrecisionStr = precision;
        }

        /// <summary>
        /// Получить координаты улицы по названию
        /// </summary>
        /// <param name="streetName">Название улицы</param>
        /// <returns>Координаты улицы</returns>
        public static Point? GetStreetCoordinates(string streetName)
        {
            try
            {
                var totalStreetName = Settings.RusCityName + "," + streetName;
                var result = GetTraceInfo(totalStreetName, totalStreetName);
                return result.FromPoint;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ошибка получени координат улицы по названию : {0}", streetName), ex);
            }
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
                var city = Settings.RusCityName;
                var address = city + ", " + street + " " + house;
                return GetTraceInfo(address, address).FromPoint;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Метод для получения координаты (начала и конца) для улиц и домов определенного города 
        /// </summary>
        /// <param name="from">Начало</param>
        /// <param name="to">Конец</param>
        /// <returns>Координаты начала адреса и его конца</returns>
        public static TraceInfo GetTraceInfo(string from, string to)
        {
            if (from.IndexOf(Settings.RusCityName + ", ") < 0)
                from = Settings.RusCityName + ", " + from;
            if (to.IndexOf(Settings.RusCityName + ", ") < 0)
                to = Settings.RusCityName + ", " + to;

            var result = GetTraceInfoDirectly(from, to);
            return result;
        }

        /// <summary>
        /// Метод для получения координаты (начала и конца) адреса по прямому запросу к яндексу
        /// </summary>
        /// <param name="from">Начало</param>
        /// <param name="to">Конец</param>
        /// <returns>Координаты начала адреса и его конца</returns>
        public static TraceInfo GetTraceInfoDirectly(string from, string to)
        {
            var result = new TraceInfo();

            UpdateKeyAndCookie();
            //var url = string.Format("http://maps.yandex.ru/services/router/search/2.x/search.json?rtm=atm&source=route&rtext={0}~{1}&lang=ru_RU&origin=maps&search_type=geo&simplify=1", from, to);
            var url = string.Format("http://maps.yandex.ru/?rtext={0}~{1}&sll=32.042393%2C54.792209&sspn=0.542450%2C0.067649&rtm=atm&source=form&key={2}&output=json", from, to, s_secretKey);

            Log.Add("HttpRequest", string.Format("Yandex GetTraceInfoDirectly. from: {0}; to: {1}; url: {2}", from, to, url));

            var wr = (HttpWebRequest)WebRequest.Create(url);
            wr.CookieContainer = new CookieContainer();
            wr.CookieContainer.Add(s_cook);

            var response = wr.GetResponse();

            StreamReader objReader = new StreamReader(response.GetResponseStream());
            var str = objReader.ReadToEnd();
            result.Length = GetLen(str);

            result.FromPoint = GetFromPoint(str);
            result.ToPoint = GetToPoint(str);
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
                // в секции "coordinates" первое значение широты, второе - долготы (например: "coordinates":[54.780673,32.035108999999999]),
                // в коде Point.X - долгота, Point.Y - широта
                return new Point(c[1].ToDouble(0), c[0].ToDouble(0));
            }
            return null;
        }

        private static Point? GetToPoint(string str)
        {
            var ind = str.IndexOf("coordinates");
            str = str.Remove(0, ind + 14);

            ind = str.IndexOf("coordinates");
            if (ind < 0)
                return null;
            str = str.Remove(0, ind + 14);

            ind = str.IndexOf("]");
            if (ind > 0)
            {
                str = str.Remove(ind);
                var c = str.Split(',');
                // в секции "coordinates" первое значение широты, второе - долготы (например: "coordinates":[54.780673,32.035108999999999]),
                // в коде Point.X - долгота, Point.Y - широта
                return new Point(c[1].ToDouble(0), c[0].ToDouble(0));
            }
            return null;
        }

        private static List<Point> GetPoints(string str)
        {
            var pointList = new List<Point>();

            // todo: Изучить возможность получения списка точек координат для построения траектории (VFomchenkov)
            return pointList;
        }

        /// <summary>
        /// Выделить значение расстояние в полученном тексте от сервиса Yandex Maps
        /// </summary>
        /// <param name="str">Текст ответа от yandex сервиса</param>
        /// <returns>Расстоние между обектами (в метрах)</returns>
        private static double GetLen(string str)
        {
            double distance = 0;
            try
            {
                // Шаблон регулярного выражения для получения значения расстояния (значение будет в метрах)
                const string regexpPattern = "a*\"Distance\":{\"value\":(?<Distance>[0-9]+\\.[0-9]+),\"text\":\"*";
                var matchList = Regex.Matches(str, regexpPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                // Если нет записей удовлетворяющих шаблону возвращаем distance
                if (matchList.Count < 1)
                    return distance;

                // Берём первую запись удовлетворяющую шаблону
                var match = matchList[0];
                var value = match.Groups["Distance"].Value;

                if (string.IsNullOrEmpty(value))
                    return distance;

                value = value.Replace('.', ',');
                distance = Convert.ToDouble(value);
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception(string.Format("Ошибка получения расстояния между адресами, при разборе ответа от сервиса Yandex Maps "), ex));
            }

            return distance;
        }

        /// <summary>
        /// Куки янденкса, для того чтобы не получать ответ Http 401
        /// </summary>
        private static Cookie s_cook; // yandexuid: "1400138161406008479"

        /// <summary>
        /// Ключ для запросов к картам яндекса (ключ и куки должны быть сопоставимы, в противном случае можно получить всё тот же Http 401)
        /// </summary>
        private static string s_secretKey; // "y67aba456c7a23e7779de1ebfe889368b";

        /// <summary>
        /// Время когда были обновлены Куки последний раз
        /// </summary>
        private static DateTime s_lastUpdateCookie;

        /// <summary>
        /// Обновить куки и ключ
        /// </summary>
        private static void UpdateKeyAndCookie()
        {
            var dateNow = DateTime.Now;

            // Каждые 10 минут обновляем куки и ключ
            const int MINUTES_FOR_UPDATE = 10;

            if (s_cook != null && !string.IsNullOrWhiteSpace(s_secretKey) && ((dateNow - s_lastUpdateCookie).TotalMinutes < MINUTES_FOR_UPDATE))
                return;

            s_lastUpdateCookie = dateNow;

            const string YANDEX_MAP_URL = "http://maps.yandex.ru";
            var mapRequest = (HttpWebRequest)WebRequest.Create(YANDEX_MAP_URL);
            
            mapRequest.CookieContainer = new CookieContainer();

            var mapResponse = (HttpWebResponse)mapRequest.GetResponse();
            foreach (Cookie cookie in mapResponse.Cookies)
            {
                if (cookie.Name != "yandexuid")
                    continue;

                s_cook = new Cookie();
                s_cook.Name = "yandexuid";
                s_cook.Value = cookie.Value;
                s_cook.Path = cookie.Path;
                s_cook.Domain = cookie.Domain;
            }

            var mapStreamReader = new StreamReader(mapResponse.GetResponseStream());
            var page = mapStreamReader.ReadToEnd();

            // Шаблон регулярного выражения для получения значения ключа
            const string REG_EXP_PATTERN = "'secret-key':'(?<Key>[0-9a-z]+)'";

            // Шаблон регулярного выражения для получения значения расстояния (значение будет в метрах)
            var matchList = Regex.Matches(page, REG_EXP_PATTERN, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            // Берём первую запись удовлетворяющую шаблону
            var match = matchList[0];
            s_secretKey = match.Groups["Key"].Value;
        }

        /// <summary>
        /// Получение адреса по координатам
        /// </summary>
        /// <param name="lon">
        /// Долгота
        /// </param>
        /// <param name="lat">
        /// Широта
        /// </param>
        /// <returns>
        /// Адрес
        /// </returns>
        public static string GetAddressByCoordinates(double lon, double lat)
        {
            try
            {
                var coordStr = lat.ToString(CultureInfo.InvariantCulture).Replace(',', '.') + "%2C" + lon.ToString(CultureInfo.InvariantCulture).Replace(',', '.');
                UpdateKeyAndCookie();
                var url = string.Format("http://maps.yandex.ru/?rtext={0}&sspn=0.542450%2C0.092844&rtm=atm&source=form&key={1}&output=json", coordStr, s_secretKey);

                Log.Add("HttpRequest", string.Format("Yandex GetAddressByCoordinates. url: {0}", url));

                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.CookieContainer = new CookieContainer();
                webRequest.CookieContainer.Add(s_cook);

                var response = webRequest.GetResponse();
                // ReSharper disable once AssignNullToNotNullAttribute
                var objReader = new StreamReader(response.GetResponseStream());
                var address = objReader.ReadToEnd();
                var index = address.IndexOf("address", StringComparison.Ordinal);
                address = address.Remove(0, index + 10);
                index = address.IndexOf("\"", StringComparison.Ordinal);
                address = index > 0 ? address.Remove(index) : string.Empty;

                if (string.IsNullOrEmpty(address))
                    return address;

                // Удаляем название страны
                index = address.IndexOf(",", StringComparison.Ordinal);
                address = address.Remove(0, index + 2);
                return address;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Метод для получения данных по длинне пути, координатам (начала и конца) адреса по прямому запросу к яндексу
        /// </summary>
        /// <param name="xfrom"> Координаты начальной точки </param>
        /// <param name="xto"> Координаты конечной точки </param>
        /// <returns>Координаты начала адреса и его конца</returns>
        public static TraceInfo GetTraceInfo(Point xfrom, Point xto)
        {
            var strfrom = xfrom.Y.ToString().Replace(',', '.') + "%2C" + xfrom.X.ToString().Replace(',', '.');
            var strto = xto.Y.ToString().Replace(',', '.') + "%2C" + xto.X.ToString().Replace(',', '.');
            var result = new TraceInfo();

            UpdateKeyAndCookie();
            //var url = string.Format("http://maps.yandex.ru/services/router/search/2.x/search.json?rtm=atm&source=route&rtext={0}~{1}&lang=ru_RU&origin=maps&search_type=geo&simplify=1", strfrom, strto);
            var url = string.Format("http://maps.yandex.ru/?rtext={0}~{1}&sspn=0.542450%2C0.092844&rtm=atm&source=form&key={2}&output=json", strfrom, strto, s_secretKey);

            Log.Add("HttpRequest", string.Format("Yandex GetTraceInfo. url: {0}", url));

            var wr = (HttpWebRequest)WebRequest.Create(url);
            wr.CookieContainer = new CookieContainer();

            wr.CookieContainer.Add(s_cook);

            var response = wr.GetResponse();

            var objReader = new StreamReader(response.GetResponseStream());
            var str = objReader.ReadToEnd();
            result.Length = GetLen(str);

            result.FromPoint = GetFromPoint(str);
            result.ToPoint = GetToPoint(str);
            result.Points = GetPoints(str);

            if (result.FromPoint.HasValue)
                result.Points.Insert(0, result.FromPoint.Value);

            if (result.ToPoint.HasValue)
                result.Points.Add(result.ToPoint.Value);

            return result;
        }

        /// <summary>
        /// Получение списка адресов по запросу
        /// </summary>
        /// <param name="searchPrefix"> Строка запроса для web сервиса </param>
        /// <returns> Список адресов </returns>
        public static List<string> GetAddresList(string searchPrefix)
        {
            var result = new List<string>();

            // Формируем запрос и получаем ответ от Yandex
            var url = string.Format("http://suggest-maps.yandex.ru/suggest-geo?part={0}&lang=ru_RU&search_type=all&ll=31.987928999999973%2C54.78322699999501&spn=0.5424499511718821%2C0.10496852055501904&fullpath=1&v=5&yu=422708661360905933&_=1386318589833", searchPrefix);


            var guid = Guid.NewGuid();
            var startTime = DateTime.Now;
            Log.Add("HttpRequest", string.Format("Yandex GetAddresList. searchPrefix: {1}; guid: {2}  url: {0}", url, searchPrefix, guid));

            var webRequest = WebRequest.Create(url);
            var response = webRequest.GetResponse();
            var responseStream = response.GetResponseStream();
            if (responseStream == null)
                return result;

            var objReader = new StreamReader(responseStream);
            var yandexResponseString = objReader.ReadToEnd();

            // Закрыть соединения
            objReader.Close();
            responseStream.Close();
            response.Close();

            var endTime = DateTime.Now;
            var delta = (endTime - startTime).TotalSeconds;
            Log.Add("HttpRequest", string.Format("Yandex GetAddresList response. guid: {0}, deltaTime: {1}", guid, delta));

            // Шаблон регулярного выражения для получения заготовок адресов
            // полученый ответ от сервера имеет вид:
            // "Россия, Смоленск, улица",
            // [
            //      ["geo","улица Рыленкова, Смоленск, Россия","Россия, Смоленск, улица Рыленкова ",
            //      {
            // 	        "hl":[[0,5],[17,25],[27,33]]
            //      }
            // ],
            // ...
            const string REGEXP_PATTERN = "\"geo\",\"(?<AddresBlank>[а-яА-Яa-zA-z, \\-\"0-9]+)\",{\"hl\":";
            var addresBlankMatchCollection = Regex.Matches(yandexResponseString, REGEXP_PATTERN, RegexOptions.Singleline | RegexOptions.IgnoreCase);


            // Шаблон регулярного выражения для получения адреса из заготовки
            // Заготовка имеет вид:
            // улица Рыленкова, Смоленск, Россия","Россия, Смоленск, улица Рыленкова
            const string REGEXP_ADDRES_PATTERN = "(?<AddresFirst>[а-яА-Яa-zA-z, \\-0-9]+)\",\"(?<AddresSecont>[а-яА-Яa-zA-z, \\-0-9]+)";

            // Если нет записей удовлетворяющих шаблону возвращаем result
            if (addresBlankMatchCollection.Count < 1)
                return result;

            foreach (Match addresBlank in addresBlankMatchCollection)
            {
                // Берём первую запись удовлетворяющую шаблону
                var blank = addresBlank.Groups["AddresBlank"].Value;

                if (string.IsNullOrEmpty(blank))
                    continue;

                var addresMatchCollection = Regex.Matches(blank, REGEXP_ADDRES_PATTERN, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                if (addresMatchCollection.Count == 0)
                    continue;

                // Берём первую запись удовлетворяющую шаблону
                var match = addresMatchCollection[0];
                var value = match.Groups["AddresSecont"].Value;
                if (string.IsNullOrEmpty(value))
                    continue;

                result.Add(value);
            }

            return result;
        }

        /// <summary>
        /// Получить координаты удаленной зоны по названию
        /// </summary>
        /// <param name="remoteZoneName">Название удаленной зоны</param>
        /// <returns>Координаты удаленной зоны</returns>
        public static Point? GetRemoteZoneCoordinates(string remoteZoneName)
        {
            try
            {
                // Для получения координаты необходимо использовать метод, который отдает координаты после непосредственно запроса в яндекс 
                var result = GetTraceInfoDirectly(remoteZoneName, remoteZoneName);
                return result.FromPoint;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ошибка получени координат удаленной зоны по названию : {0}", remoteZoneName), ex);
            }
        }
    }
}
