// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ucUnits.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Окно объектов
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
    using Init.Tools.UI;
    using DAL.WCF.ServiceReference;
    using Init.Tools.DevExpress.UI.Components;
    using Client.UI.EditForms;

    using DAL.WCF.Enums;

    using Init.Tools;
    using DevExpress.XtraLayout.Utils;

    /// <summary>
    /// Форма объектов
    /// </summary>
    public partial class ucUnits : XtraUserControl, IBaseControl
    {
        public FormHelper FormHelper { get; set; }

        public string Header { get; set; }

        private bool IsFirstLoading = true;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public ucUnits()
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

                    UnitStrategy();
                    UniqEquipmentStrategy();
                    NonUniqEquipmentStrategy();

                    IsFirstLoading = false;
                }
                else
                    RefreshPage();

                tmRefresh.Enabled = true;
                Log.Add("main", "Загрузка формы машиг и их укомплектованности");
            }
            catch (Exception ex)
            {
                Log.AddException("error", new Exception("Ошибка загрузки раздела объектов и оборудования", ex));
            }
        }

        /// <summary>
        /// Дезактиватор формы
        /// </summary>
        public void Deactivate()
        {
            tmRefresh.Enabled = false;
        }

        #region Unit page

        /// <summary>
        /// Ссылка на объект реализации интерфейса грида
        /// </summary>
        private GridHelperWF<Unit> helperUnit = null;

        /// <summary>
        /// Метод заполнения страницы хранение на складе
        /// </summary>
        private void UnitStrategy()
        {
            try
            {
                var strategy = new PlainStrategy<Unit>(
                    (unit) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.AddUnit(unit);
                        helperUnit.AsyncUpdateData();

                        DalContainer.WcfDataManager.UnitList.Add(unit);
                        gvUnit.RefreshData();
                    },
                    (unit) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.EditUnit(unit);
                        helperUnit.AsyncUpdateData();

                        var index = DalContainer.WcfDataManager.UnitList.FindIndex(c => c.Id == unit.Id);
                        DalContainer.WcfDataManager.UnitList[index] = unit;
                        gvUnit.RefreshData();
                    },
                    (unit) =>
                    {
                        var listNonUniq = DalContainer.WcfDataManager.NonUniqEquipmentList.Where(e => e.UnitId == unit.Id).ToList();
                        var listUniq = DalContainer.WcfDataManager.UniqEquipmentList.Where(e => e.UnitId == unit.Id).ToList();

                        if ((listNonUniq.Count != 0) || (listUniq.Count != 0))
                            xMsg.Information("Невозможно удалить объект: " + unit.Name + ", т.к. список оборудования, ссылающегося на него, не пуст!");
                        else
                        {
                            var listMovement =
                                DalContainer.WcfDataManager.ServiceOperationClient.GetMovementListByTimeAndUnitId(
                                    DateTime.Now,
                                    DateTime.Now,
                                    unit.Id);

                            foreach (var item in listMovement)
                                DalContainer.WcfDataManager.ServiceOperationClient.DeleteMovement(item);

                            DalContainer.WcfDataManager.ServiceOperationClient.DeleteUnit(unit);
                            DalContainer.WcfDataManager.UnitList.RemoveAll(e => e.Id == unit.Id);
                            gvUnit.RefreshData();

                            helperUnit.AsyncUpdateData();
                        }
                    },
                    () =>
                    {
                        var item = new Unit();
                        return item;
                    },
                    () =>
                    {
                        var items = DalContainer.WcfDataManager.UnitList;
                        return items;
                    });

                if (helperUnit == null)
                    helperUnit = new GridHelperWF<Unit>(
                        gcUnit,
                        sbAddUnit,
                        sbEditUnit,
                        sbDeleteUnit,
                        fmEditUnit.Execute,
                        strategy);

                helperUnit.AsyncUpdateData();
            }
            catch (Exception exception)
            {
                Log.AddException("error", new Exception("Ошибка загрузки страницы объектов", exception));
            }
        }

        #endregion

        #region UniqEquipment page

        /// <summary>
        /// Ссылка на объект реализации интерфейса грида
        /// </summary>
        private GridHelperWF<UniqEquipmentObject> helperUniqEquipment = null;

        /// <summary>
        /// Метод заполнения страницы хранение на складе
        /// </summary>
        private void UniqEquipmentStrategy()
        {
            try
            {
                var strategy = new PlainStrategy<UniqEquipmentObject>(
                    (uniqEquipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.AddUniqEquipmentObject(uniqEquipment);
                        helperUniqEquipment.AsyncUpdateData();

                        DalContainer.WcfDataManager.UniqEquipmentList.Add(uniqEquipment);
                        gvUniqEquipment.RefreshData();
                    },
                    (uniqEquipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.EditUniqEquipmentObject(uniqEquipment);
                        helperUniqEquipment.AsyncUpdateData();

                        var index = DalContainer.WcfDataManager.UniqEquipmentList.FindIndex(c => c.Id == uniqEquipment.Id);
                        DalContainer.WcfDataManager.UniqEquipmentList[index] = uniqEquipment;
                        gvUniqEquipment.RefreshData();
                    },
                    (uniqEquipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.DeleteUniqEquipmentObject(uniqEquipment);
                        helperUniqEquipment.AsyncUpdateData();

                        DalContainer.WcfDataManager.UniqEquipmentList.RemoveAll(e => e.Id == uniqEquipment.Id);
                        gvUniqEquipment.RefreshData();
                    },
                    () =>
                    {
                        var obj = gvUnit.GetFocusedRow() as Unit;
                        var item = new UniqEquipmentObject { UnitId = obj.Id };
                        return item;
                    },
                    () => this.SearchUniqEquipmentObject(this.gvUnit.GetFocusedRow() as Unit));

                if (helperUniqEquipment == null)
                    helperUniqEquipment = new GridHelperWF<UniqEquipmentObject>(
                        gcUniqEquipment,
                        sbAddUniqEquipment,
                        sbEditUniqEquipment,
                        sbDeleteUniqEquipment,
                        fmEditUniqEquipment.Execute,
                        strategy);

                helperUniqEquipment.AsyncUpdateData();
            }
            catch (Exception exception)
            {
                Log.AddException("error", new Exception("Ошибка загрузки страницы уникального оборудования", exception));
            }
        }

        /// <summary>
        /// Метод заполнения списка уникального оборудования и определения цвета строк оного
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
                    if (equipment.LastUnitId != obj.Id || !equipment.IsArrivedBool)
                        continue;
                    uniqEquipment.ColorForAppearance = (int)ColourEnum.Yellow; // yellow
                    list.Add(uniqEquipment);
                }
            }

            return list;
        }

        #endregion

        #region NonUniqEquipment page

        /// <summary>
        /// Ссылка на объект реализации интерфейса грида
        /// </summary>
        private GridHelperWF<NonUniqEquipmentObject> helperNonUniqEquipment = null;

        /// <summary>
        /// Метод заполнения страницы хранение на складе
        /// </summary>
        private void NonUniqEquipmentStrategy()
        {
            try
            {
                var strategy = new PlainStrategy<NonUniqEquipmentObject>(
                    (nonUniqequipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.AddNonUniqEquipmentObject(nonUniqequipment);
                        helperNonUniqEquipment.AsyncUpdateData();

                        DalContainer.WcfDataManager.NonUniqEquipmentList.Add(nonUniqequipment);
                        gvNonUniqEquipment.RefreshData();
                    },
                    (nonUniqequipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.EditNonUniqEquipmentObject(nonUniqequipment);
                        helperNonUniqEquipment.AsyncUpdateData();

                        var index = DalContainer.WcfDataManager.NonUniqEquipmentList.FindIndex(c => c.Id == nonUniqequipment.Id);
                        DalContainer.WcfDataManager.NonUniqEquipmentList[index] = nonUniqequipment;
                        gvNonUniqEquipment.RefreshData();
                    },
                    (nonUniqequipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.DeleteNonUniqEquipmentObject(nonUniqequipment);
                        helperNonUniqEquipment.AsyncUpdateData();

                        DalContainer.WcfDataManager.NonUniqEquipmentList.RemoveAll(e => e.Id == nonUniqequipment.Id);
                        gvNonUniqEquipment.RefreshData();
                    },
                    () =>
                    {
                        var obj = gvUnit.GetFocusedRow() as Unit;
                        var item = new NonUniqEquipmentObject { UnitId = obj.Id };
                        return item;
                    },
                    () => this.SearchNonUniqEquipmentObject(this.gvUnit.GetFocusedRow() as Unit));

                if (helperNonUniqEquipment == null)
                    helperNonUniqEquipment = new GridHelperWF<NonUniqEquipmentObject>(
                        gcNonUniqEquipment,
                        sbAddNonUniqEquipment,
                        sbEditNonUniqEquipment,
                        sbDeleteNonUniqEquipment,
                        fmEditNonUniqEquipment.Execute,
                        strategy);

                helperNonUniqEquipment.AsyncUpdateData();
            }
            catch (Exception exception)
            {
                Log.AddException("error", new Exception("Ошибка загрузки страницы неуникального оборудования", exception));
            }
        }

        /// <summary>
        /// Метод заполнения списка неуникального оборудования и определения цвета строк оного
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Обновление таблиц оборудования и движения
        /// </summary>
        private void RefreshTables()
        {
            var obj = new Unit();
            if (IsFirstLoading)
            {
                var items = DalContainer.WcfDataManager.UnitList;
                obj = items[0];

                IsFirstLoading = false;
            }
            else
                obj = gvUnit.GetFocusedRow() as Unit;

            if (obj == null)
                return;
            this.gcUniqEquipment.DataSource = this.SearchUniqEquipmentObject(obj);
            this.gcNonUniqEquipment.DataSource = this.SearchNonUniqEquipmentObject(obj);
        }

        /// <summary>
        /// Обновление таблиц оборудования и движения по выбранному объекту
        /// </summary>
        private void gvUnit_FocusedRowChanged(
            object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (!IsFirstLoading)
                RefreshTables();

            var obj = gvUnit.GetFocusedRow() as Unit;
            if (obj.IsStore)
            {
                lciUniqForStore.Visibility = LayoutVisibility.Always;
                lciUniqForUnit.Visibility = LayoutVisibility.Never;
                lciNonUniqForStore.Visibility = LayoutVisibility.Always;
                lciNonUniqForUnit.Visibility = LayoutVisibility.Never;
            }
            else
            {
                lciUniqForStore.Visibility = LayoutVisibility.Never;
                lciUniqForUnit.Visibility = LayoutVisibility.Always;
                lciNonUniqForStore.Visibility = LayoutVisibility.Never;
                lciNonUniqForUnit.Visibility = LayoutVisibility.Always;
            }
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
            gcUnit.DataSource = DalContainer.WcfDataManager.UnitList;

            var rowHandle = focused != null ? gvUnit.LocateByValue("Id", focused.Id)
                                            : gvUnit.LocateByValue("Id", DalContainer.WcfDataManager.UnitList[0].Id);
            if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                gvUnit.FocusedRowHandle = rowHandle;

            RefreshTables();
        }

        /// <summary>
        /// Вывод формы движения по нажатию кнопки у объекта
        /// </summary>
        private void sbViewMovementUnit_Click(object sender, EventArgs e)
        {
            var focused = gvUnit.GetFocusedRow() as Unit;
            if (focused != null)
                fmMovement.Execute(focused, null);
            else
                xMsg.Information("Выберите объект");
        }

        /// <summary>
        /// Вывод формы движения по нажатию кнопки у уникального оборудования
        /// </summary>
        private void sbViewMovementUniq_Click(object sender, EventArgs e)
        {
            var focusedEquipment = gvUniqEquipment.GetFocusedRow() as UniqEquipmentObject;
            var focusedUnit = gvUnit.GetFocusedRow() as Unit;

            if (focusedEquipment != null && focusedUnit != null)
                fmMovement.Execute(focusedUnit, DalContainer.WcfDataManager.EquipmentList.FirstOrDefault(eq => eq.Id == focusedEquipment.EquipmentId));
            else
                xMsg.Information("Выберите объект и оборудование");
        }

        /// <summary>
        /// Вывод формы движения по двойному клику на объекте
        /// </summary>
        public void gvUnit_DoubleClick(object sender, EventArgs e)
        {
            var focused = gvUnit.GetFocusedRow() as Unit;
            if (focused != null)
                fmMovement.Execute(focused, null);
        }

        /// <summary>
        /// Вывод формы движения по двойному клику на уникальном оборудовании
        /// </summary>
        private void gvUniqEquipment_DoubleClick(object sender, EventArgs e)
        {
            var focusedEquipment = gvUniqEquipment.GetFocusedRow() as UniqEquipmentObject;
            var focusedUnit = gvUnit.GetFocusedRow() as Unit;

            if (focusedEquipment != null && focusedUnit != null)
                fmMovement.Execute(focusedUnit, DalContainer.WcfDataManager.EquipmentList.FirstOrDefault(eq => eq.Id == focusedEquipment.EquipmentId));
        }

    }
}
