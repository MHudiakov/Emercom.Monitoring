namespace Maps.Base
{
    using System;
    using System.Windows;

    public class MapController
    {
        //public bool IsUseEllips { get; set; }
        MapDesigner Map { get; set; }

        public MapController(MapDesigner map)
        {
            this.Map = map;
            //IsUseEllips = MapDesigner.MapSettings.IsUseEllips;
        }

        public Point GetLeftTop(double lon, double lat, int zoom)
        {
            if (MapDesigner.MapSettings.IsUseEllips)
                return this.GetLeftTopEllips(lon, lat, zoom);
            else
                return this.GetLeftTopScope(lon, lat, zoom);
        }

        public Point GetCoordinates(double tile_x, double tile_y, int zoom = -1)
        {
            if (zoom == -1)
                zoom = this.Map.Z_Level;

            if (MapDesigner.MapSettings.IsUseEllips)
                return this.GetCoordinatesEllips(tile_x, tile_y, zoom);
            else
                return this.GetCoordinatesScope(tile_x, tile_y, zoom);
        }

        public Point GetLeftTop(double lon, double lat)
        {
            return this.GetLeftTop(lon, lat, this.Map.Z_Level);
        }

        public Point GetLeftTopScope(double lon, double lat, int zoom)
        {
            Point p = new Point();
            p.X = (long)(((lon + 180.0) / 360.0 * (1 << zoom)) * 256);
            p.Y = (long)(((1.0 - Math.Log(Math.Tan(lat * Math.PI / 180.0) +
                1.0 / Math.Cos(lat * Math.PI / 180.0)) / Math.PI) / 2.0 * (1 << zoom)) * 256);

            return p;
        }
        public Point GetLeftTopEllips(double lon, double lat, int zoom)
        {
            Point p = new Point();

            double rLon, rLat, a, k, z;
            rLon = lon * Math.PI / 180;
            rLat = lat * Math.PI / 180;
            a = 6378137;
            k = 0.0818191908426;
            z = Math.Tan(Math.PI / 4 + rLat / 2) / Math.Pow((Math.Tan(Math.PI / 4 + Math.Asin(k * Math.Sin(rLat)) / 2)), k);

            p.X = (long)((20037508.342789 + a * rLon) * 53.5865938 / Math.Pow(2, (23 - zoom)));
            p.Y = (long)((20037508.342789 - a * Math.Log(z)) * 53.5865938 / Math.Pow(2, (23 - zoom)));

            return p;
        }

        public Point GetCoordinatesScope(double tile_x, double tile_y, int zoom)
        {
            tile_y = tile_y / 256;
            tile_x = tile_x / 256;

            Point p = new Point();
            double n = Math.PI - ((2.0 * Math.PI * tile_y) / Math.Pow(2.0, zoom));

            p.X = (double)((tile_x / Math.Pow(2.0, zoom) * 360.0) - 180.0);
            p.Y = (double)(180.0 / Math.PI * Math.Atan(Math.Sinh(n)));

            return p;
        }

        public Point GetCoordinatesEllips(double tile_x, double tile_y, int zoom)
        {

            // tile_y = tile_y / 256;
            // tile_x = tile_x / 256;

            Point p = new Point();
            double a, c1, c2, c3, c4, g, z;
            double mercX, mercY;
            a = 6378137;
            c1 = 0.00335655146887969;
            c2 = 0.00000657187271079536;
            c3 = 0.00000001764564338702;
            c4 = 0.00000000005328478445;
            mercX = (tile_x * Math.Pow(2, (23 - zoom))) / 53.5865938 - 20037508.342789;
            mercY = 20037508.342789 - (tile_y * Math.Pow(2, (23 - zoom))) / 53.5865938;

            g = Math.PI / 2 - 2 * Math.Atan(1 / Math.Exp(mercY / a));
            z = g + c1 * Math.Sin(2 * g) + c2 * Math.Sin(4 * g) + c3 * Math.Sin(6 * g) + c4 * Math.Sin(8 * g);

            p.Y = z * 180 / Math.PI;
            p.X = mercX / a * 180 / Math.PI;
            return p;
        }
    }
}
