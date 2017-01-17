// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmEditkObject.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Defines the fmEditkObject type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Client.UI.EditForms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Linq;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using DAL.WCF.ServiceReference;
    using DAL.WCF;

    using Init.Tools;
    using Init.Tools.DevExpress.UI;

    /// <summary>
    /// Форма редактирования типа объекта
    /// </summary>
    public partial class fmEditkObject : fmBaseEditForm
    {
        /// <summary>
        /// Конструктор формы редактирования типа объекта
        /// </summary>
        public fmEditkObject()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Тип объекта
        /// </summary>
        public kObject kUnit { get; set; }

        /// <summary>
        /// Создание формы редактирования типа объекта
        /// </summary>
        /// <param name="kobject">Передаваемый экземпляр типа объекта</param>
        public static bool Execute(kObject kobject)
        {
            Log.Add("main", "Создание формы редактирования типа объекта");
            using (var fm = new fmEditkObject())
            {
                fm.kUnit = kobject;
                return fm.ShowDialog() == DialogResult.OK;
            }
        }
        
        /// <summary>
        /// Загрузка формы
        /// </summary>
        protected override void LoadData()
        {
            if (kUnit.Id != 0)
            {
                teType.Text = kUnit.Type;
                teDescription.Text = kUnit.Description;
                ceIsStore.Checked = kUnit.IsStore;
            }

            base.LoadData();
        }

        /// <summary>
        /// Сохранение данных формы
        /// </summary>
        protected override void SaveData()
        {
            if (string.IsNullOrEmpty(teType.Text))
                throw new Exception("Нужно ввести название типа объекта");

            kUnit.Type = teType.Text;
            kUnit.Description = teDescription.Text;
            kUnit.IsStore = ceIsStore.Checked ;

            base.SaveData();
        }

        /// <summary>
        /// Нажатие спец-клавиш
        /// </summary>
        /// <param name="e">Аргумент события</param>
        private void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sbSave.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                sbCancel.PerformClick();
            }
        }

        private void teType_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void teDescription_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void ceIsStore_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }
    }
}