// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlertControlManager.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Менеджер всплывающих сообщений
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.XtraBars
{
    using System;
    using System.Windows.Forms;

    using global::DevExpress.XtraBars.Alerter;

    /// <summary>
    /// Менеджер всплывающих сообщений
    /// </summary>
    public class AlertControlManager
    {
        /// <summary>
        /// Менеджер всплывающих сообщений
        /// </summary>
        /// <param name="form">
        /// Компонент, в потоке которого будет выполняться вывод сообщений
        /// </param>
        public AlertControlManager(Form form)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            this._form = form;

            this._alertControl = new AlertControl { AllowHtmlText = true, AllowHotTrack = true, AutoFormDelay = 10000 };
            this._alertControl.AppearanceText.TextOptions.WordWrap = global::DevExpress.Utils.WordWrap.Wrap;
            this._alertControl.AppearanceCaption.TextOptions.WordWrap = global::DevExpress.Utils.WordWrap.Wrap;
        }

        /// <summary>
        /// Всплывающее сообщение
        /// </summary>
        private readonly AlertControl _alertControl;

        /// <summary>
        /// Кешированная ссылка на форму
        /// </summary>
        private readonly Form _form;

        /// <summary>
        /// Показать всплывающее сообщение
        /// </summary>
        /// <param name="caption">
        /// Заголовок
        /// </param>
        /// <param name="text">
        /// Текст
        /// </param>
        public void ShowAlert(string caption, string text)
        {
            this._form.Invoke(
                new Action<string, string>((c, t) => this._alertControl.Show(this._form, new AlertInfo(c, t) { Tag = this })),
                caption,
                text);
        }
    }
}
