// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmBaseEditForm.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Базовый класс формы редактирования
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
    /// Базовый класс формы редактирования
    /// </summary>
    public class fmBaseEditForm : XtraForm
    {
        /// <summary>
        /// Флаг: true, если происходит загрузка данных
        /// </summary>
        public bool IsLoading { get; private set; }

        /// <summary>
        /// Базовый класс формы редактирования
        /// </summary>
        public fmBaseEditForm()
        {
            this.IsLoading = true;
            this.InitializeComponent();

            this.Load += this.BaseEditFormLoad;
            this.FormClosing += this.BaseEditFormFormClosing;
        }

        /// <summary>
        /// Обработка загрузки формы
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void BaseEditFormLoad(object sender, EventArgs args)
        {
            try
            {
                var senderControl = sender as Control;
                if (senderControl != null)
                    this.SetErrorIcomAlignmentRecursive(senderControl);

                this.LoadDataCore();
            }
            catch (Exception ex)
            {
                xMsg.Error(new Exception("Произошла ошибка загрузки формы.", ex));
            }

            this.IsLoading = false;
        }

        /// <summary>
        /// Метод загрузки данных. гарантирует правильный вызов логики ядра и наследников. Не использовать для загрузки данных
        /// </summary>
        protected virtual void LoadDataCore()
        {
            this.LoadData();
        }

        /// <summary>
        /// Выполняет загрузку данных из полей объекта в элементы редактирования
        /// </summary>
        protected virtual void LoadData()
        {
        }

        /// <summary>
        /// Рекурсивная установка положения иконки контрола
        /// </summary>
        /// <param name="control">Контрол для настройки</param>
        private void SetErrorIcomAlignmentRecursive(Control control)
        {
            if (control == null)
                throw new ArgumentNullException("control");

            var baseEdit = control as BaseEdit;
            if (baseEdit != null)
                baseEdit.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;

            foreach (Control chield in control.Controls)
                this.SetErrorIcomAlignmentRecursive(chield);
        }

        /// <summary>
        /// Обработка закрытия формы. Диалог с пользователем
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        protected virtual void BaseEditFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
            {
                // Спросить и продолжить или нет..
                var result = MessageBox.Show(@"Сохранить изменения?", @"Внимание", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Cancel)
                    e.Cancel = true;
                else if (result == DialogResult.Yes)
                    this.DialogResult = DialogResult.OK;
                else if (result == DialogResult.No)
                    this.DialogResult = DialogResult.Abort;
            }

            if (this.DialogResult == DialogResult.OK)
            {
                if (this.CheckFields())
                {
                    try
                    {
                        this.SaveDataCore();
                    }
                    catch (Exception ex)
                    {
                        xMsg.Error(ex);
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Выполняет пвлидацию полей
        /// </summary>
        /// <returns>true, если проврека пройдена</returns>
        protected virtual bool CheckFieldsCore()
        {
            return this.CheckFields();
        }

        /// <summary>
        /// Выполняет пвлидацию полей
        /// </summary>
        /// <returns>true, если проврека пройдена</returns>
        protected virtual bool CheckFields()
        {
            return true;
        }

        /// <summary>
        /// Выполняет сохранение данных в поля редактирования
        /// </summary>
        protected virtual void SaveDataCore()
        {
            this.SaveData();
        }

        /// <summary>
        /// Выполняет сохранение данных в поля редактирования
        /// </summary>
        protected virtual void SaveData()
        {
        }

        /// <summary>
        /// Инициализация формы. Установка позициии и настроек по умолчанию
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
        }
    }
}
