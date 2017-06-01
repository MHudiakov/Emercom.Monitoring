// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SplashScreen.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Форма заставки
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.UI.Forms
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Форма заставки
    /// </summary>
    public class SplashScreen : Form
    {
        #region Приватные поля класса

        /// <summary>
        /// Сохраняется текущий размер окна
        /// </summary>
        private const uint SWP_NOSIZE = 0x0001;

        /// <summary>
        /// Сохраняется текущая позиция окна
        /// </summary>
        private const uint SWP_NOMOVE = 0x0002;

        /// <summary>
        /// Окно становится самым верхним окном
        /// </summary>
        private const int HWND_TOPMOST = -1;

        /// <summary>
        /// Установка тени от окна
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private const int CS_DROPSHADOW = 0x00020000;

        /// <summary>
        /// Семафор доступа к коду создания экземпляра класса
        /// </summary>
        private static readonly object s_lockObject = new object();

        /// <summary>
        /// Ссылка на экземпляр текущего класса
        /// </summary>
        private static SplashScreen s_instance;

        /// <summary>
        /// Задает прозрачность объекта
        /// </summary>
        private static byte s_alphaBlend;

        #endregion

        #region Прорисовка элементов

        /// <summary>
        /// Прорисовка текста
        /// </summary>
        /// <param name="g">
        /// Поверхность рисования GDI+
        /// </param>
        /// <param name="text">
        /// Текст
        /// </param>
        /// <param name="x">
        /// Координата x левой верхней точки прямоугольника прорисовки текста
        /// </param>
        /// <param name="y">
        /// Координата y левой верхней точки прямоугольника прорисовки текста
        /// </param>
        /// <param name="font">
        /// Формат текста
        /// </param>
        /// <param name="brush">
        /// Объект, использующийся для заливки
        /// </param>
        private static void DrawText(Graphics g, string text, int x, int y, Font font, Brush brush)
        {
            int yOffset = 0;
            int xOffset = 0;

            // изменяем строку
            SizeF size = g.MeasureString(text, font, Instance.Width, StringFormat.GenericDefault);
            xOffset += Convert.ToInt32(Math.Ceiling(size.Width));
            yOffset += Convert.ToInt32(Math.Ceiling(size.Height));
            var rectangle = new RectangleF(x, y, xOffset, yOffset);
            g.DrawString(text, font, brush, rectangle, StringFormat.GenericDefault);
        }        
        #endregion 

        #region Настройки

        /// <summary>
        /// Эффект возникновения
        /// </summary>
        private bool _ifShowFadeIn;

        /// <summary>
        /// Заголовок
        /// </summary>
        private string _title = "Загрузка";

        /// <summary>
        /// Текущий заголовок
        /// </summary>
        private string _currenttitle = "Пожалуйста подождите";

        /// <summary>
        /// Название продукта
        /// </summary>
        public static string Productname = Application.ProductName;

        /// <summary>
        /// Версия продукта
        /// </summary>
        private readonly string _productversion = System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.ExecutablePath).ProductVersion +
            " (" + new FileInfo(Application.ExecutablePath).LastWriteTime + ")";
        
        /// <summary>
        /// Копирайт
        /// </summary>
        public static string Copyrights = "Все права защищены © \"ИНИТ-центр\"";

        /// <summary>
        /// Вывод оснойвной информации на форму
        /// </summary>
        /// <param name="g">
        /// Поверхность рисования GDI+
        /// </param>
        private void WriteText(Graphics g)
        {
            g.TextRenderingHint = TextRenderingHint.SystemDefault;
            var brushFont = new SolidBrush(Color.White);

            // Вывод информации            
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            DrawText(g, Productname, 25, 30, new Font(Instance.Font.FontFamily, 26, FontStyle.Bold), brushFont);
            g.TextRenderingHint = TextRenderingHint.SystemDefault;
            DrawText(g, this._productversion, 280, 72, Instance.Font, brushFont);
            DrawText(g, this._title, 25, 135, new Font(Instance.Font.FontFamily, Instance.Font.SizeInPoints, FontStyle.Bold), brushFont);
            DrawText(g, this._currenttitle, 25, 150, this.Font, brushFont);
            DrawText(g, Copyrights, 25, 185, this.Font, brushFont);
            brushFont.Dispose();
            g.Dispose();
        }

        #endregion

        #region WIN32
       
        /// <summary>
        /// Перечисление логических значений - используется в функциях User32.dll
        /// </summary>
        public enum Bool
        {
            /// <summary>
            /// Логическое "Нет"
            /// </summary>
            False = 0,

            /// <summary>
            /// Логическое "Да"
            /// </summary>
            True = 1
        }

        /// <summary>
        /// Размер плашки
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SplashSize
        {
            /// <summary>
            /// Размер по оси X
            /// </summary>
            public int Cx { get; set; }

            /// <summary>
            /// Размер по оси Y
            /// </summary>
            public int Cy { get; set; }

            /// <summary>
            /// Инициализирует новый экземпляр структуры <see cref="SplashSize"/>.
            /// </summary>
            /// <param name="cx">
            /// Размер по оси X
            /// </param>
            /// <param name="cy">
            /// Размер по оси Y
            /// </param>
            public SplashSize(int cx, int cy)
                : this()
            {
                this.Cx = cx;
                this.Cy = cy;
            }
        }

        /// <summary>
        /// Управляет плавным переходом цвета и тона, устанавливая функции сопряжения  для источниковых и принимающих точечных рисунков.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Blendfunction
        {
            /// <summary>
            /// Определяет исходную операцию плавного перехода цвета и тона.
            /// </summary>
            public byte BlendOp;

            /// <summary>
            /// Флаги структуры
            /// </summary>
            public byte BlendFlags;

            /// <summary>
            /// Устанавливает α-значение прозрачности, которое будет использоваться на всем источниковом точечном рисунке.
            /// </summary>
            public byte SourceConstantAlpha;

            /// <summary>
            /// Управляет способом, которым интерпретируются исходные и принимающие точечные рисунки 
            /// </summary>
            public byte AlphaFormat;
        }

        /// <summary>
        /// Использует параметр crKey как цвет прозрачности (функция UpdateLayeredWindow)
        /// </summary>
        public const int ULW_COLORKEY = 0x00000001;
        
        /// <summary>
        /// Флаг - использует параметр pblend как функцию перехода в функции UpdateLayeredWindow. Если режим изображения на экране - 256 цветов или меньше, действие этого значения то же самое, что и от действия флажка ULW_OPAQUE.
        /// </summary>
        public const int ULW_ALPHA = 0x00000002;

        /// <summary>
        /// Рисует многослойное непрозрачное окно (функция UpdateLayeredWindow)
        /// </summary>
        public const int ULW_OPAQUE = 0x00000004;

        /// <summary>
        /// Значение плавного перехода цвета итона
        /// </summary>
        public const byte AC_SRC_OVER = 0x00;

        /// <summary>
        /// Задает способ, которым интерпретируются исходные и принимающие точечные рисунки 
        /// </summary>
        public const byte AC_SRC_ALPHA = 0x01;

        /// <summary>
        /// Модифицирует позицию, размер, форму, содержание и светопроницаемость многослойного окна.
        /// </summary>
        /// <param name="hwnd">
        /// Дескриптор многослойного окна. Многослойное окно создается при помощи установки флажка WS_EX_LAYERED при создании окна функцией CreateWindowEx.
        /// </param>
        /// <param name="hdcDst">
        /// Дескриптор контекста устройства для экрана. 
        /// </param>
        /// <param name="pptDst">
        /// Указатель на структуру POINT, которая устанавливает новую экранную позицию многослойного окна.
        /// </param>
        /// <param name="psize">
        /// Указатель на структуру SIZE, которая устанавливает новый размер многослойного окна.
        /// </param>
        /// <param name="hdcSrc">
        /// Дескриптор DC для плоскости, которая определяет многослойное окно.
        /// </param>
        /// <param name="pprSrc">
        /// Указатель на структуру POINT, которая устанавливает местоположение слоя в контексте устройства.
        /// </param>
        /// <param name="crKey">
        /// Указатель на значение COLORREF, которое устанавливает цвет клавиши, которая будет использоваться при создании многослойного окна.
        /// </param>
        /// <param name="pblend">
        /// Указатель на структуру BLENDFUNCTION, устанавливающую значение прозрачности, которое будет использоваться при создании многослойного окна.
        /// </param>
        /// <param name="dwFlags">
        /// Этот параметр может быть одним из нижеследующих значений:
        /// ULW_ALPHA - использует параметр pblend как функцию перехода. Если режим изображения на экране - 256 цветов или меньше, действие этого значения то же самое, что и от действия флажка ULW_OPAQUE.
        /// ULW_COLORKEY - Использует параметр crKey как цвет прозрачности.
        /// ULW_OPAQUE - Рисует многослойное непрозрачное окно.
        /// </param>
        /// <returns>
        /// The <see cref="Bool"/>.
        /// Если функция завершается успешно, величина возвращаемого значения - не нуль.
        /// Если функция завершается с ошибкой, величина возвращаемого значения - нуль.
        /// </returns>
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref SplashSize psize, IntPtr hdcSrc, ref Point pprSrc, int crKey, ref Blendfunction pblend, int dwFlags);

        /// <summary>
        /// Извлекает дескриптор дисплейного контекста устройства для рабочей области заданного окна или для всего экрана.
        /// </summary>
        /// <param name="hWnd">
        /// Дескриптор окна, контекст устройства которого должен извлечься. Если это значение -  ПУСТО (NULL), GetDC извлекает контекст устройства для всего экрана.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        ///  Дескриптор контекста устройства
        /// </returns>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here."),
        DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        /// <summary>
        /// Освобождает контекст устройства для использования другими приложениями.
        /// </summary>
        /// <param name="hWnd">
        /// Дескриптор окна, контекст устройства которого должен быть освобожден.
        /// </param>
        /// <param name="hDc">
        /// Дескриптор контекста устройства, который будет освобожден.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here."),
        DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDc);

        /// <summary>
        /// Создает контекст устройства в памяти, совместимый с заданным устройством.
        /// </summary>
        /// <param name="hDc">
        /// Дескриптор существующего контекста устройства.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// Дескриптор контекста устройства  в памяти.
        /// </returns>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here."),
        DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDc);

        /// <summary>
        /// Удаляет заданный контекст устройства.
        /// </summary>
        /// <param name="hdc">
        /// Дескриптор контекста устройства.
        /// </param>
        /// <returns>
        /// The <see cref="Bool"/>.
        /// Если функция завершается успешно, величина возвращаемого значения - не ноль.
        /// Если функция завершается с ошибкой, величина возвращаемого значения - ноль.
        /// </returns>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here."),
        DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteDC(IntPtr hdc);

        /// <summary>
        /// Выбирает объект в заданный контекст устройства.
        /// </summary>
        /// <param name="hDc">
        /// Дескриптор контекста устройства.
        /// </param>
        /// <param name="hObject">
        /// Дескриптор объекта, который выбирается.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// Если выбранный объект - не регион, и функция завершается успешно, возвращаемое значение - дескриптор заменяемого  объекта. 
        /// Если выбранный объект - регион, и функция завершается успешно, возвращаемое значение - одно из ниже перечисленных значений:
        /// SIMPLEREGION -регион состоит из одиночного прямоугольника;
        /// COMPLEXREGION - регион состоит из более чем одного прямоугольника;
        /// NULLREGION - регион пустой.  
        /// </returns>
        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDc, IntPtr hObject);

        /// <summary>
        /// Удаляет логическое перо, кисть, шрифт, точечную картинку, регион или палитру, освобождая все системные ресурсы, связанные с объектом.
        /// </summary>
        /// <param name="hObject">
        /// Дескриптор логического пера, кисти, шрифта, точечной картинки, региона или палитры.
        /// </param>
        /// <returns>
        /// The <see cref="Bool"/>.
        /// Если функция завершается успешно, величина возвращаемого значения - не ноль.
        /// Если функция завершается с ошибкой, величина возвращаемого значения - ноль.
        /// </returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// Возвращает значение дескриптора окна рабочего стола. Окно рабочего стола покрывает весь экран. 
        /// Окно рабочего стола является областью, в верхней части которой рисуются все пиктограммы и другие окна.
        /// </summary>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// Дескриптор окна рабочего стола.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDesktopWindow();

        /// <summary>
        /// Меняет родительское окно указанного дочернего окна.
        /// </summary>
        /// <param name="hChild">
        /// Идентифицирует дочернее окно.
        /// </param>
        /// <param name="hParent">
        /// Идентифицирует новое родительское окно. Если значение этого параметра равно NULL, то родительским окном становится окно рабочего стола.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// В случае успеха возвращается дескриптор предыдущего родителького окна. В случае ошибки возвращается NULL.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetParent(IntPtr hChild, IntPtr hParent);

        /// <summary>
        /// Изменяет размер, позицию и Z-последовательность дочернего, выскакивающего или верхнего уровня окна.
        /// </summary>
        /// <param name="hWnd">
        /// Идентификатор окна.
        /// </param>
        /// <param name="hWndInsertAfter">
        /// Идентифицирует окно, которое предшествует установленному окну в Z-последовательности.
        /// </param>
        /// <param name="x">
        /// Устанавливает новую позицию с левой стороны окна. 
        /// </param>
        /// <param name="y">
        /// Устанавливает новую позицию верхней части окна. 
        /// </param>
        /// <param name="cx">
        /// Устанавливает новую ширину окна, в пикселях. 
        /// </param>
        /// <param name="cy">
        /// Устанавливает новую высоту окна, в пикселях. 
        /// </param>
        /// <param name="uFlags">
        /// Определяет флажки, устанавливающие размеры и позиционирование окна.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// Если функция завершилась успешно, возвращается значение отличное от нуля. Если функция потерпела неудачу, возвращаемое значение - ноль.
        /// </returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowPos", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        #endregion

        #region Реализации прозрачности

        /// <summary>
        /// Время обновления
        /// </summary>
        private readonly Timer _refreshFormTimer = new Timer();

        /// <summary>
        /// Битовая карта формы
        /// </summary>
        private Bitmap _formBitmap;

        /// <summary>
        /// Уровень непрозрачности формы.
        /// </summary>
        private byte _formOpacity;

        /// <summary>
        /// Обновление формы по таймеру
        /// </summary>
        /// <param name="sender">
        /// Объект, вызвавший событие
        /// </param>
        /// <param name="e">
        /// Дополнительная информация обработчику события
        /// </param>
        private void RefreshFormTimerTick(object sender, EventArgs e)
        {
            if (this._formBitmap != null)
                this.SetBitmap(this._formBitmap, this._formOpacity);
        }

        /// <summary>
        /// Установка битовой кары
        /// </summary>
        /// <param name="bitmap">
        /// Битовая карта
        /// </param>
        public void SetBitmap(Bitmap bitmap)
        {
            this.SetBitmap(bitmap, 255);
        }

        /// <summary>
        /// Установка битовой кары
        /// </summary>
        /// <param name="bitmap">
        /// Битовая карта
        /// </param>
        /// <param name="opacity">
        /// Уровень непрозрачности
        /// </param>
        /// <exception cref="ApplicationException">
        /// Исключение неправильного формата картинки
        /// </exception>
        public void SetBitmap(Bitmap bitmap, byte opacity)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("Картинка должна иметь 32-битный альфа-канал");

            this._formBitmap = bitmap;
            this._formOpacity = opacity;

            IntPtr screenDc = GetDC(IntPtr.Zero);
            IntPtr memDc = CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                var tempBmp = new Bitmap(bitmap);

                // Прорисовка каждого элемента управления
                foreach (Control ctrl in this.Controls)
                    ctrl.DrawToBitmap(tempBmp, ctrl.Bounds);

                // Вывод текста при доступности формы
                if (this.Bounds.Width > 0 && this.Bounds.Height > 0 && this.Visible)
                {
                    try
                    {
                        var g = Graphics.FromImage(tempBmp);
                        g.SetClip(new Rectangle(0, 0, this.Width, this.Height));        
                
                        // Пишем текст на форме
                        this.WriteText(g);                        
                    }
                    catch (Exception exception)
                    {
                        var stackFrame = new System.Diagnostics.StackFrame(true);
                        Console.WriteLine(
                            @"Ошибка: {0}, {1}, {2}, {3}",
                            this.GetType(),
                            stackFrame.GetMethod(),
                            stackFrame.GetFileLineNumber(),
                            exception.Message);
                    }
                }

                hBitmap = tempBmp.GetHbitmap(Color.FromArgb(0));
                tempBmp.Dispose();
                oldBitmap = SelectObject(memDc, hBitmap);

                var size = new SplashSize(bitmap.Width, bitmap.Height);
                var pointSource = new Point(0, 0);

                Rectangle screenArea = SystemInformation.WorkingArea;
                int nx = (screenArea.Width - this.Width) / 2;
                int ny = (screenArea.Height - this.Height) / 2;
                var topPos = new Point(nx, ny);

                var blend = new Blendfunction
                                {
                                    BlendOp = AC_SRC_OVER,
                                    BlendFlags = 0,
                                    SourceConstantAlpha = opacity,
                                    AlphaFormat = AC_SRC_ALPHA
                                };
                UpdateLayeredWindow(this.Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, ULW_ALPHA);
            }
            finally
            {
                ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    SelectObject(memDc, oldBitmap);
                    DeleteObject(hBitmap);
                }

                DeleteDC(memDc);
            }
        }

        /// <summary>
        /// Возвращает обязательные параметры создания при создании дескриптора элемента управления.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x00080000;
                return cp;
            }
        }

        #endregion

        #region Конструктор

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SplashScreen"/>.
        /// </summary>
        public SplashScreen()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            
            // задаем форму текста
            var stringFormat = new StringFormat();
            stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
            stringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

            // таймер для отрисовки окна. Вместо OnPaint
            this._refreshFormTimer.Interval = 100;
            this._refreshFormTimer.Tick += this.RefreshFormTimerTick;
            this._refreshFormTimer.Enabled = true;

            this.ShowInTaskbar = false;            
        }

        /// <summary>
        /// Экземпляр класса
        /// </summary>
        public static SplashScreen Instance
        {
            get
            {
                lock (s_lockObject)
                {
                    if (s_instance == null || s_instance.IsDisposed)
                    {
                        s_instance = new SplashScreen();
                    }

                    return s_instance;
                }
            }
        }

        /// <summary>
        /// Происходит при создании указателя
        /// </summary>
        /// <param name="e">
        /// Дополнительная информация обработчику события
        /// </param>
        protected override void OnHandleCreated(EventArgs e)
        {
            if (this.Handle != IntPtr.Zero)
            {
                IntPtr hWndDeskTop = GetDesktopWindow();
                SetParent(this.Handle, hWndDeskTop);
            }

            base.OnHandleCreated(e);
        }

        #endregion

        #region Показывать/скрыть

        /// <summary>
        /// Показать окно
        /// </summary>
        public static void BeginDisplay()
        {
            Instance.Visible = true;
            if (!Instance.Created)
            {
                Instance.CreateControl();
            }
            
            SetWindowPos(Instance.Handle, (IntPtr)HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            s_alphaBlend = Instance.FadeIn ? (byte)15 : (byte)255;
            Instance.SetBitmap((Bitmap)Instance.BackgroundImage, s_alphaBlend);

            // реализация эффекта мягкого возникновения
            if (Instance.FadeIn)
            {
                var tmrFade = new Timer { Interval = 10 };
                tmrFade.Tick += FadeTick;
                tmrFade.Start();
            }

            Instance.Show();
            Application.DoEvents();
        }
        
        /// <summary>
        /// Эффект мягкого возникновения
        /// </summary>
        /// <param name="sender">
        /// Объект, вызвавший событие
        /// </param>
        /// <param name="args">
        /// Дополнительная информация обработчику события
        /// </param>
        //// Несоответствие имени переменной принятому стилю
        // ReSharper disable once InconsistentNaming
        protected static void FadeTick(object sender, EventArgs args)
        {
            if (s_alphaBlend >= 255)
            {
                ((Timer)sender).Stop();
                Instance.SetBitmap((Bitmap)Instance.BackgroundImage);
                return;
            }

            Instance.SetBitmap((Bitmap)Instance.BackgroundImage, s_alphaBlend);
            s_alphaBlend += 10;            
            Instance.Refresh();
        }

        /// <summary>
        /// Закрыть окно
        /// </summary>
        public static void EndDisplay()
        {
            Instance.Visible = false;
            Instance._refreshFormTimer.Stop();
        }
        #endregion

        #region Свойства

        /// <summary>
        /// Установить заголовок комментария
        /// </summary>
        /// <param name="text">
        /// Текст заголовка
        /// </param>
        public static void SetTitle(string text)
        {
            Instance.Title = text;
        }

        /// <summary>
        /// Фон формы
        /// </summary>
        /// <param name="image">
        /// Картинка фона формы
        /// </param>
        public static void SetBackgroundImage(Image image)
        {
            Instance.BackgroundImage = image;
            Instance.Width = image.Width;
            Instance.Height = image.Height;
        }

        /// <summary>
        /// Установить текущий комментарий
        /// </summary>
        /// <param name="text">
        /// Текст комментария
        /// </param>
        public static void SetCurrentTitle(string text)
        {
            Instance.CurrentTitle = text;
        }

        /// <summary>
        /// Установить эффект возникновения
        /// </summary>
        /// <param name="iffadein">
        /// True - установить эффект прозрачности, fakse - отменить эффект прозрачности
        /// </param>
        public static void SetFadeIn(bool iffadein)
        {
            Instance.FadeIn = iffadein;
        }

        #region Private

        /// <summary>
        /// Заголовок
        /// </summary>
        private string Title
        {
            set
            {
                this._title = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Текущий заголовок
        /// </summary>
        private string CurrentTitle
        {
            set
            {
                this._currenttitle = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Эффект возникновения
        /// </summary>
        private bool FadeIn
        {
            get
            {
                return this._ifShowFadeIn;
            }

            set
            {
                this._ifShowFadeIn = value;
                this.Refresh();
            }
        }

        #endregion

        #endregion
    }
}