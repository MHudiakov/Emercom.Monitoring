// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ucWaitControl.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Control ожидания загрузки
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.UI.Forms
{
    /// <summary>
    /// Класс графического элемента ожидания загрузки
    /// </summary>
    public partial class ucWaitControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ucWaitControl"/>.
        /// </summary>
        public ucWaitControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Отображение загружаемого окна по окончании загрузки
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void HeCancelOpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            this.Parent.Enabled = true;
            this.Parent.Controls.Remove(this);
        }
    }
}
