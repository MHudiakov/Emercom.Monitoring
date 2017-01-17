// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CityBoundaryCoordinateParser.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Класс преобразующий строку координат в список точек с координатами и наоборот
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PointLocalisation
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Класс преобразующий строку координат в списки точек с координатами
    /// </summary>
    public class StoreBoundaryCoordinateParser
    {
        /// <summary>
        /// Метод получения из строки с координатами списков точек с координатами
        /// </summary>
        /// <param name="coordinatesString"> Строка с координатами </param>
        /// <returns> Списки точек с координатами </returns>
        public static List<List<Point>> ParseStringCoordinates(string coordinatesString)
        {
            // списки точек с координатами
            var coordinatesForPolygonsList = new List<List<Point>>();

            coordinatesString = coordinatesString.Trim(new[] { '[', ']' });

            var polygonCoordinateStringsList = coordinatesString.Split('|').ToList();

            foreach (var polygonCoordinateString in polygonCoordinateStringsList)
            {
                var coordinatesList = new List<double>();
                var cityBoundariesPointList = new List<Point>();

                var coordinateStringList = polygonCoordinateString.Split(';').ToList();

                // формирование списка координат из строчного представления в Double
                foreach (var coordinate in coordinateStringList)
                {
                    var currentCoordinate = coordinate.Trim();
                    double resultCoordinate;
                    if (!double.TryParse(currentCoordinate, out resultCoordinate))
                        continue;
                    coordinatesList.Add(resultCoordinate);
                }

                // формирование из массива Double точек с координатами
                for (var i = 0; i < coordinatesList.Count; i = i + 2)
                    cityBoundariesPointList.Add(new Point(coordinatesList[i], coordinatesList[i + 1]));

                coordinatesForPolygonsList.Add(cityBoundariesPointList);
            }

            return coordinatesForPolygonsList;
        }
    }
}
