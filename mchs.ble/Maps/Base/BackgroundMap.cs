namespace Maps.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class BackgroundMap : Canvas
    {
        public Point StartTile { get; set; }
        public List<Tile> Tiles { get; private set; }
        public int WidthTilesCnt { get; set; }
        public int HeightTilesCnt { get; set; }
        public new MapDesigner Parent { get; protected set; }

        protected TextBlock Coordinates = new TextBlock() 
        {
            IsHitTestVisible = false, 
            Visibility = Visibility.Hidden 
        };
        public BackgroundMap(MapDesigner designer)
            : base()
        {
            this.Parent = designer;
            this.Tiles = new List<Tile>();
        }

        bool isLeftMouseButtonPressed = false;
        Point? fromMoveXY = null;

        public void DoOnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            this.OnMouseDown(e);
        }
        public void DoOnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                this.OnMouseUp(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                this.EndMove();
                
                this.CaptureMouse();
                
                this.isLeftMouseButtonPressed = e.ChangedButton == System.Windows.Input.MouseButton.Left;
                this.fromMoveXY = e.GetPosition(this);

                this.Parent.Cursor = Cursors.Hand;
                this.Cursor = Cursors.Hand;
                
                base.OnMouseDown(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        // ispressed = false
        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            /*Cursor = Cursors.Arrow;
            isLeftMouseButtonPressed = false;
            fromMoveXY = null;
            base.OnMouseLeave(e);*/
        }

        public void DoOnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            this.OnMouseMove(e);


        }

        public void DoOnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            this.OnMouseLeave(e);
        }
        public Point? LastMouseMoveCoordinates = null;
        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                var pos = e.GetPosition(this);
                if (this.fromMoveXY != null && this.isLeftMouseButtonPressed)
                {
                    this.MoveTiles(this.Parent.StartPoint.X - pos.X + this.fromMoveXY.Value.X,
                        this.Parent.StartPoint.Y - pos.Y + this.fromMoveXY.Value.Y);
                    this.fromMoveXY = pos;
                    this.Parent.UpdateMapItemsPosition();
                }

                Canvas.SetLeft(this.Coordinates, e.GetPosition(this).X);
                Canvas.SetTop(this.Coordinates, e.GetPosition(this).Y);

                /*var x = pos.X + Parent.StartPoint.X;
                var y = pos.Y + Parent.StartPoint.Y;
                var wp = Parent.MapController.GetCoordinates(x, y);
                Coordinates.Text = string.Format("x={0}, y={1}, lat={2}, lon={3}", x, y, wp.X, wp.Y);
                */
                var x = pos.X + this.Parent.StartPoint.X;
                var y = pos.Y + this.Parent.StartPoint.Y;
                var wp = this.Parent.MapController.GetCoordinates(x, y);
                this.LastMouseMoveCoordinates = wp;
                base.OnMouseMove(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        


        // ispressed = false
        protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                this.EndMove();
                base.OnMouseUp(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private void EndMove()
        {
            this.ReleaseMouseCapture();
            this.Cursor = Cursors.Arrow;
            this.Parent.Cursor = Cursors.Arrow; 
            this.fromMoveXY = null;
            this.isLeftMouseButtonPressed = false;

            /*foreach (var i in Parent.Children.OfType<xDesignerItemBase>())
                (i as xDesignerItemBase).EndMoveElement();*/
        }


        public void RefreshTiles()
        {
            try
            {
                //Children.Clear();            
                //ClearTiles();
                this.StartTile = this.GetTileXYIndex(Convert.ToInt64(this.Parent.StartPoint.X), Convert.ToInt64(this.Parent.StartPoint.Y), this.Parent.Z_Level);

                this.WidthTilesCnt = Convert.ToInt32((this.Parent as Canvas).Width / 256) + 3;
                this.HeightTilesCnt = Convert.ToInt32((this.Parent as Canvas).Height / 256) + 3;

                //---- tile xy = 256*256 ----//
                this.SnapsToDevicePixels = true;

                for (int i = 0; i < this.WidthTilesCnt; i++)
                {
                    for (int j = 0; j < this.HeightTilesCnt; j++)
                    {
                        var t = this.GetTile(i + (int)this.StartTile.X, j + (int)this.StartTile.Y);

                        var left = this.GetLeftTile(i);
                        var top = this.GetTopTile(j);
                        Canvas.SetLeft(t, left);
                        Canvas.SetTop(t, top);

                        // debug
                        TextBlock tb;
                        if (t.DataContext as TextBlock == null)
                        {
                            tb = new TextBlock();
                            t.DataContext = tb;
                            tb.IsHitTestVisible = false;
                            this.Children.Add(tb);
                            tb.Visibility = System.Windows.Visibility.Hidden;
                        }
                        else
                            tb = t.DataContext as TextBlock;

                        Canvas.SetLeft(tb, left);
                        Canvas.SetTop(tb, top);

                        tb.Text = string.Format("x={0}, y={1}, left={2}, top={3}, z={4}", i + (int)this.StartTile.X, j + (int)this.StartTile.Y, left, top, this.Parent.Z_Level);
                    }
                }

                foreach (var t in this.Tiles.ToList())
                {
                    if (t.X < (int)this.StartTile.X-1 || t.X >= (int)this.StartTile.X + this.WidthTilesCnt+1 ||
                        t.Y < (int)this.StartTile.Y-1 || t.Y >= (int)this.StartTile.Y + this.HeightTilesCnt+1)
                    {
                        this.Tiles.Remove(t);
                        this.Children.Remove(t);
                    }
                }

                // debug
                if (this.Children.IndexOf(this.Coordinates) == -1)
                {
                    this.Children.Add(this.Coordinates);
                }
                else
                    SetZIndex(this.Coordinates, int.MaxValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private Tile GetTile(int x, int y)
        {
            var t = this.Tiles.FirstOrDefault(tile => tile.X == x && tile.Y == y);
            if (t == null)
            {
                t = new Tile(this, x, y);
                this.Tiles.Add(t);
                this.Children.Add(t);
            }
            return t;
        }

        private double GetTopTile(int topIndex)
        {
            var dy = this.Parent.StartPoint.Y - (this.StartTile.Y * 256);
            return topIndex * 256 - dy;
        }

        private double GetLeftTile(int leftIndex)
        {
            var dx = this.Parent.StartPoint.X - (this.StartTile.X * 256);
            return leftIndex * 256 - dx;
        }

        protected void ClearTiles()
        {
            foreach (var t in this.Tiles)
            {
                this.Children.Remove(t);
                this.Children.Remove(t.DataContext as UIElement);
            }
            this.Tiles.Clear();
        }

        private Point GetTileXYIndex(long x_start, long y_start, int z_level)
        {
            // todo: выход за границы z_level
            var res = new Point((int)(x_start / 256) - 1, (int)(y_start / 256) - 1);
            return res;
        }

        public void MoveTiles(double newStartPointX, double newStartPointY)
        {
            // 1. move all tiles
            /*
            var dx = Parent.StartPoint.X - newStartPointX;
            var dy = Parent.StartPoint.Y - newStartPointY;

            foreach(var t in Tiles)
            {
                var left = Canvas.GetLeft(t);
                Canvas.SetLeft(t, left + dx);
                var top = Canvas.GetTop(t);
                Canvas.SetTop(t, top + dy);
            }
            var newStartTilesPosition = GetTileXYIndex(Convert.ToInt64(newStartPointX), Convert.ToInt64(newStartPointY), Parent.Z_Level);*/
            // 2. kill Tiles


            // 3. new Tiles


            /*ClearTiles();*/
            this.Parent.StartPoint = new Point(newStartPointX, newStartPointY);

            this.RefreshTiles();

            var items = this.Parent.Children.OfType<MapItemUI>().ToList();
            foreach (var i in items)
                i.UpdatePosition();
        }
    }
}
