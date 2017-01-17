// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmEditNonUniqEquipment.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Defines the fmEditNonUniqEquipment type.
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

    /// <summary>
    /// Форма редактирования неуникального оборудования
    /// </summary>
    public partial class fmEditNonUniqEquipment : fmBaseEditForm
    {
        /// <summary>
        /// Конструктор формы редактирования неуникального оборудования
        /// </summary>
        public fmEditNonUniqEquipment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Неуникальное оборудование
        /// </summary>
        private NonUniqEquipmentObject NonUniqEquipment { get; set; }

        /// <summary>
        /// Создание формы редактирования неуникального оборудования
        /// </summary>
        /// <param name="nonUniqEquipment">Передаваемый экземпляр неуникального оборудования</param>
        public static bool Execute(NonUniqEquipmentObject nonUniqEquipment)
        {
            Log.Add("main", "Создание формы редактирования неуникального оборудования");
            using (var fm = new fmEditNonUniqEquipment())
            {
                fm.NonUniqEquipment = nonUniqEquipment;

                return fm.ShowDialog() == DialogResult.OK;
            }
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        protected override void LoadData()
        {
            var listUnit = DalContainer.WcfDataManager.UnitList.ToList();
            lueUnit.Properties.DataSource = listUnit;
            var unit = listUnit.FirstOrDefault(u => u.Id == NonUniqEquipment.UnitId);
            lueUnit.EditValue = unit.Id;

            var listNonUniqEquipment = DalContainer.WcfDataManager.NonUniqEquipmentList.Select(e => e.kEquipmentId).Distinct().ToList();
            var listkEquipment = new List<kEquipment>();

            foreach (var item in listNonUniqEquipment)
                listkEquipment.Add(DalContainer.WcfDataManager.kEquipmentList.FirstOrDefault(e => e.Id == item));

            luekEquipment.Properties.DataSource = listkEquipment.Where(eq => !eq.IsUniq).ToList();

            if (NonUniqEquipment.Id != 0)
            {
                var equipment = listkEquipment.FirstOrDefault(eq => eq.Id == NonUniqEquipment.kEquipmentId);
                luekEquipment.EditValue = equipment.Id;

                seCount.EditValue = NonUniqEquipment.Count;
            }

            base.LoadData();
        }

        /// <summary>
        /// Сохранение данных формы
        /// </summary>
        protected override void SaveData()
        {
            if (lueUnit.EditValue == null)
                throw new Exception("Необходимо выбрать объект");
            if (luekEquipment.EditValue == null)
                throw new Exception("Необходимо выбрать тип оборудования");
            if (Convert.ToInt32(seCount.EditValue) <= 0)
                throw new Exception("Необходимо ввести количество оборудования");

            NonUniqEquipment.UnitId = Convert.ToInt32(lueUnit.EditValue);
            NonUniqEquipment.kEquipmentId = Convert.ToInt32(luekEquipment.EditValue);
            NonUniqEquipment.Count = Convert.ToInt32(seCount.EditValue);

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

        private void lueUnit_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void luekEquipment_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void seCount_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

    }
}