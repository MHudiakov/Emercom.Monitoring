// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ucRealTime.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Окно Мониторинга
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
    using System.Threading;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using Init.Tools.DevExpress;
    using DAL.WCF;
    using DAL.WCF.Enums;
    using Init.Tools.UI;
    using DAL.WCF.ServiceReference;
    using Init.Tools.DevExpress.UI.Components;
    using Client.UI.EditForms;

    using DevExpress.XtraLayout.Utils;

    using Init.Tools;
    using Client.UI.SpecialForms;

    /// <summary>
    /// Мониторинг, реальное время
    /// </summary>
    public partial class ucRealTime : XtraUserControl, IBaseControl
    {
        public FormHelper FormHelper { get; set; }

        public string Header { get; set; }

        private bool IsFirstLoading = true;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public ucRealTime()
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
                    DalContainer.WcfDataManager.RefreshCashLists();
                    gcUnit.DataSource = GetUnitList();
                    IsFirstLoading = false;
                }
                else
                    RefreshPage();

                tmRefresh.Enabled = true;
                Log.Add("main", "Загрузка формы мониторинга в реальном времени");
            }
            catch (Exception ex)
            {
                Log.AddException("error", new Exception("Ошибка загрузки раздела мониторинга", ex));
            }
        }

        /// <summary>
        /// Дезактиватор формы
        /// </summary>
        public void Deactivate()
        {
            tmRefresh.Enabled = false;
        }

        /// <summary>
        /// Таймер обновления таблиц
        /// </summary>
        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            RefreshPage();
        }

        /// <summary>
        /// Обновление страницы
        /// </summary>
        private void RefreshPage()
        {
            var focused = gvUnit.GetFocusedRow() as Unit;
            gcUnit.DataSource = GetUnitList();

            var rowHandle = focused != null ? gvUnit.LocateByValue("Id", focused.Id)
                                            : gvUnit.LocateByValue("Id", DalContainer.WcfDataManager.UnitList[0].Id);
            if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                gvUnit.FocusedRowHandle = rowHandle;
        }

        /// <summary>
        /// Вывод формы движения по двойному клику на объекте
        /// </summary>
        private void gvUnit_DoubleClick(object sender, EventArgs e)
        {
            var focused = gvUnit.GetFocusedRow() as Unit;
            if (focused != null)
                fmEquipment.Execute(focused);
        }

        private List<Unit> GetUnitList()
        {
            var unitList = DalContainer.WcfDataManager.UnitList;
            var list = new List<Unit>();

            foreach (var unit in unitList)
            {
                var uniqList = SearchUniqEquipmentObject(unit);
                var nonUniqList = SearchNonUniqEquipmentObject(unit);
                var indexlist = uniqList.Select(item => item.ColorForAppearance).ToList();
                indexlist.AddRange(nonUniqList.Select(item => item.ColorForAppearance));

                unit.ColorForAppearance = indexlist.Exists(n => n == (int)ColourEnum.Red)
                                          ? (int)ColourEnum.Red : (indexlist.Exists(n => n == (int)ColourEnum.Grey)
                                                                  ? (int)ColourEnum.Grey : (indexlist.Exists(n => n == (int)ColourEnum.Yellow)
                                                                                           ? (int)ColourEnum.Yellow : (int)ColourEnum.Green));
                list.Add(unit);
            }

            return list;
        }

        #region UniqEquipment page

        /// <summary>
        /// Метод заполнения списка уникального оборудования и определения цвета строк оного
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns>Список уникального оборудования объекта</returns>
        private List<UniqEquipmentObject> SearchUniqEquipmentObject(Unit obj)
        {
            var list = new List<UniqEquipmentObject>();
            var store = DalContainer.WcfDataManager.UnitList.FirstOrDefault(u => u.IsStore);
            var helpList = obj.IsStore
                           ? DalContainer.WcfDataManager.UniqEquipmentList.Where(eq => eq.UnitId == obj.Id).ToList()
                           : DalContainer.WcfDataManager.UniqEquipmentList.Where(eq => eq.UnitId != store.Id).ToList();

            foreach (var item in helpList)
            {
                var uniqEquipment = DalContainer.WcfDataManager.UniqEquipmentList.FirstOrDefault(ueq => ueq.Id == item.Id);
                var equipment = (uniqEquipment.UnitId == obj.Id || obj.IsStore) ? DalContainer.WcfDataManager.EquipmentList.FirstOrDefault(eq => eq.Id == uniqEquipment.EquipmentId) : uniqEquipment.Equipment;
                if (equipment == null)
                    continue;

                if (obj.IsStore || uniqEquipment.UnitId == obj.Id)
                {
                    uniqEquipment.ColorForAppearance = !equipment.IsArrivedBool ? (int)ColourEnum.Red : (equipment.LastUnitId == uniqEquipment.UnitId ? (int)ColourEnum.Green : (int)ColourEnum.Grey);
                    list.Add(uniqEquipment);
                }
                else
                {
                    if (equipment.LastUnitId == obj.Id && equipment.IsArrivedBool)
                    {
                        uniqEquipment.ColorForAppearance = (int)ColourEnum.Yellow; // yellow
                        list.Add(uniqEquipment);
                    }
                }
            }

            return list;
        }

        #endregion

        #region NonUniqEquipment page

        /// <summary>
        /// Метод заполнения списка неуникального оборудования и определения цвета строк оного
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns>Список неуникального оборудования объекта</returns>
        private List<NonUniqEquipmentObject> SearchNonUniqEquipmentObject(Unit obj)
        {
            var list = new List<NonUniqEquipmentObject>();
            var nonUniqList = DalContainer.WcfDataManager.NonUniqEquipmentList.Where(eq => eq.UnitId == obj.Id).ToList();
            var allEquipment = DalContainer.WcfDataManager.EquipmentList.Where(eq => !eq.IsUniq).ToList();

            if (!obj.IsStore)
                allEquipment = allEquipment.Where(eq => eq.LastUnitId == obj.Id && eq.IsArrivedBool).ToList();

            foreach (var item in nonUniqList)
            {
                var nonUniqEquipment = DalContainer.WcfDataManager.NonUniqEquipmentList.FirstOrDefault(nueq => nueq.Id == item.Id);
                var listEquipment = allEquipment.Where(eq => eq.kEquipmentId == nonUniqEquipment.kEquipmentId).ToList();

                var isRed = obj.IsStore
                            ? (listEquipment.Any(eq => (eq.LastMovement == null) || (eq.LastMovement != null && !eq.IsArrivedBool)))
                            : listEquipment.Count == 0;
                var isGreen = !isRed && obj.IsStore
                                        ? listEquipment.All(eq => eq.IsInStore)
                                        : listEquipment.Count >= nonUniqEquipment.Count;

                nonUniqEquipment.ColorForAppearance = isRed ? (int)ColourEnum.Red : (isGreen ? (int)ColourEnum.Green : (int)ColourEnum.Grey);
                nonUniqEquipment.RealCount = obj.IsStore ? listEquipment.Count(eq => eq.IsInStore && eq.IsArrivedBool) : listEquipment.Count;

                list.Add(nonUniqEquipment);
            }

            return list;
        }

        #endregion

    }
}
