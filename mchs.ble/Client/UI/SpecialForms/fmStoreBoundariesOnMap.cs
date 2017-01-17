using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Init.Tools.UI;
using Init.Tools;
using ServerManager.Maps;
using System.Windows;
using System.Windows.Media;
using PointLocalisation;
using DAL.WCF;
using DAL.WCF.ServiceReference;
using Maps;
using Maps.Item;

namespace Client.UI.SpecialForms
{
    public partial class fmStoreBoundariesOnMap : XtraForm
    {
        public fmStoreBoundariesOnMap()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Объект отвечающий за отрисовку карты
        /// </summary>
        private ConvexPolygonMap _map;

        /// <summary>
        /// Текущий выделенный многоугольник
        /// </summary>
        private CityConvexPolygonUi _currentFocusedPolygon;

        /// <summary>
        /// Настройки сервера
        /// </summary>
        private Store Store { get; set; }

        //private string storeBoundary { get { return this.Store.StoreBoundaries; } }

        /// <summary>
        /// Метод вызова формы
        /// </summary>
        /// <param name="serverSettings"> Настройки сервера </param>
        /// <returns> Удачно ли прошло редактирование </returns>
        public static bool Execute(Store store)
        {
            using (var fm = new fmStoreBoundariesOnMap())
            {
                fm.Store = store;
                return fm.ShowDialog() == DialogResult.OK;
            }
        }

        /// <summary>
        /// Метод записи координат выставленных на карте точек в строку
        /// </summary>
        /// <returns> Строка с координатами </returns>
        private string CreateStringCoordinate()
        {
            try
            {
                var stringCoordinate = "[";
                for (var i = 0; i < _map.CityConvexPolygonUIList.Count; i++)
                {
                    stringCoordinate += _map.CityConvexPolygonUIList[i].CreateStringCoordinate();
                    if (i != _map.CityConvexPolygonUIList.Count - 1)
                        stringCoordinate += " | ";
                }

                stringCoordinate += "]";
                return stringCoordinate;
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception("Ошибка преобразования списков координат многоугольников в строку", ex));
                xMsg.Error("Произошла ошибка сохранения выставленных значений границ в базу данных. Обратитесь к разработчику");
                return this.Store.StoreBoundaries;
            }
        }

        /// <summary>
        /// Метод загрузки формы
        /// </summary>
        /// <param name="sender"> Отправитель </param>
        /// <param name="e"> Аргументы </param>
        private void CityBoundariesOnMapLoad(object sender, EventArgs e)
        {
            try
            {
                this._map = new ConvexPolygonMap { AllowDrop = true, Background = Brushes.Transparent, Focusable = true };
                this._map.MouseEnter += this.MapMouseEnter;
                this.UpdateMapLayout();
                this._map.ReloadLoad();

                var pointUI = new MapObjectUI<Store>(Store, Store.Longitude, Store.Latitude, _map);
                pointUI.SetIconMargin(-25, -37, 25, 37);
                pointUI.SetIcon(kMapIcons.Store);

                // загрузка текущих многоугольников границ на карту для отображения
                var coordinatesForPolygonsList = StoreBoundaryCoordinateParser.ParseStringCoordinates(this.Store.StoreBoundaries);

                foreach (var coordinatesForPolygonList in coordinatesForPolygonsList)
                {
                    if (coordinatesForPolygonList.Any())
                    {
                        var cityBoundariesPointList = new List<System.Windows.Point>();
                        foreach (var point in coordinatesForPolygonList)
                            cityBoundariesPointList.Add(new System.Windows.Point(point.X, point.Y));

                        var newConvexpolygon = this._map.AddCityBoundaryPolygon(cityBoundariesPointList);
                        newConvexpolygon.PolygonClick += CityConvexPolygonUiOnPolygonClick;
                    }
                }

                // фокусировка карты на границах города
                this.FocusMapOnBoundaries();

                this.mapHost.Child = this._map;
                this.UpdateMapLayout();
            }
            catch (Exception ex)
            {
                xMsg.Error(@"Произошла ошибка. Невозможно отобразить карту!");
                Log.AddException(new Exception("Произошла ошибка при отображении карты", ex));
            }
        }

        /// <summary>
        /// Обработчик события выделения многоугольника
        /// </summary>
        /// <param name="cityConvexPolygonUi"> Текущий выделенный многоугольник </param>
        private void CityConvexPolygonUiOnPolygonClick(CityConvexPolygonUi cityConvexPolygonUi)
        {
            _currentFocusedPolygon = cityConvexPolygonUi;
            this.HighlightPolygon(_currentFocusedPolygon);
        }

