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
using Init.Tools.UI;

namespace Client.UI.EditForms
{
    /// <summary>
    /// Окно движения выбранного объекта/оборудования
    /// </summary>
    public partial class fmMovement : XtraForm
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public fmMovement()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Оборудование
        /// </summary>
        private Equipment Equipment { get; set; }

        /// <summary>
        /// Объект
        /// </summary>
        private Unit Unit { get; set; }

        /// <summary>
        /// Создание формы списка движения
        /// </summary>
        public static bool Execute(Unit unit, Equipment equipment)
        {
            using (var fm = new fmMovement())
            {
                fm.Equipment = equipment;
                fm.Unit = unit;

                return fm.ShowDialog() == DialogResult.OK;
            }
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void fmMovement_Load(object sender, EventArgs e)
        {
            lueEquipment.Properties.DataSource = DalContainer.WcfDataManager.EquipmentList;
            lueUnit.Properties.DataSource = DalContainer.WcfDataManager.UnitList;

            if (this.Equipment != null)
            {
                lueEquipment.EditValue = DalContainer.WcfDataManager.EquipmentList.FirstOrDefault(eq => eq.Id == this.Equipment.Id).Id;
                lueEquipment.ReadOnly = true;
            }

            if (this.Unit != null)
            {
                lueUnit.EditValue = DalContainer.WcfDataManager.UnitList.FirstOrDefault(u => u.Id == this.Unit.Id).Id;
                lueUnit.ReadOnly = true;
            }

            deFrom.EditValue = DateTime.Today.Date.AddDays(-1);
            deTo.EditValue = DateTime.Today.Date.AddDays(1);
            teFrom.EditValue = new TimeSpan(0, 0, 0);
            teTo.EditValue = new TimeSpan(0, 0, 0);

            gcMovement.DataSource = GetMovementList();
        }
        
        /// <summary>
        /// Получение списка движения по объекту/оборудованию
        /// </summary>
        /// <returns></returns>
        private List<Movement> GetMovementList()
        {
            var list = DalContainer.WcfDataManager.MovementList.Where(m => m.DateOfMovement >= this.From && m.DateOfMovement <= this.To).ToList();

            if (lueUnit.EditValue != null)
            {
                list = list.Where(m => m.UnitId == (int)lueUnit.EditValue).ToList();
                clmUnit.Visible = false;
            }

            if (lueEquipment.EditValue != null)
            {
                list = list.Where(m => m.EquipmentId == (int)lueEquipment.EditValue).ToList();
                clmEquipment.Visible = false;
            }

            list.Reverse();

            return list;
        }

        /// <summary>
        /// Дата-время начала
        /// </summary>
        private DateTime From
        {
            get
            {
                DateTime res;

                res = deFrom.DateTime.Date;
                res = res.Add(teFrom.Time.TimeOfDay);

                return res;
            }
        }

        /// <summary>
        /// Дата-время окончания
        /// </summary>
        private DateTime To
        {
            get
            {
                DateTime res;

                res = deTo.DateTime.Date;
                res = res.Add(teTo.Time.TimeOfDay);

                return res;
            }
        }

        /// <summary>
        /// Загрузка списка движения по фильтрам формы
        /// </summary>
        private void sbFind_Click(object sender, EventArgs e)
        {
            if (From > To)
                xMsg.Information("Дата начала периода наблюдения не может быть похже даты конца!");
            else
            {
                gcMovement.DataSource = GetMovementList();
            }
        }

        /// <summary>
        /// Очистка поля объекта
        /// </summary>
        private void lueUnitButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (lueUnit.ReadOnly == false)
                lueUnit.EditValue = null;
        }

        /// <summary>
        /// Очистка поля оборудования
        /// </summary>
        private void lueEquipmentButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (lueEquipment.ReadOnly == false)
                lueEquipment.EditValue = null;
        }

    }
}