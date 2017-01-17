// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapItemUI.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Defines the MapItemUI type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Maps.Base
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    using xGraphic;

    /// <summary>
    /// Класс, отображающий объекты на карте
    /// </summary>
    public class MapItemUI : xDesignerItemBase
    {
        /// <summary>
        /// Родительский 
        /// </summary>
        public new MapDesigner ParentDesigner
        {
            get { return base.ParentDesigner as MapDesigner; }
            set { base.ParentDesigner = value; }
        }

        /// <summary>
        /// Элемент запроса, отображаемый с помощью данного визуального элементом
        /// </summary>
        public object MapItem
        {
            get
            {
                return this.BindObject;
            }
        }

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="mapDesigner"> Родительский контрол карты </param>
        public MapItemUI(object item, MapDesigner mapDesigner)
            : base(item, mapDesigner)
        {
        }

        /// <summary>
        /// Метод инициализации объекта на карте
        /// </summary>
        /// <param name="map"> Карта </param>
        public void Init(MapDesigner map)
        {
            this.xInitItem(map);
        }

        /// <summary>
        /// у элемента есть название и поля.
        /// </summary> 
        protected override void CreateMainShape()
        {

            this.MainShape = new Ellipse();
            this.MainShape.Stroke = Brushes.Black;
            this.MainShape.StrokeThickness = 1;
            this.Container.Children.Add(this.MainShape);

            this.SetShadow();

            this.MainText = this.AddText(120, this.Height, "25В", 12, 8, 0, TextAlignment.Left);
            this.MainText.Height = 46;
            this.MainText.Width = 120;
            this.MainText.Margin = new Thickness(20, 0, -120, -20);

            this.MainText.VerticalAlignment = VerticalAlignment.Top;

            this.MainShape.Fill = new LinearGradientBrush(
                                    Color.FromArgb(255, 250, 170, 170),
                                    Color.FromArgb(255, 255, 255, 255),
                                    0);
        }

        /// <summary>
        /// Созданеи элемента перетаскивания
        /// </summary>
        /// <param name="parent"></param>
        protected override void CreateThumb(xDesignerBase parent)
        {
            base.CreateThumb(parent);
            this.DragThumb.OnAfterDrug += new AfterDrugDelegate(this.OnAfterDrug);
        }

        /// <summary>
        /// Перетаскивание объекта
        /// </summary>
        /// <param name="e"> Аргументы </param>
        protected void OnAfterDrug(AfterDrugEventArgs e)
        {
            var x = (e.Left + this.ParentDesigner.StartPoint.X);
            var y = (e.Top + this.ParentDesigner.StartPoint.Y);
            var pos = this.ParentDesigner.MapController.GetCoordinates(x, y);
            this.Coordinates = pos;
            this.MoveElement(e);
            this.EndMoveElement();
        }

        protected virtual void MoveElement(AfterDrugEventArgs e)
        {
            this.MainText.Text = string.Format("lon: {0}, {2}lat: {1}", this.Coordinates.Value.X.ToString("0.000000"), this.Coordinates.Value.Y.ToString("0.000000"), Environment.NewLine);
        }

        /// <summary>
        /// Инициализируем размер
        /// </summary>
        protected override void InitSize()
        {
            this.Height = 10;
            this.Width = 10;
        }

        Point? _coordinates = null;
        public Point? Coordinates
        {
            get
            {
                return this._coordinates;
            }
            set
            {
                this.UpdatePosition(value);
                this._coordinates = value;
            }
        }

        public virtual void UpdatePosition()
        {
            this.UpdatePosition(this.Coordinates);
        }

        public virtual void UpdatePosition(Point? value)
        {
            if (value == null)
                this.Visibility = Visibility.Hidden;
            else
                this.Visibility = Visibility.Visible;

            if (value.HasValue)
            {
                var pos = this.ParentDesigner.MapController.GetLeftTop(value.Value.X, value.Value.Y);
                Canvas.SetLeft(this, pos.X - this.ParentDesigner.StartPoint.X);
                Canvas.SetTop(this, pos.Y - this.ParentDesigner.StartPoint.Y);
            }

        }

        public string GetTextCoordinates()
        {
            if (!this.Coordinates.HasValue)
                return "координаты не заданы";

            var pos = this.Coordinates.Value;
            string lon = GetUserCoordinateString(pos.X);
            string lat = GetUserCoordinateString(pos.Y);
            return lat + " " + lon;
        }

        public static string GetTextCoordinates(Point pos)
        {
            string lon = GetUserCoordinateString(pos.X);
            string lat = GetUserCoordinateString(pos.Y);
            return lat + " " + lon;
        }

        private static string GetUserCoordinateString(double x)
        {
            int gr = (int)x;
            int sec = (int)((x - gr) * 3600);
            int min = sec / 60;
            sec = sec - min * 60;
            string lat = gr.ToString() + "° " + min + "' " + sec + "''";
            return lat;
        }

        /// <summary>
        /// Метод, удаляющий дом с карты
        /// </summary>
        public void DeleteFromMap()
        {
            this.ParentDesigner.Children.Remove(this);
        }
    }
}
