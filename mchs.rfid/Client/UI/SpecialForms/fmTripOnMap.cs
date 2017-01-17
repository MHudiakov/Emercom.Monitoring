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
using PointLocalisation;

namespace Client.UI.SpecialForms
{
    /// <summary>
    /// Карта поездки выбранной машины
    /// </summary>
    public partial class fmTripOnMap : XtraForm
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public fmTripOnMap()
        {
            InitializeComponent();
        }

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
        /// Создание формы списка движения
        /// </summary>
        public static bool Execute(Trip trip)
        {
            using (var fm = new fmTripOnMap())
            {
                fm.Trip = trip;
                return fm.ShowDialog() == DialogResult.OK;
            }
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void fmTripOnMap_Load(object sender, EventArgs e)
        {
            try
            {
                // уникальное оборудование
                gcUniqEquipmentBegin.DataSource = DalContainer.WcfDataManager.ServiceOperationClient.GetComplectationByTripId(Trip.Id, true).Where(eq => eq.IsUniq).ToList();
                gcUniqEquipmentEnd.DataSource = GetEndUniqList();

                // неуникальное оборудование
                gcNonUniqEquipmentBegin.DataSource = GetNonUniqList(true);
                gcNonUniqEquipmentEnd.DataSource = GetNonUniqList(false);

                // движение
                gcMovement.DataSource = DalContainer.WcfDataManager.ServiceOperationClient.GetMovementListByTripId(Trip.Id);
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception("Возникли проблемы при загрузке комплектации вызова с ID = " + Trip.Id, ex));
            }

            // отрисовка карты
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
                var store = DalContainer.WcfDataManager.ServiceOperationClient.GetAllStore()[0];
                var addressPointUI = new MapObjectUI<Store>(store, store.Longitude, store.Latitude, _map);
                addressPointUI.SetIconMargin(-25, -37, 25, 37);
                addressPointUI.SetIcon(kMapIcons.Store);

                var coordinatesForPolygonsList = StoreBoundaryCoordinateParser.ParseStringCoordinates(store.StoreBoundaries);

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

                if (Trip == null)
                    return;

                // выгрузка списка точек
                var addresPointList = GetGeoPointsList();

                // подготавливаем точки для отрисовки треков 
                PrepareTrackPointLists();

                // отрисовываем треки заказа
                _traceLine = new LineUI(_linePointList, _map, Color.FromArgb(200, 150, 80, 255));
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception("Возникли проблемы при отрисовке линий поездки на карте вызова с ID = " + Trip.Id, ex));
            }
        }

        /// <summary>
        /// Возвращает список неуникального оборудования
        /// </summary>
        /// <param name="isStart">Выезд или приезд</param>
        /// <returns>Список оборудования</returns>
        private List<Equipment> GetNonUniqList(bool isStart)
        {
            var beginList = new List<Equipment>();

            var l = DalContainer.WcfDataManager.ServiceOperationClient.GetComplectationByTripId(Trip.Id, isStart).Where(eq => !eq.IsUniq).ToList();
            var helplist = l.Select(v => v.kEquipmentId).Distinct().ToList();
            foreach (var type in helplist)
            {
                var wr = l.Where(eq => eq.kEquipmentId == type).ToList();
                wr[0].RealCount = wr.Count;
                beginList.Add(wr[0]);
            }

            return beginList;
        }

        /// <summary>
        /// Возвращает список уникального оборудования, имеющегося на конец вызова
        /// </summary>
        /// <returns>Список оборудования</returns>
        private List<Equipment> GetEndUniqList()
        {
            var beginUniqList = DalContainer.WcfDataManager.ServiceOperationClient.GetComplectationByTripId(Trip.Id, true).Where(eq => eq.IsUniq).ToList();
            var endUniqList = DalContainer.WcfDataManager.ServiceOperationClient.GetComplectationByTripId(Trip.Id, false).Where(eq => eq.IsUniq).ToList();

            foreach (var equipment in endUniqList)
            {
                if (!beginUniqList.Exists(e => e.Id == equipment.Id))
                    equipment.flag = 1;
                else
                    equipment.flag = 0;
            }

            foreach (var equipment in beginUniqList)
                if (!endUniqList.Exists(e => e.Id == equipment.Id))
                {
                    equipment.flag = 2;
                    endUniqList.Add(equipment);
                }

            return endUniqList;
        }

        #region Методы для карты

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
                    idFrom = partList.Any() ? partList.Last().Id + 1 : 0;
                    addresPointList.AddRange(partList);
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

        #endregion
    }
}