//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="ucTripRealTime.cs" company="ИНИТ-центр">
////   ИНИТ-центр, 2016г.
//// </copyright>
//// <summary>
////   Мониторинг поездок
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------

namespace Client.UI.Controls
{
    using System;
    using System.Data;
    using System.Text;
    using System.Linq;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.ComponentModel;
    using System.Collections.Generic;
    using DevExpress.XtraEditors;
    using Init.Tools.UI;
    using Client.UI.Map;
    using Client.UI.Controls;
    using DAL.WCF;
    using DAL.WCF.ServiceReference;
    using Maps.Item;
    using global::Maps;
    using global::Maps.Item;
    using Init.Tools;
    using Init.Tools.DevExpress;
using Client.UI.SpecialForms;
    using PointLocalisation;

    /// <summary>
    /// Форма объектов
    /// </summary>
    public partial class ucTripRealTime : XtraUserControl, IBaseControl
    {
        public FormHelper FormHelper { get; set; }

        public string Header { get; set; }

        private bool IsFirstLoading = true;

        private bool IsMonitorCar = false;

        private int IdFrom { get; set; }

        /// <summary>
        /// Карта
        /// </summary>
        private ServerManagerMap _map;

        /// <summary>
        /// Линия
        /// </summary>
        private LineUI _traceLine;

        private LineUI _boundaryLine;

        /// <summary>
        /// Список точек для линии
        /// </summary>
        private List<System.Windows.Point> _linePointList = new List<System.Windows.Point>();

        /// <summary>
        /// Поездка
        /// </summary>
        private Trip Trip { get; set; }

        /// <summary>
        /// Машина
        /// </summary>
        private Unit Unit { get; set; }