        /// <summary>
        /// Метод подсвечивания многоугольника
        /// </summary>
        /// <param name="cityConvexPolygonUi"> Многоугольник, который надо подсветить </param>
        private void HighlightPolygon(CityConvexPolygonUi cityConvexPolygonUi)
        {
            foreach (var cityPolygonUi in _map.CityConvexPolygonUIList)
                if (cityPolygonUi != cityConvexPolygonUi)
                    cityPolygonUi.Checked(false);
                else
                    cityPolygonUi.Checked(true);
        }

        /// <summary>
        /// Метод обновления карты
        /// </summary>
        private void UpdateMapLayout()
        {
            if (this._map != null)
            {
                this._map.Width = this.mapHost.Width;
                this._map.Height = this.mapHost.Height;
            }
        }

        /// <summary>
        /// Метод изменения размера карты
        /// </summary>
        /// <param name="sender"> Отправитель </param>
        /// <param name="e"> Аргументы </param>
        private void CityBoundariesOnMapResize(object sender, EventArgs e)
        {
            try
            {
                this.UpdateMapLayout();

                // фокусировка карты на границах города
                this.FocusMapOnBoundaries();
            }
            catch (Exception ex)
            {
                Log.AddException("errors", new Exception("Ошибка при обновлении карты на форме \"Задание границ города\"", ex));
            }
        }

