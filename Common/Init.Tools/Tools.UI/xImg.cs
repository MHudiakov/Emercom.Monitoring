// --------------------------------------------------------------------------------------------------------------------
// <copyright file="xImg.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Helper для работы с изображениями
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.UI
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    /// <summary>
    /// Helper для работы с изображениями
    /// </summary>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here.")]
    // ReSharper disable once InconsistentNaming
    public static class xImg
    {
        /// <summary>
        /// Преобразовать в массив  байт
        /// </summary>
        /// <param name="image">
        /// Изображение
        /// </param>
        /// <returns>
        /// Массив байт
        /// </returns>
        public static byte[] ToByteArray(this Image image)
        {
            var stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            return stream.ToArray();
        }

        /// <summary>
        /// Преобразовать из массива байт
        /// </summary>
        /// <param name="byteArray">
        /// Массив байт
        /// </param>
        /// <returns>
        /// Изображение  
        /// </returns>
        public static Image ToImage(this byte[] byteArray)
        {
            return new Bitmap(new MemoryStream(byteArray));
        }

        /// <summary>
        /// Получение изображения из файла и изменение его размера
        /// </summary>
        /// <param name="path">
        /// Путь к изображению
        /// </param>
        /// <param name="size">
        /// Новые размеры
        /// </param>
        /// <returns>
        /// Изображение с указанными размерами
        /// </returns>
        public static Image GetAndResize(string path, Size size)
        {
            Image img = Image.FromFile(path);
            var origImg = (Bitmap)img.Clone();
            Size resizedDimensions = GetDimensions(size.Width, size.Height, ref origImg);
            var newImg = new Bitmap(origImg, resizedDimensions);
            return newImg;
        }

        /// <summary>
        /// Получить новый пропорциональный размер для изображения
        /// </summary>
        /// <param name="maxWidth">
        /// Максимальная ширина
        /// </param>
        /// <param name="maxHeight">
        /// Максимальная высота
        /// </param>
        /// <param name="img">
        /// Оригинальное изображение
        /// </param>
        /// <returns>
        /// Новый пропорциальный размер
        /// </returns>
        public static Size GetDimensions(int maxWidth, int maxHeight, ref Bitmap img)
        {
            int height = img.Height;
            int width = img.Width;

            // if (Height == MaxHeight && Width == MaxWidth)
            if (height <= maxHeight && width <= maxWidth)
                return new Size(width, height);

            var multiplier = (double)maxWidth / width;

            if (Math.Abs((height * multiplier) - maxHeight) < double.MinValue)
            {
                height = (int)(height * multiplier);
                return new Size(maxWidth, height);
            }

            multiplier = (double)maxHeight / height;
            width = (int)(width * multiplier);

            return new Size(width, maxHeight);
        }
    }
}
