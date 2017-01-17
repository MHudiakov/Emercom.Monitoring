// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Point.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Представляет точку в двумерном Евклидовом пространстве
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PointLocalisation
{
    /// <summary>
    /// Представляет точку в двумерном Евклидовом пространстве
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Point"/>.
        /// </summary>
        /// <param name="x">
        /// Координата абсциссы точки
        /// </param>
        /// <param name="y">
        /// Координата ординаты точки
        /// </param>
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Point"/>.
        /// </summary>
        public Point()
        {
        }

        /// <summary>
        /// Координата абсциссы точки
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Координата ординаты точки
        /// </summary>
        public double Y { get; set; }
    }
}
