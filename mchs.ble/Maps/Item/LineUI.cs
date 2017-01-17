// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LineUI.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Класс, отрисовывающий линию на карте
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Maps.Item
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    using Maps.Base;

    using xGraphic;

    /// <summary>
    /// Класс, отрисовывающий линию на карте
    /// </summary>
    public class LineUI : MapItemUI
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LineUI"/>.
        /// </summary>
        /// <param name="pointList"> Список точек для соединения их линией </param>
        /// <param name="mapDesigner"> Карта </param>
        /// <param name="color"> Цвет линии </param>
        public LineUI(List<Point> pointList, MapDesigner mapDesigner, Color color = default(Color))
            : base(pointList, mapDesigner)
        {
            if (mapDesigner == null)
                throw new ArgumentNullException("mapDesigner");

            if (pointList == null)
                throw new ArgumentNullException("pointList");

            // инициализация объекта
            this.Init(mapDesigner);

            this._mainColor = color;
            this.PointList = pointList;
            this.BlackBrush = new SolidColorBrush(this._mainColor);
            this.CreatePath();
        }

        #region Свойства и поля

        /// <summary>
        /// Кисть для отрисовки линии
        /// </summary>
        protected SolidColorBrush BlackBrush { get; set; }

        /// <summary>
        /// Список точек для отображения
        /// </summary>
        public List<Point> PointList { get; set; }

        /// <summary>
        /// Заданный по умолчанию цвет линии
        /// </summary>
        private readonly Color _mainColor;

        /// <summary>
        /// Элемент отрисованной линии
        /// </summary>
        public Path Path { get; private set; } 

        #endregion

        #region Методы

        /// <summary>
        /// Метод отрисовки линии
        /// </summary>
        public void CreatePath()
        {
            if (this.PointList == null)
                return;
            if (this.PointList.Count == 0)
                return;

            var bluePath = new Path
                               {
                                   VerticalAlignment = VerticalAlignment.Stretch,
                                   HorizontalAlignment = HorizontalAlignment.Stretch,
                                   Stroke = this.BlackBrush,
                                   StrokeThickness = 4
                               };

            var blueGeometryGroup = new GeometryGroup();

            var beforePoint = this.PointList[0];
            for (var i = 1; i < this.PointList.Count; i++)
            {
                var p = this.PointList[i];
                var blackLineGeometry1 = new LineGeometry
                                             {
                                                 StartPoint = this.ParentDesigner.GetLeftTop(beforePoint.X, beforePoint.Y),
                                                 EndPoint = this.ParentDesigner.GetLeftTop(p.X, p.Y)
                                             };
                blueGeometryGroup.Children.Add(blackLineGeometry1);
                beforePoint = p;
            }

            bluePath.Data = blueGeometryGroup;
            if (this.Path != null)
                this.ParentDesigner.Children.Remove(this.Path);
            this.ParentDesigner.Children.Add(bluePath);

            this.Path = bluePath;
        }

        /// <summary>
        /// Метод обновления списка точек для трека
        /// </summary>
        /// <param name="newPointList"> Новый список точек </param>
        public void RefreshPoints(List<Point> newPointList)
        {
            this.PointList = newPointList;
            this.CreatePath();
        }

        /// <summary>
        /// Метод изменения выделения линии
        /// </summary>
        /// <param name="check"> Выделен/Не выделен </param>
        public void MarkTrackAsChecked(bool check)
        {
            this.BlackBrush.Color = check ? Color.FromArgb(255, 255, 0, 0) : this._mainColor;
        }

        /// <summary>
        /// Обновление позиции
        /// </summary>
        /// <param name="value"> Координата </param>
        public override void UpdatePosition(Point? value)
        {
            this.CreatePath();
        }

        /// <summary>
        /// Переопределнный метод перетаскивания элемента
        /// </summary>
        /// <param name="parent"> Родительский контрол </param>
        protected override void CreateThumb(xDesignerBase parent)
        {
        }

        /// <summary>
        /// Создание фигуры
        /// </summary>
        protected override void CreateMainShape()
        {
        }

        #endregion
    }
}
