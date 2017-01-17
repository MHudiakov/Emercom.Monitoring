// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapObjectUI.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Основной объект для отображения элементов на карте
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Maps.Item
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows;
    using System.Windows.Media.Imaging;

    using Maps.Base;

    using xGraphic;

    using Image = System.Windows.Controls.Image;
    using Point = System.Windows.Point;

    /// <summary>
    /// Основной объект для отображения элементов на карте
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class MapObjectUI<T> : MapItemUI
        where T : class
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MapObjectUI{T}"/>.
        /// </summary>
        /// <param name="item"> Объект, который необходимо отобразить </param>
        /// <param name="longtitude"> Долгота </param>
        /// <param name="latitude"> Широта </param>
        /// <param name="map"> Карта </param>
        /// <param name="isAllowMove"> Перетаскиваемый ли объект </param>
        public MapObjectUI(T item, double longtitude, double latitude, MapDesigner map, bool isAllowMove = false)
            : base(item, map)
        {
            this.IsAllowMove = isAllowMove;

            // инициализация объекта
            this.Init(map);

            this.Coordinates = new Point(longtitude, latitude);
        }

        #region События
        /// <summary>
        /// Событие перетаскивания элемента
        /// </summary>
        public event Action<Point> MovedUI;

        /// <summary>
        /// Событие по перетаскиванию элемента
        /// </summary>
        /// <param name="point"> Обновленные координаты </param>
        private void OnChanged(Point point)
        {
            var handler = this.MovedUI;
            if (handler != null)
                handler(point);
        }
        #endregion

        #region Поля и свойства

        /// <summary>
        /// Элемент, представленный этим изображением на карте
        /// </summary>
        public T Item
        {
            get
            {
                return this.MapItem as T;
            }
        }

        /// <summary>
        /// Дата последнего обновления координат обеъкта на карте
        /// </summary>
        public DateTime DtLastUpdateCoordinates { get; set; }

        /// <summary>
        /// Текущая иконка объекта
        /// </summary>
        private Image _currentImage;

        /// <summary>
        /// Можно ли передвигать элемент
        /// </summary>
        public bool IsAllowMove { get; private set; }

        /// <summary>
        /// Текущий значок элемента
        /// </summary>
        public kMapIcons CurrentImage { get; private set; }

        #endregion

        #region Методы

        /// <summary>
        /// Основной метод создания элемента
        /// </summary>
        protected override void CreateMainShape()
        {
            this._currentImage = new Image();
            this.CurrentImage = kMapIcons.Store;
            this.AddChild(this._currentImage);
        }

        /// <summary>
        /// Метод установки иконки для объекта
        /// </summary>
        /// <param name="iconName"> Картинки </param>
        public void SetIcon(kMapIcons iconName)
        {
            // if (this.CurrentImage == iconName)
            //     return;

            var icon = Properties.Resources.ResourceManager.GetObject(iconName.ToString("g")) as Bitmap;
            if (icon == null)
                return;
            var bi = new BitmapImage();
            bi.BeginInit();
            var ms = new MemoryStream();
            icon.Save(ms, ImageFormat.Png);
            bi.StreamSource = ms;
            bi.EndInit();
            this._currentImage.Source = bi;
            this.CurrentImage = iconName;
        }

        /// <summary>
        /// Метод инициализации размера иконки машинки
        /// </summary>
        protected override void InitSize()
        {
            this.Width = 25;
            this.Height = 25;
        }

        /// <summary>
        /// Метод установки текста
        /// </summary>
        /// <param name="text"> Текст, который необходимо отобразить </param>
        public void SetText(string text)
        {
            if (this.MainText == null)
            {
                this.MainText = this.AddText(120, this.Height, "-", 12, 8, 0, TextAlignment.Left);
                this.MainText.Height = 46;
                this.MainText.Width = 120;
                this.MainText.Margin = new Thickness(30, 0, -120, -30);

                this.MainText.VerticalAlignment = VerticalAlignment.Top;
            }

            this.MainText.Text = text;
        }

        /// <summary>
        /// Метод установки пиксельного смещения иконки внутри контрола
        /// </summary>
        /// <param name="left"> Смещение влево </param>
        /// <param name="top"> Смещение вверх </param>
        /// <param name="right"> Смещение вправо </param>
        /// <param name="bottom"> Смещение вниз </param>
        public void SetIconMargin(double left, double top, double right, double bottom)
        {
            this.Margin = new Thickness(left, top, right, bottom);
        }

        /// <summary>
        /// Метод создания перетаскиваемого элемента
        /// </summary>
        /// <param name="parent"> Дизайнер </param>
        protected override void CreateThumb(xDesignerBase parent)
        {
            if (this.IsAllowMove)
                base.CreateThumb(parent);
        }

        /// <summary>
        /// Окончание перетаскивания элемента
        /// </summary>
        public override void EndMoveElement()
        {
            var point = new Point();
            var coordinates = this.Coordinates;
            if (coordinates != null)
            {
                point.X = coordinates.Value.X;
                point.Y = coordinates.Value.Y;
            }

            this.OnChanged(point);
        }

        #endregion
    }
}
