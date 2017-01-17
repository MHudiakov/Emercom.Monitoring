// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerManagerMap.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Defines the HousesMap type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Client.UI.Map
{
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using global::Maps.Base;
    using DAL.WCF;

    /// <summary>
    /// Класс описывающий карту и объекты на ней
    /// </summary>
    public class ServerManagerMap : MapDesigner
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ServerManagerMap"/>.
        /// </summary>
        public ServerManagerMap()
            : base(null)
        {
            UpdateMapSettings();
        }

        /// <summary>
        /// Метод, который обновляет настройки отображения карты
        /// </summary>
        public static void UpdateMapSettings()
        {
            var store = DalContainer.WcfDataManager.ServiceOperationClient.GetAllStore()[0];
            MapSettings = new MapSettings
            {
                CityCenterLat = store.Latitude,
                CityCenterLon = store.Longitude//,
                //DefaultZoomLevel = 15//,
                // CalculateCoordinatesSource = s.CalculateCoordinatesSource,
                // CityBoundaries = s.CityBoundaries,
                // CalculateAddressCoordinatesSource = s.MapCalculateAddressCoordinatesSource,
                // CalculateDistanceSource = s.MapCalculateDistanceSource,
                // TileSampleURL = s.MapTileSampleURL,
                // TileCashPrefix = s.MapTileCashPrefix,
                // IsUseEllips = s.MapIsUseEllips
            };
        }

        /// <summary>
        /// По координатам списка точек находятся координаты левого верхнего и правого нижнего углов, и по этим точкам производится фокусировка карты
        /// </summary>
        /// <param name="pointList"> Список точек, на которых необходимо сфокусироваться </param>
        public void FocusMapOnPointList(List<Point> pointList)
        {
            if (pointList == null)
                throw new ArgumentNullException("pointList");

            // перегоняем координаты всех точек трека в список, чтобы найти оптимальную фокусировку
            var analysingPointList = pointList.ToList();

            if (!analysingPointList.Any())
                return;

            // ищем координаты левого верхнего угла и правого нижнего по координатам
            var rightBottomLat = analysingPointList[0].Y;
            var rightBottomLon = analysingPointList[0].X;
            var leftTopLat = analysingPointList[0].Y;
            var leftTopLon = analysingPointList[0].X;
            for (int i = 1; i < analysingPointList.Count; i++)
            {
                if (analysingPointList[i].Y > leftTopLat)
                    leftTopLat = analysingPointList[i].Y;
                if (analysingPointList[i].X > rightBottomLon)
                    rightBottomLon = analysingPointList[i].X;
                if (analysingPointList[i].Y < rightBottomLat)
                    rightBottomLat = analysingPointList[i].Y;
                if (analysingPointList[i].X < leftTopLon)
                    leftTopLon = analysingPointList[i].X;
            }

            var leftTopPoint = new Point(leftTopLon, leftTopLat);
            var rigthBottomPoint = new Point(rightBottomLon, rightBottomLat);

            // фокусируемся на найденных координатах
            if (leftTopPoint == rigthBottomPoint)
                this.FocusTo(leftTopPoint);
            else
                this.FocusTo(new Point(leftTopPoint.X, leftTopPoint.Y), new Point(rigthBottomPoint.X, rigthBottomPoint.Y));
        }
    }
}

