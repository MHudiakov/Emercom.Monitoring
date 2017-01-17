// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmEditUnit.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Defines the fmEditUnit type.
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
    using Init.Tools.DevExpress.UI;
    using DAL.WCF.ServiceReference;
    using DAL.WCF;

    using Init.Tools;
    using Init.Tools.UI;

    /// <summary>
    /// Форма редактирования объекта
    /// </summary>
    public partial class fmEditUnit : fmBaseEditForm
    {
        /// <summary>
        /// Конструктор формы редактирования объекта
        /// </summary>
        public fmEditUnit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Объект
        /// </summary>
        private Unit Unit { get; set; }

        /// <summary>
        /// Создание формы редактирования типа объекта
        /// </summary>
        /// <param name="unit">Передаваемый экземпляр объекта</param>
        public static bool Execute(Unit unit)
        {
            Log.Add("main", "Создание формы редактирования типа объекта");
            var kObjectList = DalContainer.WcfDataManager.kObjectList;

            if (!kObjectList.Any())
            {
                xMsg.Information("В базе нет типов объектов! Заполните соответствующий справочник.");
                return false;
            }

            using (var fm = new fmEditUnit())
            {
                fm.Unit = unit;

                return fm.ShowDialog() == DialogResult.OK;
            }
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        protected override void LoadData()
        {
            luekObject.Properties.DataSource = DalContainer.WcfDataManager.kObjectList;

            if (Unit.Id != 0)
            {
                teName.Text = Unit.Name;
                teDescription.Text = Unit.Description;
                var unit = DalContainer.WcfDataManager.kObjectList.FirstOrDefault(u => u.Id == Unit.kObjectId);
                luekObject.EditValue = unit.Id;
                teSerialNum.Text = Unit.SerialNum;
                teGosNum.Text = Unit.GosNum;
                teBortNum.Text = Unit.BortNum;
            }
            else
            {
                luekObject.EditValue = DalContainer.WcfDataManager.kObjectList[0].Id;
            }

            base.LoadData();
        }

        /// <summary>
        /// Сохранение данных формы
        /// </summary>
        protected override void SaveData()
        {
            if (string.IsNullOrEmpty(teName.Text))
                throw new Exception("Нужно ввести название объекта");
            if (luekObject.EditValue == null)
                throw new Exception("Необходимо выбрать тип объекта");

            Unit.Name = teName.Text;
            Unit.Description = teDescription.Text;
            var kobject = DalContainer.WcfDataManager.kObjectList.FirstOrDefault(u => u.Id == (int)luekObject.EditValue);
            Unit.kObjectId = kobject.Id;
            Unit.SerialNum = teSerialNum.Text;
            Unit.GosNum = teGosNum.Text;
            Unit.BortNum = teBortNum.Text;

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

        private void luekObject_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void teName_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void teDescription_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void teSerialNum_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void teGosNum_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void teBortNum_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

    }
}