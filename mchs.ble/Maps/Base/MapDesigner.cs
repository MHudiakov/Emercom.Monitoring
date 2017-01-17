namespace Maps.Base
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using xGraphic;

    public class MapDesigner : xDesignerBase
    {
        static MapSettings _mapSettings = new MapSettings();
        public static MapSettings MapSettings 
        {
            get
            {
                return _mapSettings;
            }
            set
            {
                _mapSettings = value;
            }            
        }

        public System.Windows.Point? CenterCoordinates { get; set; }

        int _z_Level = 10;
        public int Z_Level 
        {
            get
            {
                return this._z_Level;
            }
            set
            {
                this._z_Level = value;
                if (this._z_Level < 5)
                    this._z_Level = 5;
            }
        }
        
        public Point StartPoint { get; set; }

        /// <summary>
        /// Основной конструктор для карты
        /// </summary>
        /// <param name="parentQuery"></param>
        public MapDesigner(object map)
            : base(map)
        {
            this.MapController = new MapController(this);
            this.SnapsToDevicePixels = true;
        }

        /// <summary>
        /// Перегрузить графические элементы 
        /// </summary>
        public virtual void ReloadLoad()
        {
            this.Children.Clear();
            
            this.CreateMapTileManager();
        }

        public void UpdateMapItemsPosition()
        {
            foreach (var i in this.Children.OfType<MapItemUI>().ToList())
                i.UpdatePosition();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            /*try
            {
                if (BackgroundMap != null)
                {
                    BackgroundMap.Width = sizeInfo.NewSize.Width;
                    BackgroundMap.Height = sizeInfo.NewSize.Height;
                    BackgroundMap.RefreshTiles();
                }
                base.OnRenderSizeChanged(sizeInfo);
            }
            catch(Exception ex)
            {
                
            }*/
        }

        public MapController MapController { get; set; }

        public void FocusTo(MapItemUI item)
        {
            if (item == null)
                return;
            if (!item.Coordinates.HasValue)
                return;
            this.FocusTo(item.Coordinates.Value);
        }

        public void FocusTo(Point leftTop, Point rigthBottom)
        {
            var z_level = this.Z_Level;

            this.StartPoint = this.MapController.GetLeftTop(leftTop.X, leftTop.Y, z_level);
            var rb = this.MapController.GetLeftTop(rigthBottom.X, rigthBottom.Y, z_level);
            double dx = rb.X - this.StartPoint.X;
            double dy = rb.Y - this.StartPoint.Y;
            

            while (dx < this.Width && dy < this.Height && z_level < 16)
            {
                z_level++;
                this.StartPoint = this.MapController.GetLeftTop(leftTop.X, leftTop.Y, z_level);
                rb = this.MapController.GetLeftTop(rigthBottom.X, rigthBottom.Y, z_level);
                dx = rb.X - this.StartPoint.X;
                dy = rb.Y - this.StartPoint.Y;
            }

            while ((dx > this.Width || dy > this.Height) && z_level > 6)
            {
                z_level--;
                this.StartPoint = this.MapController.GetLeftTop(leftTop.X, leftTop.Y, z_level);
                rb = this.MapController.GetLeftTop(rigthBottom.X, rigthBottom.Y, z_level);
                dx = rb.X - this.StartPoint.X;
                dy = rb.Y - this.StartPoint.Y;
            }

            this.StartPoint = new Point((long)(this.StartPoint.X - (this.Width - dx) / 2), (long)(this.StartPoint.Y - (this.Height - dy) / 2));
            this.Z_Level = z_level;
            this.BackgroundMap.RefreshTiles();
            this.UpdateMapItemsPosition();
        }

        public void FocusTo(Point centerPoint)
        {
            this.StartPoint = this.MapController.GetLeftTop(centerPoint.X, centerPoint.Y);
            this.StartPoint = new Point((long)this.StartPoint.X - (int)(this.Width / 2), (long)this.StartPoint.Y - (int)(this.Height / 2));
            
            this.BackgroundMap.RefreshTiles();
            this.UpdateMapItemsPosition();
        }


        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            this.SetZLevel(this.Z_Level + (e.Delta > 0 ? 1: -1));
            //base.OnMouseWheel(e);
        }

        public void SetZLevel(int value)
        {
            var dl = value - this.Z_Level;
            if (dl == 0)
                return;
            if (dl > 0)
                this.StartPoint = new Point((long)this.StartPoint.X << dl, (long)this.StartPoint.Y << dl);
            else
                this.StartPoint = new Point((long)this.StartPoint.X >> -dl, (long)this.StartPoint.Y >> -dl);
            this.Z_Level = value;
            Tile.LoadStack.Clear();
            Tile.LoadStack.StopThreads();

            this.BackgroundMap.RefreshTiles();
            this.UpdateMapItemsPosition();
            if (this.BackgroundMap.LastMouseMoveCoordinates.HasValue)
            {
                this.FocusTo(this.BackgroundMap.LastMouseMoveCoordinates.Value);
            }
        }

        public BackgroundMap BackgroundMap { get; set; }
        private void CreateMapTileManager()
        {
            // StartPoint = new Point(38605 * 256, 20802 * 256);
            
            this.Z_Level = MapSettings.DefaultZoomLevel;

            this.CenterCoordinates = new Point(MapSettings.CityCenterLon, MapSettings.CityCenterLat);
            this.BackgroundMap = new BackgroundMap(this);

            this.BackgroundMap.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            this.BackgroundMap.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.Children.Add(this.BackgroundMap);

            this.FocusTo(this.CenterCoordinates.Value);
            this.BackgroundMap.RefreshTiles();
            
            Canvas.SetLeft(this.BackgroundMap, 0);
            Canvas.SetTop(this.BackgroundMap, 0);

            
        }
        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.BackgroundMap != null)
                this.BackgroundMap.DoOnMouseDown(e);
            //base.OnMouseDown(e);
        }
        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            if (this.BackgroundMap != null)
                this.BackgroundMap.DoOnMouseMove(e);
            //base.OnMouseMove(e);
        }
        protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.BackgroundMap != null)
                this.BackgroundMap.DoOnMouseUp(e);
            //base.OnMouseUp(e);
        }
        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            if (this.BackgroundMap != null)
                this.BackgroundMap.DoOnMouseLeave(e);
        }

        public Point GetLeftTop(double lat, double lon)
        {
            var pos = this.MapController.GetLeftTop(lat, lon);
            pos = new Point(pos.X - this.StartPoint.X, pos.Y - this.StartPoint.Y);
            return pos;
        }
    }

   
}
