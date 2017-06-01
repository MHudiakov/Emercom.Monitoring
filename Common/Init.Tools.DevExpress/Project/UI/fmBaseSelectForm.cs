// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmBaseSelectForm.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Базовый класс для формы выбора.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.UI
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Forms;

    using global::DevExpress.XtraEditors;

    using Init.Tools.UI;

    /// <summary>
    /// Базовый класс для формы выбора.
    /// </summary>
    public partial class fmBaseSelectForm : XtraForm
    {
        /// <summary>
        /// Подписывание на события загрузки и закрытия формы.
        /// </summary>
        protected fmBaseSelectForm()
        {
            this.Load += this.BaseSelectFormLoad;
            this.FormClosing += this.BaseEditFormFormClosing;
        }

        /// <summary>
        /// Выгрузка данных в элементы.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void BaseSelectFormLoad(object sender, EventArgs e)
        {
            try
            {
                this.LoadData();
            }
            catch (Exception ex)
            {
                xMsg.Warning(ex.Message);
                this.Close();
            }
        }

        /// <summary>
        /// Закрытие формы по нажатию на кнопку "X"
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        protected virtual void BaseEditFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
            {
                var closingDialog = MessageBox.Show(@"Вы закрываете форму. Выбрать выделенный элемент?", @"Внимание!", MessageBoxButtons.YesNoCancel);
                if (closingDialog == DialogResult.Cancel)
                    e.Cancel = true;
                if (closingDialog == DialogResult.Yes)
                    this.DialogResult = DialogResult.OK;
                if (closingDialog == DialogResult.No)
                    this.DialogResult = DialogResult.Abort;
            }

            if (this.DialogResult == DialogResult.OK)
            {
                try
                {
                    this.SaveData();
                }
                catch (Exception ex)
                {
                    xMsg.Warning(ex.Message);
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Метод загружающий необходимые данные.
        /// </summary>
        protected virtual void LoadData()
        {
        }

        /// <summary>
        /// Метод, записывающий выбранный пользователем объект во временную переменную.
        /// </summary>
        protected virtual void SaveData()
        {
        }
    }
}