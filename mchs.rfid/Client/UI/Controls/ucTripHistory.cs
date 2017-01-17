//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="ucTripHistory.cs" company="ИНИТ-центр">
////   ИНИТ-центр, 2016г.
//// </copyright>
//// <summary>
////   Карта поездок
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------

namespace Client.UI.Controls
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
    using Init.Tools;
    using DAL.WCF;
    using Client.UI.SpecialForms;
    using DAL.WCF.ServiceReference;
    using Init.Tools.UI;
    using System.Data.SqlTypes;

    /// <summary>
    /// Форма объектов
    /// </summary>
    public partial class ucTripHistory : XtraUserControl, IBaseControl
    {
        public FormHelper FormHelper { get; set; }

        public string Header { get; set; }

        private bool IsFirstLoading = true;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public ucTripHistory()
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
                    deFrom.EditValue = DateTime.Today.Date.AddDays(-4);
                    deTo.EditValue = DateTime.Today.Date.AddDays(1);
                    teFrom.EditValue = new TimeSpan(0, 0, 0);
                    teTo.EditValue = new TimeSpan(0, 0, 0);

                    lueUnit.Properties.DataSource = DalContainer.WcfDataManager.UnitList.Where(u => !u.IsStore).ToList();
                    // lueUnit.EditValue = DalContainer.WcfDataManager.UnitList[1].Id;

                    IsFirstLoading = false;
                }
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception("Ошибка загрузки раздела поездок", ex));
            }
        }

        /// <summary>
        /// Дезактиватор формы
        /// </summary>
        public void Deactivate()
        { }

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
        /// Загрузка списка поездок по фильтрам формы
        /// </summary>
        private void sbFind_Click(object sender, EventArgs e)
        {
            // отображение данных в таблице по фильтрам
            if (CheckFilter())
            {
                var list = DalContainer.WcfDataManager.TripList.Where(t => t.UnitId == (int)lueUnit.EditValue && t.StartTime >= From && t.StartTime < To);

                foreach (var trip in list)
                    if (trip.EndTime == null || trip.EndTime <= (DateTime)SqlDateTime.MinValue)
                        trip.EndTime = DateTime.Now;

                gcTrip.DataSource = list;
            }
        }

        /// <summary>
        /// Проверка фильтров
        /// </summary>
        /// <returns>результат</returns>
        private bool CheckFilter()
        {
            if (From >= To)
            {
                xMsg.Information("Дата/время начала периода не может быть больше даты/времени конца!");
                return false;
            }
            if (lueUnit.EditValue == null)
            {
                xMsg.Information("Не выбрана машина!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Вывод карты поездки по нажатию на кнопку
        /// </summary>
        private void sbView_Click(object sender, EventArgs e)
        {
            try
            {
                var focused = gvTrip.GetFocusedRow() as Trip;
                if (focused != null)
                    fmTripOnMap.Execute(focused);
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception("Ошибка при открытии окна карты поездки", ex));
            }
        }

        /// <summary>
        /// Вывод карты поездки по двойному клику на поездке
        /// </summary>
        private void gcTrip_Click(object sender, EventArgs e)
        {
            try
            {
                var focused = gvTrip.GetFocusedRow() as Trip;
                if (focused != null)
                    fmTripOnMap.Execute(focused);
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception("Ошибка при открытии окна карты поездки", ex));
            }
        }

    }
}
