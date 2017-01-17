// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmLoading.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Форма загрузки
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.UI.Forms
{
    using System.Windows.Forms;

    /// <summary>
    /// Класс загрузочной плашки
    /// </summary>
    public partial class fmLoading : Form
    {
        /// <summary>
        /// Плашка загрузки
        /// </summary>
        private static fmLoading s_fm;

        /// <summary>
        /// Плашка загрузки
        /// </summary>
        public static fmLoading Fm
        {
            get
            {
                return s_fm ?? (s_fm = new fmLoading());
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="fmLoading"/>.
        /// </summary>
        public fmLoading()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Начать отображение плашки загрузки
        /// </summary>
        /// <param name="msg">
        /// Отображаемое на плашке сособщение
        /// </param>
        public static void BeginDisplay(string msg = "")
        {
            if (msg != string.Empty)
                Fm.Text = msg;
            Fm.Show();
            Fm.Refresh();
        }

        /// <summary>
        /// Завершение отображения плашки загрузки
        /// </summary>
        public static void EndDisplay()
        {
            if (Fm != null)
                Fm.Hide();
        }
    }
}
