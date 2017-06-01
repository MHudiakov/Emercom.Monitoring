// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmError.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Форма сообщения об ошибке
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.UI.Forms
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Форма сообщения об ошибке
    /// </summary>
    public partial class fmError : Form
    {
        /// <summary>
        /// Форма сообщения об ошибке
        /// </summary>
        private fmError()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Отображает форму сообщения об ошибке
        /// </summary>
        /// <param name="ex">Информация об ошибке</param>
        public static void Show(Exception ex)
        {
            if (ex == null)
                throw new ArgumentNullException("ex");

            using (var fm = new fmError())
            {
                fm._messageError = ex;
                fm.ShowDialog();
            }
        }

        /// <summary>
        /// Информация об ошибке
        /// </summary>
        private Exception _messageError;

        /// <summary>
        /// Рекурсивное форматирование полного описания ошибки
        /// </summary>
        /// <param name="ex">Информация об ошибке</param>
        /// <returns>Текстовое описание ошибки</returns>
        private string GetExtendedErrorDescription(Exception ex)
        {
            if (ex == null)
                return string.Empty;

            return string.Format("{0} Стек:{1}\r\n----------\r\n\r\n{2}", ex.Message, ex.StackTrace, this.GetExtendedErrorDescription(ex.InnerException));
        }

        /// <summary>
        /// Рекурсивное форматирование краткого описания ошибки
        /// </summary>
        /// <param name="ex">Информаиция об ошибке</param>
        /// <returns>Текстовое описание ошибки</returns>
        private string GetErrorDescription(Exception ex)
        {
            if (ex == null)
                return string.Empty;

            // Добавляем точку в конце предложения
            var message = ex.Message;
            if (!message.Trim().EndsWith("."))
                message = message.Trim() + ".";

            return string.Format("{0} {1}", message, this.GetErrorDescription(ex.InnerException));
        }

        /// <summary>
        /// Обработка загрузки формы
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void FmErrorLoad(object sender, EventArgs e)
        {
            this.meError.Text = this.GetExtendedErrorDescription(this._messageError);
            this.lcMessage.Text = this.GetErrorDescription(this._messageError);
            this.meError.Visible = false;
            this.meError.Visible = false;
            this.ClientSize = new Size(this.ClientSize.Width, this.pcTop.Height + this.pcBottom.Height + this.Padding.Vertical);
        }

        /// <summary>
        /// Раскрываем описание ошибки по кнопке
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void LcDetailsClick(object sender, EventArgs e)
        {
            this.meError.Visible = !this.meError.Visible;
            this.lcDetails.Text = this.meError.Visible ? "скрыть..." : "подробнее...";
            if (this.meError.Visible)
                this.Height += 200;
            else
                this.ClientSize = new Size(this.ClientSize.Width, this.pcTop.Height + this.pcBottom.Height + this.Padding.Vertical);
        }
    }
}