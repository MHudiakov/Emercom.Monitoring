// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmEditUniqEquipment.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Defines the fmEditUniqEquipment type.
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
    using System.ServiceModel;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using DAL.WCF.ServiceReference;
    using Init.Tools.DevExpress.UI;
    using DAL.WCF;

    using Init.Tools;
    using Init.Tools.UI;

    /// <summary>
    /// Форма редактирования уникального оборудования
    /// </summary>
    public partial class fmEditUniqEquipment : fmBaseEditForm
    {
        public fmEditUniqEquipment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Уникальное оборудование
        /// </summary>
        private UniqEquipmentObject UniqEquipment { get; set; }

        /// <summary>
        /// Создание формы редактирования уникального оборудования
        /// </summary>
        /// <param name="uniqEquipment">Передаваемый экземпляр уникального оборудования</param>
        public static bool Execute(UniqEquipmentObject uniqEquipment)
        {
            Log.Add("main", "Создание формы редактирования уникального оборудования");
			var listEquipment = DalContainer.WcfDataManager.EquipmentList.Where(eq => eq.IsUniq && eq.IsInStore).ToList();
            var store = DalContainer.WcfDataManager.UnitList.FirstOrDefault(u => u.IsStore);
            var uniqList = DalContainer.WcfDataManager.UniqEquipmentList.Where(eq => eq.UnitId != store.Id).ToList();
            foreach (var item in uniqList)
            {
                var eq = listEquipment.FirstOrDefault(e => e.Id == item.EquipmentId);

                if (eq == null)
                    continue;

                listEquipment.Remove(eq);
            }

            if (uniqEquipment.Id == 0 && !listEquipment.Any())
            {
                xMsg.Information("Нет оборудования на складе!");
                return false;
            }
            else
            {
                using (var fm = new fmEditUniqEquipment())
                {
                    fm.UniqEquipment = uniqEquipment;
                    return fm.ShowDialog() == DialogResult.OK;
                }
            }
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        protected override void LoadData()
        {
            var listUnit = DalContainer.WcfDataManager.UnitList;
            lueUnit.Properties.DataSource = listUnit;
            var unit = listUnit.FirstOrDefault(u => u.Id == UniqEquipment.UnitId);
            lueUnit.EditValue = unit.Id;

            var listEquipment = DalContainer.WcfDataManager.EquipmentList.Where(eq => eq.IsUniq && eq.IsInStore).ToList();

            var store = listUnit.FirstOrDefault(u => u.IsStore);
            var uniqList = DalContainer.WcfDataManager.UniqEquipmentList.Where(eq => eq.UnitId != store.Id).ToList();
            foreach (var item in uniqList)
            {
                var eq = listEquipment.FirstOrDefault(e => e.Id == item.EquipmentId);

                if (eq == null)
                    continue;

                listEquipment.Remove(eq);
            }

            if (UniqEquipment.Id != 0)
            {
                listEquipment.Add(DalContainer.WcfDataManager.EquipmentList.FirstOrDefault(eq => eq.Id == UniqEquipment.EquipmentId));
                lueEquipment.Properties.DataSource = listEquipment;

                var equipment = listEquipment.FirstOrDefault(eq => eq.Id == UniqEquipment.EquipmentId);
                lueEquipment.EditValue = equipment.Id;
            }
            else
            {
                lueEquipment.Properties.DataSource = listEquipment;
                // lueEquipment.EditValue = listEquipment[0].Id;
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
            if (lueEquipment.EditValue == null)
                throw new Exception("Необходимо выбрать оборудование");

            UniqEquipment.UnitId = Convert.ToInt32(lueUnit.EditValue);
            UniqEquipment.EquipmentId = Convert.ToInt32(lueEquipment.EditValue);

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

        private void lueEquipment_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

    }
}