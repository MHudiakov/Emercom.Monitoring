// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ucMovementLog.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Журнал движения
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Client.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Linq;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using Init.Tools.DevExpress;
    using Init.Tools.DevExpress.UI.Components;
    using Init.Tools;
    using Init.Tools.UI;
    using DAL.WCF;
    using DAL.WCF.ServiceReference;
    using Client.UI.EditForms;

    /// <summary>
    /// Журнал движения
    /// </summary>
    public partial class ucMovementLog : XtraUserControl, IBaseControl
    {
        public FormHelper FormHelper { get; set; }

        public string Header { get; set; }

        private bool IsFirstLoading = true;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public ucMovementLog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Активатор формы
        /// </summary>
        public void Activate()
        {
            try
            {
                if (IsFirstLoading)
                {
                    deFrom.EditValue = DateTime.Today.Date.AddDays(-1);
                    deTo.EditValue = DateTime.Today.Date.AddDays(1);
                    teFrom.EditValue = new TimeSpan(0, 0, 0);
                    teTo.EditValue = new TimeSpan(0, 0, 0);

                    lueUnit.Properties.DataSource = DalContainer.WcfDataManager.UnitList;
                    lueEquipment.Properties.DataSource = DalContainer.WcfDataManager.EquipmentList;

                    IsFirstLoading = false;
                }
                Log.Add("main", "Загрузка формы передвижения оборудования");
            }
            catch (Exception ex)
            {
                Log.AddException("error", new Exception("Ошибка загрузки журнала движения", ex));
            }
        }

        /// <summary>
        /// Дезактиватор формы
        /// </summary>
        public void Deactivate()
        {
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
            if (CheckFilter())
            {
                var list = DalContainer.WcfDataManager.ServiceOperationClient.GetMovementListByTimeAndUnitId(From, To, 0);

                if (lueEquipment.EditValue != null)
                    list = list.Where(m => m.EquipmentId == (int)lueEquipment.EditValue).ToList();

                if (lueUnit.EditValue != null)
                    list = list.Where(m => m.UnitId == (int)lueUnit.EditValue).ToList();

                list.Reverse();
                gcMovement.DataSource = list;
            }
            else
                xMsg.Information("Дата начала периода наблюдения не может быть позже даты конца!");
        }

        /// <summary>
        /// Проверка фильтров
        /// </summary>
        /// <returns>результат</returns>
        private bool CheckFilter()
        {
            if (From >= To)
                return false;

            return true;
        }

        /// <summary>
        /// Очистка поля оборудования
        /// </summary>
        private void lueEquipmentButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            lueEquipment.EditValue = null;
        }

        /// <summary>
        /// Очистка поля объекта
        /// </summary>
        private void lueUnitButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            lueUnit.EditValue = null;
        }

    }
}