        private Store Store { get; set; }

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public ucTripRealTime()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Активатор формы
        /// </summary>
        public void Activate()
        {
            if (IsFirstLoading)
            {
                lueUnit.Properties.DataSource = DalContainer.WcfDataManager.UnitList.Where(u => !u.IsStore).ToList();

                // отрисовка карты с базой
                try
                {
                    // загрузка карты
                    _map = new ServerManagerMap { AllowDrop = true, Background = Brushes.Transparent, Focusable = true };
                    _map.MouseEnter += MapMouseEnter;

                    UpdateMapLayout();
                    _map.ReloadLoad();
                    mapHost.Child = _map;
                    UpdateMapLayout();

                    // добавляем базу на карту
                    Store = DalContainer.WcfDataManager.ServiceOperationClient.GetAllStore()[0];
                    var addressPointUI = new MapObjectUI<Store>(Store, Store.Longitude, Store.Latitude, _map);
                    addressPointUI.SetIconMargin(-25, -37, 25, 37);
                    addressPointUI.SetIcon(kMapIcons.Store);

                    var coordinatesForPolygonsList = StoreBoundaryCoordinateParser.ParseStringCoordinates(this.Store.StoreBoundaries);

                    foreach (var coordinatesForPolygonList in coordinatesForPolygonsList)
                    {
                        if (coordinatesForPolygonList.Any())
                        {
                            var cityBoundariesPointList = new List<System.Windows.Point>();
                            foreach (var point in coordinatesForPolygonList)
                                cityBoundariesPointList.Add(new System.Windows.Point(point.X, point.Y));

                            cityBoundariesPointList.Add(new System.Windows.Point(cityBoundariesPointList[0].X, cityBoundariesPointList[0].Y));

                            _boundaryLine = new LineUI(cityBoundariesPointList, _map, Color.FromArgb(230, 60, 60, 60));
                        }
                    }

                    IsFirstLoading = false;
                }
                catch (Exception ex)
                {
                    Log.AddException(new Exception("Возникли проблемы при отрисовке карты мониторинга вызова", ex));
                }
            }
            else
            {
                if (IsMonitorCar)
                {
                    DrawCarTrack();
                    tmRefresh.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Дезактиватор формы
        /// </summary>
        public void Deactivate()
        {
            tmRefresh.Enabled = false;
        }

        private void sbFind_Click(object sender, EventArgs e)
        {
            try
            {
                tmRefresh.Enabled = false;

                gcUniqEquipment.DataSource = null;
                gcUniqEquipment.Refresh();
                gcNonUniqEquipment.DataSource = null;
                gcNonUniqEquipment.Refresh();
                gcMovement.DataSource = null;
                gcMovement.Refresh();

                tabbedControlGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                Unit = DalContainer.WcfDataManager.UnitList.FirstOrDefault(u => u.Id == (int)lueUnit.EditValue);
                if (Unit == null)
                {
                    xMsg.Information("Выберите машину!");
                    return;
                }

                Trip = DalContainer.WcfDataManager.ServiceOperationClient.GetLastTrip(Unit.Id);

                IdFrom = 0;

                _map.ReloadLoad();
                mapHost.Child = _map;
                UpdateMapLayout();

                DrawCarTrack();
                tmRefresh.Enabled = true;

                IsMonitorCar = true;
            }
            catch (Exception ex)
            {
                Log.Add("Возникли проблемы при загрузке комплектации вызова с ID = " + Trip.Id);
            }
        }

        private void DrawCarTrack()
        {
            try
            {
                _linePointList.Clear();

                var unitList = _map.Children.OfType<MapObjectUI<GeoPoints>>().ToList();
                foreach (var unit in unitList)
                    _map.Children.Remove(unit);

                // добавляем базу на карту
                //var store = DalContainer.WcfDataManager.ServiceOperationClient.GetAllStore()[0];
                var pointUI = new MapObjectUI<Store>(Store, Store.Longitude, Store.Latitude, _map);
                pointUI.SetIconMargin(-25, -37, 25, 37);
                pointUI.SetIcon(kMapIcons.Store);

                if (Trip == null)
                    return;

                // выгрузка списка точек
                var addresPointList = GetGeoPointsList();

                // подготавливаем точки для отрисовки треков 
                PrepareTrackPointLists();

                // отрисовываем треки заказа
                _traceLine = new LineUI(_linePointList, _map, Color.FromArgb(200, 150, 80, 255));

                // добавляем конечную точку на карту в виде иконки
                if (addresPointList.Any())
                {
                    var addressPointUI = new MapObjectUI<GeoPoints>(addresPointList.Last(), addresPointList.Last().Longitude, addresPointList.Last().Latitude, _map);
                    addressPointUI.SetIconMargin(-25, -37, 25, 37);
                    addressPointUI.SetIcon(kMapIcons.MapCar);
                }

                // уникальное оборудование
                gcUniqEquipment.DataSource = GetUniqList();
                // неуникальное оборудование
                gcNonUniqEquipment.DataSource = GetNonUniqList();
                // движение
                gcMovement.DataSource = DalContainer.WcfDataManager.ServiceOperationClient.GetMovementListByTripId(Trip.Id);
            }
            catch (Exception ex)
            {
                Log.Add("Не отрисован трек или не заполнена одна из таблиц");
            }
        }

        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            DrawCarTrack();
        }

        #region Для таблиц

        /// <summary>
        /// Возвращает список неуникального оборудования
        /// </summary>
        /// <param name="isStart">Выезд или приезд</param>
        /// <returns>Список оборудования</returns>
        private List<Equipment> GetNonUniqList()
        {
            var list = new List<Equipment>();
            var beginList = DalContainer.WcfDataManager.ServiceOperationClient.GetСomplectationListByUnitId(Unit.Id).Where(eq => !eq.IsUniq).ToList();

            var helplist = beginList.Select(v => v.kEquipmentId).Distinct().ToList();
            foreach (var type in helplist)
            {
                var wr = beginList.Where(eq => eq.kEquipmentId == type).ToList();
                wr[0].RealCount = wr.Count;
                list.Add(wr[0]);
            }

            return list;
        }

        /// <summary>
        /// Возвращает список уникального оборудования, имеющегося на конец вызова
        /// </summary>
        /// <returns>Список оборудования</returns>
        private List<Equipment> GetUniqList()
        {
            var endUniqList = DalContainer.WcfDataManager.ServiceOperationClient.GetСomplectationListByUnitId(Unit.Id).Where(eq => eq.IsUniq).ToList();
            return endUniqList;
        }

        #endregion

        #region Методы для карты

        /// <summary>
        /// Метод подготовки списка точек по типам для отрисовки
        /// </summary>
        private void PrepareTrackPointLists()
        {
            var addressPointsList = GetGeoPointsList();
            if (!addressPointsList.Any())
                return;

            var beforePoint = addressPointsList.FirstOrDefault();
            for (var i = 1; i < addressPointsList.Count; i++)
            {
                var addresPoint = addressPointsList[i];

                if (beforePoint != null)
                {
                    _linePointList.Add(
                        new System.Windows.Point(beforePoint.Longitude, beforePoint.Latitude));
                    _linePointList.Add(new System.Windows.Point(addresPoint.Longitude, addresPoint.Latitude));
                }

                beforePoint = addresPoint;
            }
        }

        /// <summary>
        /// Выгрузка списка гео-точек поездки
        /// </summary>
        /// <returns>Список гео-точек поездки</returns>
        private List<GeoPoints> GetGeoPointsList()
        {
            var addresPointList = new List<GeoPoints>();
            var idFrom = 0;
            var diff = 200;
            var partList = new List<GeoPoints>();

            try
            {
                do
                {
                    partList = DalContainer.WcfDataManager.ServiceOperationClient.GetGeoPointListByTripId(Trip.Id, idFrom, diff).ToList();
                    addresPointList.AddRange(partList);

                    if (partList.Any())
                        idFrom = partList.Last().Id + 1;
                }
                while (partList.Any());
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception("Возникли проблемы при загрузке списка гео-точек поездки", ex));
            }

            return addresPointList;
        }

        /// <summary>
        /// Отдаление /приближение карты мышью
        /// </summary>
        /// <param name="sender"> Отправитель </param>
        /// <param name="e"> Аргументы </param>
        private void MapMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                _map.Focus();
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception("Возникли проблемы при фокусировке карты", ex));
            }
        }

        /// <summary>
        /// Метод изменения размера контрола, в котором хранится карта
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Аргументы</param>
        private void HousesInMapResize(object sender, EventArgs e)
        {
            try
            {
                UpdateMapLayout();
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception("Ошибка загрузки окна карты поездки", ex));
            }
        }

        /// <summary>
        /// Метод обновления контрола карты
        /// </summary>
        private void UpdateMapLayout()
        {
            if (_map != null)
            {
                _map.Width = mapHost.Width;
                _map.Height = mapHost.Height;
            }
        }

        #endregion

        private void sbEditStoreBoundary_Click(object sender, EventArgs e)
        {
            fmStoreBoundariesOnMap.Execute(Store);
        }

    }
}
