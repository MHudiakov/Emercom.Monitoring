// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConvexPolygonMap.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Класс с картографией для работы с многоугольниками границ города
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ServerManager.Maps
{
    using Client.UI.Map;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Класс с картографией для работы с многоугольниками границ города
    /// </summary>
    public class ConvexPolygonMap : ServerManagerMap
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ConvexPolygonMap"/>.
        /// </summary>
        public ConvexPolygonMap()
        {
            CityConvexPolygonUIList = new List<CityConvexPolygonUi>();
        }

        /// <summary>
        /// Список всех возможных цветов линий многоугольников
        /// </summary>
        private List<Color> _allTrackColorList;

        /// <summary>
        /// Список отображаемых на карте границ
        /// </summary>
        public List<CityConvexPolygonUi> CityConvexPolygonUIList { get; set; }

        /// <summary>
        /// Индекс последнего использованного для отрисовки многоугольников цвета
        /// </summary>
        private int _lastUsedColorIndex;

        /// <summary>
        /// Добавляет многоугольник, отображающий границы города на карте
        /// </summary>
        /// <param name="convexPointList"> Список координат вершин многоугольника  </param>
        /// <returns> Добавленный многоугольник </returns>
        public CityConvexPolygonUi AddCityBoundaryPolygon(List<Point> convexPointList)
        {
            if (_allTrackColorList == null)
                _allTrackColorList = this.GetAllTrackColors();
            var convex = new CityConvexPolygonUi(this, convexPointList, _allTrackColorList[_lastUsedColorIndex]);
            CityConvexPolygonUIList.Add(convex);
            _lastUsedColorIndex += 1;
            return convex;
        }

        /// <summary>
        /// Удаляет многоугольник, отображающий границы города на карте
        /// </summary>
        /// <param name="cityConvexPolygonUi"> Удаляемый многоугольник </param>
        public void DeleteCityBoundaryPolygon(CityConvexPolygonUi cityConvexPolygonUi)
        {
            cityConvexPolygonUi.DeleteAllCityBoundaries();
            CityConvexPolygonUIList.Remove(cityConvexPolygonUi);
        }

        /// <summary>
        /// Метод получения цветов для треков
        /// </summary>
        /// <returns> Список цветов </returns>
        private List<Color> GetAllTrackColors()
        {
            var colorList = new List<Color>();
            var colorType = typeof(System.Drawing.Color);
            var proInfos = colorType.GetProperties(BindingFlags.Static | BindingFlags.Public);

            // выбираем цвета
            foreach (var proInfo in proInfos)
            {
                var convertFromString = ColorConverter.ConvertFromString(proInfo.Name);
                if (convertFromString != null)
                    colorList.Add((Color)convertFromString);
            }

            // отметаем прозрачные цвета и те, которые по тону близки к белому
            return colorList.Where(cl => cl.A > 200 && cl.G <= 230 && cl.R <= 230 && cl.B <= 230).ToList();
        }
    }
}
