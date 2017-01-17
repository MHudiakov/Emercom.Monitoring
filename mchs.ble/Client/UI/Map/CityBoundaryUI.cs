// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CityBoundaryUI.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ServerManager.Maps
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Input;

    using global::Maps.Base;

    using xGraphic;

    using Image = System.Windows.Controls.Image;
    using Point = System.Windows.Point;
    using Client.UI.Map;

    /// <summary>
    /// Элемент отображения границы города
    /// </summary>
    public class CityBoundaryUI : MapItemUI
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CityBoundaryUI"/>.
        /// </summary>
        /// <param name="item"> Точка с координатами </param>
        /// <param name="mapDesigner"> Родительский элемент карты </param>
        public CityBoundaryUI(Point item, MapDesigner mapDesigner)
            : base(item, mapDesigner)
        {
            // инициализация объекта
            this.Init(mapDesigner);

            this.Coordinates = new Point(item.X, item.Y);
            this.PreviewMouseLeftButtonUp += MouseUpEvent;
        }

        /// <summary>
        /// Флаг фокусировки на объекте
        /// </summary>
        public bool IsFocusedUI = false;

        /// <summary>
        /// Картинка отображаемая на объекте границы
        /// </summary>
        private Image _image;

        /// <summary>
        /// Событие отпускания левой кнопки мыши
        /// </summary>
        public event Action<CityBoundaryUI> LeftMouseUp;

        /// <summary>
        /// Обработчик отпускания левой кнопки мыши
        /// </summary>
        private void OnMouseClick()
        {
            var handler = LeftMouseUp;
            if (handler != null)
                handler(this);
        }

        /// <summary>
        /// Метод, подписанный на отпускание левой кнопки мыши
        /// </summary>
        /// <param name="sender"> Отправитель </param>
        /// <param name="e"> Аргументы </param>
        private new void MouseUpEvent(object sender, MouseButtonEventArgs e)
        {
                OnMouseClick();
        }

        /// <summary>
        /// Родительский 
        /// </summary>
        public new ServerManagerMap ParentDesigner
        {
            get { return base.ParentDesigner as ServerManagerMap; }
            set { base.ParentDesigner = value; }
        }

        #region Overrides of MapItemUI

        /// <summary>
        /// Создание формы с картинкой для границ города
        /// </summary>
        protected override void CreateMainShape()
        {
            this._image = new Image();
            this.AddChild(this._image);

            // указываем иконку для отображения границы на карте по текущему фокусу элемента
            this.SetIconForFocus();
        }

        /// <summary>
        /// Метод установки иконки при фокусировке
        /// </summary>
        public void SetIconForFocus()
        {
            if (this.IsFocusedUI)
                this.UpdateMainShape(Client.Properties.Resources.CityBoundaryFocused);
            else
                this.UpdateMainShape(Client.Properties.Resources.CityBoundary);
        }

        /// <summary>
        /// Метод перерисовки иконки при фокусировке
        /// </summary>
        /// <param name="icon"> Картинка для состояния выделения </param>
        private void UpdateMainShape(Bitmap icon)
        {
            this.Margin = new Thickness(-5, -60, 5, -35);
            var bi = new BitmapImage();
            bi.BeginInit();
            var ms = new MemoryStream();
            icon.Save(ms, ImageFormat.Png);
            bi.StreamSource = ms;
            bi.EndInit();
            this._image.Source = bi;
        }

        /// <summary>
        /// Инициализация размеров контейнера для объекта
        /// </summary>
        protected override void InitSize()
        {
            Width = 32;
            Height = 37;
        }

        /// <summary>
        /// Метод сдвига элемента границы
        /// </summary>
        /// <param name="e"> Аргументы </param>
        protected override void MoveElement(AfterDrugEventArgs e)
        {
        }

        /// <summary>
        /// Метод окончания перетаскивания объекта границы
        /// </summary>
        public override void EndMoveElement()
        {
            this.OnChanged();
        }

        /// <summary>
        /// Событие перетаскивания элемента границы
        /// </summary>
        public event Action MovedUI;

        /// <summary>
        /// Событие по перетаскиванию элемента границы
        /// </summary>
        private void OnChanged()
        {
            var handler = MovedUI;
            if (handler != null)
                handler();
        }

        #endregion
    }
}
