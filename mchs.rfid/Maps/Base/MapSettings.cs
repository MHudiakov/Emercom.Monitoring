namespace Maps.Base
{
    public class MapSettings
    {
        public MapSettings()
        {
            this.CalculateAddressCoordinatesSource = 1;
            this.CalculateCoordinatesSource = 1;
            this.CalculateDistanceSource = 1;
            this.CityBoundaries = "[]";
            this.CityCenterLat = 32.051026;
            this.CityCenterLon = 54.778486;
            this.DefaultZoomLevel = 14;
            this.RusCityName = "Смоленск";
            //TileCashPrefix = "Google";
            //TileSampleURL = "http://mt0.google.com/vt/lyrs=m@176116505&hl=ru&src=app&x={0}&y={1}&z={2}&s=Gali"; // google
            //TileCashPrefix = "OSM";
            //TileSampleURL = "http://otile4.mqcdn.com/tiles/1.0.0/osm/{2}/{0}/{1}.png"; //OSM
            this.TileCashPrefix = "Yandex";
            this.TileSampleURL = "http://vec01.maps.yandex.net/tiles?l=map&v=2.26.0&x={0}&y={1}&z={2}&lang=ru-RU"; //Yandex
            this.IsUseEllips = true;
        }

        static MapSettings Active { get { return MapDesigner.MapSettings; } }

        /// <summary>
        /// Использовать элиптический расчет координат (для яндекса)
        /// </summary>
        public bool IsUseEllips { get; set; }
        
        /// <summary>
        /// Центр город - широта
        /// </summary>
        public double CityCenterLat { get; set; }

        /// <summary>
        /// Центр города - долгота
        /// </summary>
        public double CityCenterLon { get; set; }

        /// <summary>
        /// Зум по-умолчанию
        /// </summary>
        public int DefaultZoomLevel { get; set; }

        /// <summary>
        /// Наименование города для поиска домов и улиц
        /// </summary>
        public string RusCityName { get; set; }

        /// <summary>
        /// Автоматический расчет координат по: 1 - Яндекс карта, 2 - OSM (Open street map)
        /// </summary>
        public int CalculateCoordinatesSource { get; set; }

        /// <summary>
        /// Границы города
        /// </summary>
        public string CityBoundaries { get; set; }

        /// <summary>
        /// Автоматический расчет координат адреса (дома/улицы) по: 0 - отключен, 1 - Яндекс карта, 2 - OSM (Open street map)
        /// </summary>
        // TODO MHudiakov: неиспользуемая настройка
        public int CalculateAddressCoordinatesSource { get; set; }

        /// <summary>
        /// Автоматический расчет расстояния между адресами по: 0 - отключен, 1 - Яндекс карта, 2 - OSM (Open street map)
        /// </summary>
        public int CalculateDistanceSource { get; set; }

        /// <summary>
        /// Путь, от куда берутся тайлы:
        /// http://otile4.mqcdn.com/tiles/1.0.0/osm/{2}/{0}/{1}.png
        /// или
        /// http://a.tile.openstreetmap.org/{2}/{0}/{1}.png
        /// , где будет подставляться {0} - X координата тайла
        /// {1} - Y координата тайла
        /// {2} - Масштаб (Z level)
        /// </summary>
        public string TileSampleURL { get; set; }

        /// <summary>
        /// Префикс папки с кешем тайлов
        /// </summary>
        public string TileCashPrefix { get; set; }
    }
}