        /// <summary>
        /// Метод фокусировки карты
        /// </summary>
        /// <param name="sender"> Отправитель </param>
        /// <param name="e"> Аргументы </param>
        private void MapMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                this._map.Focus();
            }
            catch (Exception ex)
            {
                xMsg.Error("Ошибка при фокусировке карты");
                Log.AddException("errors", new Exception("Ошибка при фокусировке карты на форме", ex));
            }
        }

        /// <summary>
        /// Кнопка "Добавить" границу города
        /// </summary>
        /// <param name="sender"> Отправитель </param>
        /// <param name="e"> Аргументы </param>
        private void SbAddCityBoundaryClick(object sender, EventArgs e)
        {
            try
            {
                if (_currentFocusedPolygon == null)
                {
                    xMsg.Error("Не выделен ни один многоугольник для добавления в него вершины!");
                    return;
                }

                _currentFocusedPolygon.AddCityBoundary();

                // фокусировка карты на границах города
                this.FocusMapOnBoundaries();

                this.UpdateMapLayout();
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception("Произошла ошибка добавления нового элемента границы на карту", ex));
                System.Windows.MessageBox.Show("Произошла ошибка добавления нового элемента границы на карту");
            }
        }

        /// <summary>
        /// Кнопка "Удалить" границу города
        /// </summary>
        /// <param name="sender"> Отправитель </param>
        /// <param name="e"> Аргументы </param>
        private void SbDeleteCityBoundaryClick(object sender, EventArgs e)
        {
            try
            {
                if (_currentFocusedPolygon == null)
                {
                    xMsg.Error("Не выделен ни один элемент для удаления");
                    return;
                }

                if (_currentFocusedPolygon.CityBoundaryUIList.Count == 3)
                {
                    xMsg.Error("В многоугольнике не может быть меньше трех вершин!");
                    return;
                }

                // удаление выделенной границы
                _currentFocusedPolygon.DeleteFocusedBoundaryUI();
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception("Произошла ошибка при удалении элемента границы с карты", ex));
                xMsg.Error("Произошла ошибка при удалении элемента границы с карты");
            }
        }

        /// <summary>
        /// По координатам отображаемых многоугольников находятся координаты левого верхнего и правого нижнего углов, и по этим точкам производится фокусировка карты
        /// </summary>
        private void FocusMapOnBoundaries()
        {
            // перегоняем координаты всех точек всех многоугольников в список, чтобы найти оптимальную фокусировку
            var cityBoundariesList = new List<System.Windows.Point>();
            foreach (var cityConvexPolygonUi in _map.CityConvexPolygonUIList)
                foreach (var cityBoundaryUI in cityConvexPolygonUi.CityBoundaryUIList)
                    if (cityBoundaryUI.Coordinates != null)
                        cityBoundariesList.Add(new System.Windows.Point(cityBoundaryUI.Coordinates.Value.X, cityBoundaryUI.Coordinates.Value.Y));

            if (!cityBoundariesList.Any())
                return;

            // ищем координаты левого верхнего угла и правого нижнего по координатам устройств
            var rightBottomLat = cityBoundariesList[0].Y;
            var rightBottomLon = cityBoundariesList[0].X;
            var leftTopLat = cityBoundariesList[0].Y;
            var leftTopLon = cityBoundariesList[0].X;
            for (int i = 1; i < cityBoundariesList.Count; i++)
            {
                if (cityBoundariesList[i].Y > leftTopLat)
                    leftTopLat = cityBoundariesList[i].Y;
                if (cityBoundariesList[i].X > rightBottomLon)
                    rightBottomLon = cityBoundariesList[i].X;
                if (cityBoundariesList[i].Y < rightBottomLat)
                    rightBottomLat = cityBoundariesList[i].Y;
                if (cityBoundariesList[i].X < leftTopLon)
                    leftTopLon = cityBoundariesList[i].X;
            }

            var leftTopPoint = new System.Windows.Point(leftTopLon, leftTopLat);
            var rigthBottomPoint = new System.Windows.Point(rightBottomLon, rightBottomLat);

            // фокусируемся на найденных координатах
            if (leftTopPoint == rigthBottomPoint)
                this._map.FocusTo(leftTopPoint);
            else
                this._map.FocusTo(new System.Windows.Point(leftTopPoint.X, leftTopPoint.Y), new System.Windows.Point(rigthBottomPoint.X, rigthBottomPoint.Y));
        }

        /// <summary>
        /// Метод проверки всех построенных многоугольников на выпуклость
        /// </summary>
        /// <returns> True - если выпуклый, в противном случае false </returns>
        private bool CheckConvexPolygon()
        {
            if (this._map != null)
                foreach (var cityBoundaryPolygon in this._map.CityConvexPolygonUIList)
                    if (cityBoundaryPolygon.CheckConvexPolygon() != true)
                    {
                        this.HighlightPolygon(cityBoundaryPolygon);
                        xMsg.Error("Один из многоугольников не является выпуклым! Сделайте его выпуклым!");
                        return false;
                    }

            return true;
        }

        /// <summary>
        /// Метод проверки количества вершин у многоугольников
        /// </summary>
        /// <returns> Пройдена ли проверка  </returns>
        private bool CheckTopCount()
        {
            if (_map != null)
                foreach (var cityBoundaryPolygon in this._map.CityConvexPolygonUIList)
                    if (cityBoundaryPolygon.CheckTopCount() != true)
                    {
                        this.HighlightPolygon(cityBoundaryPolygon);
                        xMsg.Error("Количество вершин одного из многоугольников меньше 3-х! Исправьте это!");
                        return false;
                    }

            return true;
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        private void fmCityBoundariesOnMapFormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
                if (!this.CheckConvexPolygon() || !this.CheckTopCount())
                    e.Cancel = true;
                else
                {
                    this.Store.StoreBoundaries = this.CreateStringCoordinate();
                    DalContainer.WcfDataManager.ServiceOperationClient.EditStore(this.Store);
                }
        }

        /// <summary>
        /// Обработчик кнопки добавления нового многоугольника на карту
        /// </summary>
        /// <param name="sender"> Отправитель </param>
        /// <param name="e"> Аргументы </param>
        private void SbAddPolygonClick(object sender, EventArgs e)
        {
            try
            {
                var cityConvexPolygonUi = this._map.AddCityBoundaryPolygon(new List<System.Windows.Point>());
                cityConvexPolygonUi.PolygonClick += CityConvexPolygonUiOnPolygonClick;

                // фокусировка карты на границах города
                this.FocusMapOnBoundaries();
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception("Ошибка при добавлении нового многоугольника на карту", ex));
                xMsg.Error("Ошибка при добавлении нового многоугольника на карту!");
            }
        }

        /// <summary>
        /// Обработчик кнопки удаления выбранного многоугольника с карты
        /// </summary>
        /// <param name="sender"> Отправитель  </param>
        /// <param name="e"> Аргументы  </param>
        private void SbDeletePolygonClick(object sender, EventArgs e)
        {
            try
            {
                if (_currentFocusedPolygon == null)
                {
                    xMsg.Error("Не выделен ни один элемент для удаления");
                    return;
                }

                // отписываемся от события выделения многоугольника
                _currentFocusedPolygon.PolygonClick -= this.CityConvexPolygonUiOnPolygonClick;

                // удаляем выбранный многоугольник
                _map.DeleteCityBoundaryPolygon(_currentFocusedPolygon);
                _currentFocusedPolygon = null;
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception("Ошибка при удалении выбранного многоугольника с карты", ex));
                xMsg.Error("Ошибка при удалении выбранного многоугольника с карты!");
            }
        }
    }
}