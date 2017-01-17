// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Localisation.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Локализация точки в выпуклом многоугольнике
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PointLocalisation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Локализация точки в выпуклом многоугольнике
    /// </summary>
    public class Localisation
    {
        /// <summary>
        /// Вычисление направления поворота вектор AB в точку C
        /// </summary>
        /// <param name="pointA"> Точка A </param>
        /// <param name="pointB"> Точка B </param>
        /// <param name="pointC"> Точка C </param>
        /// <returns>
        /// Направление поворота
        /// (больше 0 - левый, меньше 0 - правый, равно нулю - точки лежат на одной прямой)
        /// </returns>
        public static double Rotate(Point pointA, Point pointB, Point pointC)
        {
            return (pointB.X - pointA.X) * (pointC.Y - pointB.Y) - (pointB.Y - pointA.Y) * (pointC.X - pointB.X);
        }

        /// <summary>
        /// Определение пересечения отрезков AB и CD
        /// </summary>
        /// <param name="pointA"> Точка A </param>
        /// <param name="pointB"> Точка B </param>
        /// <param name="pointC"> Точка C </param>
        /// <param name="pointD"> Точка D </param>
        /// <returns> True - если пересекаются, иначе false </returns>
        private static bool IsIntersect(Point pointA, Point pointB, Point pointC, Point pointD)
        {
            return Rotate(pointA, pointB, pointC) * Rotate(pointA, pointB, pointD) <= 0
                   && Rotate(pointC, pointD, pointA) * Rotate(pointC, pointD, pointB) < 0;
        }

        /// <summary>
        /// Локализация точки в выпуклом многоугольнике
        /// (обход точек против часовой стрелки)
        /// </summary>
        /// <param name="coordinates"> Координаты вершин выпуклого многоугольника </param>
        /// <param name="point"> Локализуемая точка </param>
        /// <returns> True - если точка принадлежит многоугольнику, иначе false </returns>
        public static bool IsPointLoc(Point[] coordinates, Point point)
        {
            if (coordinates == null)
                throw new ArgumentNullException("coordinates");
            if (point == null)
                throw new ArgumentNullException("point");
            if (coordinates.Length < 3)
                throw new ArithmeticException("Недостаточное количество вершин для построения выпуклого многоугольника");

            var n = coordinates.Length;
            if (Rotate(coordinates[0], coordinates[1], point) < 0
                || Rotate(coordinates[0], coordinates[n - 1], point) > 0)
                return false;

            // Запускаем двоичный поиск
            var l = 1;
            var r = n - 1;
            while (r - l > 1)
            {
                var q = (l + r) / 2;
                if (Rotate(coordinates[0], coordinates[q], point) < 0)
                    r = q;
                else
                    l = q;
            }

            return !IsIntersect(coordinates[0], point, coordinates[l], coordinates[r]);
        }

        /// <summary>
        /// Метод проверки многоугольника на выпуклость
        /// </summary>
        /// <param name="pointList"> Список точек многоугольника </param>
        /// <returns> Выпуклый многоугольник или нет </returns>
        public static bool CheckConvexityOfPolygon(List<Point> pointList)
        {
            if (pointList == null)
                throw new ArgumentNullException("pointList");

            return pointList.Count >= 3 && pointList.All(point => Steep(point, pointList));
        }


        /// <summary>
        /// Проверка, расположена ли точка по одну сторону от сторон многоугольника 
        /// </summary>
        /// <param name="point"> Проверяемя точка </param>
        /// <param name="polygon"> Многоугольник </param>
        /// <returns> Результат проверки</returns>
        private static bool Steep(Point point, List<Point> polygon)
        {
            var prevRotation = 0;
            for (var i = 1; i < polygon.Count; i++)
            {
                var rotation = GetRotateDir(polygon[i - 1], polygon[i], point);

                if (rotation == 0)
                    continue;

                if (rotation == prevRotation)
                    continue;
                if (prevRotation != 0)
                    return false;
                prevRotation = rotation;
            }

            return true;
        }


        /// <summary>
        /// Определение, с какой стороны находится точка С относительно отрезка АВ
        /// </summary>
        /// <param name="pointA"> Точка А </param>
        /// <param name="pointB"> Точка В </param>
        /// <param name="pointC"> Точка С </param>
        /// <returns> 1 - слева, -1 - справа, 0 - точки А, В и С лежат на одной прямой </returns>
        private static int GetRotateDir(Point pointA, Point pointB, Point pointC)
        {
            var rotate = Rotate(pointA, pointB, pointC);
            if (Math.Abs(rotate) < double.Epsilon)
                return 0;
            if (rotate > 0)
                return 1;
            return -1;
        }
    }
}