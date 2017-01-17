// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CityConvexPolygonUi.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Класс, управляющий отрисовкой и поведением выпуклого многоугольника отображающего границы города
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ServerManager.Maps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Media;

    using global::Maps.Base;
    using global::Maps.Item;

    using Point = System.Windows.Point;
    using PointLocalisation;

    /// <summary>
    /// Класс, управляющий отрисовкой и поведением выпуклого многоугольника отображающего границы города
    /// </summary>
    public class CityConvexPolygonUi
    {
        /// <summary>
        /// Конструктор класса управляющего отображением выпуклого многоугольника
        /// </summary>
        /// <param name="mapDesigner"> Карта, на которой отрисовывается многоугольник </param>
        /// <param name="pointList"> Список точек для отрисови многоугольника </param>
        /// <param name="trackColor"> Цвет соединительных линий между вершинами </param>
        public CityConvexPolygonUi(MapDesigner mapDesigner, List<Point> pointList, Color trackColor)
        {
            if (trackColor == null)
                throw new ArgumentNullException("trackColor");
            if (pointList == null)
                throw new ArgumentNullException("pointList");
            if (mapDesigner == null)
                throw new ArgumentNullException("mapDesigner");

            _map = mapDesigner;
            _pointList = pointList;
            _trackColor = trackColor;
            CityBoundaryUIList = new List<CityBoundaryUI>();

            CreatePolygon();
        }

        #region Поля и свойства
        /// <summary>
        /// Карта, на которой отрисовывается многоугольник
        /// </summary>
        private MapDesigner _map;

        /// <summary>
        /// Цвет соединительных линий между вершинами
        /// </summary>
        private Color _trackColor;

        /// <summary>
        /// Список точек для отрисови многоугольника
        /// </summary>
        private List<Point> _pointList;

        /// <summary>
        /// Соединительная линия между вершинами многоугольника
        /// </summary>
        private LineUI _lineUI;

        /// <summary>
        /// Список отображаемых на карте границ
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public List<CityBoundaryUI> CityBoundaryUIList { get; private set; }
        #endregion

        #region События

        /// <summary>
        /// Событие выделения многоугольника
        /// </summary>
        public event Action<CityConvexPolygonUi> PolygonClick;

        /// <summary>
        /// Обработчик события выделения многоугольника
        /// </summary>
        /// <param name="currentConvexPolygonUi"> Текущий выделенный многоугольник </param>
        private void OnPolygonClick(CityConvexPolygonUi currentConvexPolygonUi)
        {
            var handler = PolygonClick;
            if (handler != null)
                handler(currentConvexPolygonUi);
        }

        #endregion

        #region Методы
        /// <summary>
        /// Метод создания многоугольника
        /// </summary>
        public void CreatePolygon()
        {
            if (!_pointList.Any())
                CreateNewPolygonPoints();

            // добавляем элементы отображения вершин границ города
            foreach (var point in this._pointList)
                AddCityBoundary(point);

            var cityBoundariesList = new List<Point>();

            // дублируем первую точку для образования замкнутого кольца
            cityBoundariesList.AddRange(_pointList);
            cityBoundariesList.Add(_pointList[0]);

            // отрисовка линий между вершинами
            TrackBuilder(cityBoundariesList);
        }

        /// <summary>
        /// Метод записи координат выставленных на карте точек многоугольника в строку
        /// </summary>
        /// <returns> Строка с координатами </returns>
        public string CreateStringCoordinate()
        {
            var stringCoordinate = string.Empty;
            for (int i = 0; i < CityBoundaryUIList.Count; i++)
            {
                var coordinates = CityBoundaryUIList[i].Coordinates;
                if (coordinates != null)
                {
                    stringCoordinate += coordinates.Value.X + "; ";
                    stringCoordinate += coordinates.Value.Y;

                    if (i != CityBoundaryUIList.Count - 1)
                        stringCoordinate += "; ";
                }
            }

            return stringCoordinate;
        }

        /// <summary>
        /// Метод отрисовки линий между вершинами многоугольника границ города
        /// </summary>
        /// <param name="cityBoundaryPointList"> Закольцованный список точек (вершин многоугольника границ города) </param>
        private void TrackBuilder(List<Point> cityBoundaryPointList)
        {
            _lineUI = new LineUI(cityBoundaryPointList.ToList(), _map, _trackColor);
        }

        /// <summary>
        /// Перерисовать цвет границ многоугольника с выделен/не выделен
        /// </summary>
        /// <param name="check"> Выделен/Не выделен многоугольник </param>
        public void Checked(bool check)
        {
            _lineUI.MarkTrackAsChecked(check);
        }

        /// <summary>
        /// Метод задания координат нового многоугольника (без заданных координат)
        /// </summary>
        private void CreateNewPolygonPoints()
        {
            // создаем 3 точки для многоугольника на основе текущего положения карты
            var longitudePixel1 = _map.StartPoint.X + _map.Width * 0.1;
            var latitudePixel1 = _map.StartPoint.Y + _map.Height * 0.1;
            var point1 = _map.MapController.GetCoordinates(longitudePixel1, latitudePixel1);
            _pointList.Add(point1);

            var longitudePixel2 = _map.StartPoint.X + _map.Width / 2;
            var latitudePixel2 = _map.StartPoint.Y + _map.Height * 0.9;
            var point2 = _map.MapController.GetCoordinates(longitudePixel2, latitudePixel2);
            _pointList.Add(point2);

            var longitudePixel3 = _map.StartPoint.X + _map.Width * 0.9;
            var latitudePixel3 = _map.StartPoint.Y + _map.Height * 0.1;
            var point3 = _map.MapController.GetCoordinates(longitudePixel3, latitudePixel3);
            _pointList.Add(point3);
        }

        /// <summary>
        /// Добавляет вершину границы на карту по координате
        /// </summary>
        /// <param name="cityBoundary"> Точка границы  </param>
        /// <param name="isBasePolygon"> Флаг, указывающий базовый ли многоугольник добавляется </param>
        /// <returns> Объект CityBoundaryUI  </returns>
        public CityBoundaryUI AddCityBoundary(Point cityBoundary, bool isBasePolygon = true)
        {
            // создаем новую вершину с учетом координат
            var cityBoundaryUI = new CityBoundaryUI(cityBoundary, _map);
            cityBoundaryUI.MovedUI += RefreshLineUI;
            cityBoundaryUI.LeftMouseUp += FocusedOnCityBoundary;

            if (isBasePolygon)
                CityBoundaryUIList.Add(cityBoundaryUI);

            // выделяем добавленную вершину
            FocusedOnCityBoundary(cityBoundaryUI);
            return cityBoundaryUI;
        }

        /// <summary>
        /// Добавляет вершину границы на карту (между выделенной и предыдущей вершиной)
        /// </summary>
        /// <returns> Объект CityBoundaryUI </returns>
        public CityBoundaryUI AddCityBoundary()
        {
            // получаем выделенную вершину и находим ее индекс в списке вершин
            var focusedCityBoundaryUI = CityBoundaryUIList.SingleOrDefault(cb => cb.IsFocusedUI);
            var focusedBoundaryIndex = CityBoundaryUIList.IndexOf(focusedCityBoundaryUI);
            int forwardBoundaryIndex;

            // находим индекс следующей вершины
            if (focusedBoundaryIndex == CityBoundaryUIList.Count - 1)
                forwardBoundaryIndex = 0;
            else
                forwardBoundaryIndex = focusedBoundaryIndex + 1;

            // определяем координаты середины отрезка
            var cityPointCoordinate = new Point();
            var focusedPointCoordinates = CityBoundaryUIList[focusedBoundaryIndex].Coordinates;
            if (focusedPointCoordinates != null)
            {
                var previousPointCoordinates = CityBoundaryUIList[forwardBoundaryIndex].Coordinates;
                if (previousPointCoordinates != null)
                {
                    var pointCoordinate = new Point
                                                  {
                                                      X = (focusedPointCoordinates.Value.X + previousPointCoordinates.Value.X) / 2,
                                                      Y = (focusedPointCoordinates.Value.Y + previousPointCoordinates.Value.Y) / 2
                                                  };
                    cityPointCoordinate = pointCoordinate;
                }
            }

            // добавляем объект по найденным координатам на карту
            var newCityBoundary = AddCityBoundary(cityPointCoordinate, false);

            // формируем список вершин многоугольника по порядку с учетом добавленной
            var updatedCityBoundaryUiList = new List<CityBoundaryUI>();
            for (var i = 0; i < CityBoundaryUIList.Count; i++)
            {
                // добавляем старые вершины до выделенной
                updatedCityBoundaryUiList.Add(CityBoundaryUIList[i]);

                // добавляем после выделенного элемента новую вершину
                if (i == focusedBoundaryIndex)
                    updatedCityBoundaryUiList.Add(newCityBoundary);
            }

            // обновляем список вершин на только что сформированный
            CityBoundaryUIList = updatedCityBoundaryUiList;

            // перерисовываем многоугольник
            RefreshLineUI();

            // фокусируемся на добавленной вершине
            FocusedOnCityBoundary(newCityBoundary);

            return newCityBoundary;
        }

        /// <summary>
        /// Метод изменения фокусировки на элементе границы
        /// </summary>
        /// <param name="cityBoundaryUI"> Выделяемый элемент </param>
        private void FocusedOnCityBoundary(CityBoundaryUI cityBoundaryUI)
        {
            foreach (var boundaryUI in CityBoundaryUIList.Where(boundaryUI => boundaryUI.IsFocusedUI))
            {
                boundaryUI.IsFocusedUI = false;
                boundaryUI.SetIconForFocus();
            }

            cityBoundaryUI.IsFocusedUI = true;
            cityBoundaryUI.SetIconForFocus();
            OnPolygonClick(this);
        }

        /// <summary>
        /// Метод удаления выделенной границы города
        /// </summary>
        public void DeleteFocusedBoundaryUI()
        {
            // получаем выделенную вершину и находим ее индекс в списке вершин
            var focusedCityBoundaryUI = CityBoundaryUIList.SingleOrDefault(cb => cb.IsFocusedUI);
            var index = CityBoundaryUIList.IndexOf(focusedCityBoundaryUI);

            // отписываемся от событий
            if (focusedCityBoundaryUI != null)
            {
                focusedCityBoundaryUI.MovedUI -= RefreshLineUI;
                focusedCityBoundaryUI.LeftMouseUp -= FocusedOnCityBoundary;

                // удаляем объект представляющий границу
                CityBoundaryUIList.Remove(focusedCityBoundaryUI);
                _map.Children.Remove(focusedCityBoundaryUI);
            }

            // обновляем очертания многоугольника
            RefreshLineUI();

            // выделяем предыдущую вершину многоугольника, если вершина была первой, то выделяем последнюю
            if (index == 0)
                index = CityBoundaryUIList.Count - 1;
            else
                index -= 1;

            FocusedOnCityBoundary(CityBoundaryUIList[index]);
        }

        /// <summary>
        /// Метод удаления всех границ города в данном многоугольнике
        /// </summary>
        public void DeleteAllCityBoundaries()
        {
            var deletingIndex = CityBoundaryUIList.Count - 1;
            while (deletingIndex >= 0)
            {
                // отписываемся от событий
                CityBoundaryUIList[deletingIndex].LeftMouseUp -= FocusedOnCityBoundary;
                CityBoundaryUIList[deletingIndex].MovedUI -= RefreshLineUI;

                // удаляем объект представляющий границу
                _map.Children.Remove(CityBoundaryUIList[deletingIndex]);
                CityBoundaryUIList.Remove(CityBoundaryUIList[deletingIndex]);
               
                deletingIndex -= 1;
            }

            // удаляем очертания границ
            _map.Children.Remove(_lineUI.Path);
            _map.Children.Remove(_lineUI);
        }

        /// <summary>
        /// Метод перерисовки границ города после перетаскивания
        /// </summary>
        public void RefreshLineUI()
        {
            var pointList = new List<Point>();
            if (CityBoundaryUIList.Count > 1)
            {
                lock (CityBoundaryUIList)
                {
                    foreach (var cityBoundaryUI in CityBoundaryUIList)
                        if (cityBoundaryUI.Coordinates != null)
                            pointList.Add(
                                new Point(cityBoundaryUI.Coordinates.Value.X, cityBoundaryUI.Coordinates.Value.Y));

                    // замыкаем границы в кольцо дублируя первую точку
                    pointList.Add(pointList[0]);
                }
            }

            // перерисовываем границы по обновленным координатам
            _lineUI.RefreshPoints(pointList);
        }

        /// <summary>
        /// Метод проверки выпуклый многоугольник или нет
        /// </summary>
        /// <returns> True - если выпуклый, в противном случае false </returns>
        public bool CheckConvexPolygon()
        {
            var convexPointList = new List<PointLocalisation.Point>();
            foreach (var cityBoundary in CityBoundaryUIList)
                if (cityBoundary.Coordinates != null)
                    convexPointList.Add(new PointLocalisation.Point(cityBoundary.Coordinates.Value.X, cityBoundary.Coordinates.Value.Y));

            if (!Localisation.CheckConvexityOfPolygon(convexPointList))
                return false;

            return true;
        }

        /// <summary>
        /// Метод проверки количества вершин многоугольника
        /// </summary>
        /// <returns> Пройдена ли проверка </returns>
        public bool CheckTopCount()
        {
            if (CityBoundaryUIList.Count >= 3)
                return true;
            return false;
        }

        #endregion
    }
}
