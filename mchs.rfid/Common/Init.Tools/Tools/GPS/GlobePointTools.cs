// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobePointTools.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Объект вычисляющий расстояние между двумя точками на поверхности сферы
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Init.Tools.GPS
{
    /// <summary>
    /// Объект вычисляющий расстояние между двумя точками на поверхности сферы
    /// </summary>
    public static class GlobePointTools
    {
        /// <summary>
        /// Вычислить расстояние между двумя точками на сфере
        /// </summary>
        /// <param name="lon1">
        /// Долгота первой точки 
        /// </param>
        /// <param name="lat1">
        /// The lat 1.
        /// </param>
        /// <param name="lon2">
        /// Долгота второй точки 
        /// </param>
        /// <param name="lat2">
        /// Широта второй точки 
        /// </param>
        /// <returns>
        /// Расстояние между двумя точками (в километрах) 
        /// </returns>
        public static double GetDistanceBetweenPoints(double lon1, double lat1, double lon2, double lat2)
        {
            const double MIN_DISTANCE_BETWEEN_POINTS = 0.000001;
            if ((Math.Abs(lon1 - lon2) < MIN_DISTANCE_BETWEEN_POINTS) && Math.Abs(lat1 - lat2) < MIN_DISTANCE_BETWEEN_POINTS)
                return 0.0;

            // Для вычисления расстояния между точками на поверхности земного шара можно использовать формулу, известную в сферической геометрии и геодезии:
            // S = 111,2×arccos(sin φ1 × sin φ2 + cos φ1 × cos φ2 × cos (L2-L1)),
            // где S - расстояние, км;
            // φ1 и L1 - широта и долгота точки 1 (для северной широты и восточной долготы со знаком плюс, для южной широты и западной долготы со знаком минус), градусы;
            // φ2 и L2 - широта и долгота точки 2, градусы;
            // константа 111,2 - средняя длина дуги в один градус на поверхности Земли, км.
            double arccosArg = Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(lon2 - lon1);
            double arccosVal = Math.Acos(arccosArg);
            double z = Math.Round(40075.676 / 360.0 * arccosVal, 3);
            return z;
        }
    }
}
