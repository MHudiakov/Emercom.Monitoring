// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmEditMovement.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Defines the fmEditMovement type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Client.UI.EditForms
{
    using System.Windows.Forms;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using DevExpress.XtraLayout.Utils;

    using Init.Tools.DevExpress.UI;
    using DAL.WCF;
    using DAL.WCF.ServiceReference;
    /// <summary>
    /// Форма редактирования новой группы оборудования
    /// </summary>
    public partial class fmEditMovement : fmBaseEditForm
    {
        /// <summary>
        /// Оборудование
        /// </summary>
        public Movement Movement { get; private set; }

        /// <summary>
        /// Конструктор формы редактирования новой группы оборудования
        /// </summary>
        public fmEditMovement()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание формы редактирования нового оборудования
        /// </summary>
        /// <returns>Новое оборудование</returns>
        public static bool Execute(Movement movement)
        {
            using (var fm = new fmEditMovement())
            {
                fm.Movement = movement;

                return fm.ShowDialog() == DialogResult.OK;
            }
        }

        protected override void LoadData()
        {
            //Добавляем оборудование в luekEquipment
            var listkEquipment = DalContainer.WcfDataManager.ServiceOperationClient.GetAllkEquipment();
            luekEquipment.Properties.DataSource = listkEquipment;

            if (Movement.Id != 0)
            {
                luekEquipment.EditValue = listkEquipment.FirstOrDefault(e => e.Id == Movement.kEquipment.Id);

                if (listEquipment != null)
                    lueEquipmentRFId.EditValue = listEquipment.FirstOrDefault(e => e.Id == Movement.EquipmentId);

                rgArrive.EditValue = Movement.IsArrived;
                deDateOfMovement.DateTime = Movement.DateOfMovement;
            }
            base.LoadData();
        }

        /// <summary>
        /// Сохранение данных формы
        /// </summary>
        protected override void SaveData()
        {
            if (luekEquipment.EditValue == null)
                throw new Exception("Необходимо выбрать оборудование");

            if (lueEquipmentRFId.EditValue == null)
                throw new Exception("Необходимо выбрать склад оборудования");

            if (string.IsNullOrEmpty(rgArrive.EditValue.ToString()))
                throw new Exception("Необходимо выбрать вид движения");

            if (string.IsNullOrEmpty(deDateOfMovement.EditValue.ToString()))
                throw new Exception("Необходимо выбрать дату перемещения");

            var equipment = lueEquipmentRFId.EditValue as Equipment;            
                
            Movement.EquipmentId = equipment.Id;

            //при необходимости добавить изменения в Movement

            Movement.IsArrived = (bool)rgArrive.EditValue;

            Movement.DateOfMovement = (DateTime)deDateOfMovement.EditValue;

            base.SaveData();
        }

        /*
        private void teNumberKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != (Char)Keys.Delete || e.KeyChar == 46)
                e.Handled = true;
        }

        */
        private void luekEquipmentEditValueChanged(object sender, EventArgs e)
        {
            var kequipment = luekEquipment.EditValue as kEquipment;
            //Добавляем оборудование в lueEquipment
            if (kequipment != null)
            {
                listEquipment = DalContainer.WcfDataManager.ServiceOperationClient.GetAllEquipment().Where(s => s.kEquipmentId == kequipment.Id).ToList();
                lueEquipmentRFId.Properties.DataSource = listEquipment;
            }            
        }
      
        private List<Equipment> listEquipment;
    }
}