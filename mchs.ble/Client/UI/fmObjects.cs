// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmObjects.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Defines the fmObjects type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Client.UI
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
    using DAL.WCF;
    using Init.Tools.DevExpress;
    using Init.Tools;
    using DAL.WCF.ServiceReference;
    using Init.Tools.DevExpress.UI.Components;
    using Client.UI.EditForms;
    using Init.Tools.UI;

    public partial class fmObjects : XtraForm
    {
        public fmObjects()
        {
            InitializeComponent();
        }

        public static bool Execute()
        {
            using (var fm = new fmObjects())
            {
                return fm.ShowDialog() == DialogResult.OK;
            }
        }

        private bool IsFirstLoading = true;

        private void fmObjects_Load(object sender, EventArgs e)
        {
            UnitStrategy();
            UniqEquipmentStrategy();
            NonUniqEquipmentStrategy();
        }

        private void fmObjects_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show(@"Вы хотите закрыть раздел объектов?", @"Внимание", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                this.DialogResult = DialogResult.OK;
            else if (result == DialogResult.No)
                e.Cancel = true;
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
            var filterBindHelperUnit = new GridFilterBindHelper(teFindUnit, gvUnit);
            filterBindHelperUnit.Init();

            try
            {
                var strategy = new PlainStrategy<Unit>(
                    (unit) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.AddUnit(unit);
                        helperUnit.AsyncUpdateData();

                    },
                    (unit) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.EditUnit(unit);
                        helperUnit.AsyncUpdateData();
                    },
                    (unit) =>
                    {
                        var listNonUniq = DalContainer.WcfDataManager.ServiceOperationClient.GetAllNonUniqEquipmentObject().Where(e => e.UnitId == unit.Id).ToList();
                        var listMovement = DalContainer.WcfDataManager.ServiceOperationClient.GetAllMovement().Where(e => e.UnitId == unit.Id).ToList();
                        var listUniq = DalContainer.WcfDataManager.ServiceOperationClient.GetAllUniqEquipmentObject().Where(e => e.UnitId == unit.Id).ToList();

                        if ((listNonUniq.Count != 0) || (listMovement.Count != 0) || (listUniq.Count != 0))
                            xMsg.Information("Невозможно удалить объект: " + unit.Name + ", т.к. список оборудования, ссылающегося на него, не пуст! (проверьте также список движения)");
                        else
                            DalContainer.WcfDataManager.ServiceOperationClient.DeleteUnit(unit);

                        helperUnit.AsyncUpdateData();
                    },
                    () =>
                    {
                        var item = new Unit();
                        return item;
                    },
                    () =>
                    {
                        var items = DalContainer.WcfDataManager.ServiceOperationClient.GetAllUnit();
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
                Log.AddException(new Exception("Ошибка загрузки страницы объектов", exception));
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
            var filterBindHelperUniqEquipment = new GridFilterBindHelper(teFindUniqEquipment, gvUniqEquipment);
            filterBindHelperUniqEquipment.Init();

            try
            {
                var strategy = new PlainStrategy<UniqEquipmentObject>(
                    (uniqEquipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.AddUniqEquipmentObject(uniqEquipment);
                        helperUniqEquipment.AsyncUpdateData();

                    },
                    (uniqEquipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.EditUniqEquipmentObject(uniqEquipment);
                        helperUniqEquipment.AsyncUpdateData();
                    },
                    (uniqEquipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.DeleteUniqEquipmentObject(uniqEquipment);
                        helperUniqEquipment.AsyncUpdateData();
                    },
                    () =>
                    {
                        var item = new UniqEquipmentObject();
                        return item;
                    },
                    () =>
                    {
                        var obj = gvUnit.GetFocusedRow() as Unit;
                        var items = DalContainer.WcfDataManager.ServiceOperationClient.GetAllUniqEquipmentObject().Where(eq => eq.UnitId == obj.Id).ToList();
                        return items;
                    });

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
                Log.AddException(new Exception("Ошибка загрузки страницы уникального оборудования", exception));
            }
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
            var filterBindHelperNonUniqEquipment = new GridFilterBindHelper(teFindNonUniqEquipment, gvNonUniqEquipment);
            filterBindHelperNonUniqEquipment.Init();

            try
            {
                var strategy = new PlainStrategy<NonUniqEquipmentObject>(
                    (nonUniqequipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.AddNonUniqEquipmentObject(nonUniqequipment);
                        helperNonUniqEquipment.AsyncUpdateData();

                    },
                    (nonUniqequipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.EditNonUniqEquipmentObject(nonUniqequipment);
                        helperNonUniqEquipment.AsyncUpdateData();
                    },
                    (nonUniqequipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.DeleteNonUniqEquipmentObject(nonUniqequipment);
                        helperNonUniqEquipment.AsyncUpdateData();
                    },
                    () =>
                    {
                        var item = new NonUniqEquipmentObject();
                        return item;
                    },
                    () =>
                    {
                        var obj = gvUnit.GetFocusedRow() as Unit;
                        var items = DalContainer.WcfDataManager.ServiceOperationClient.GetAllNonUniqEquipmentObject().Where(eq => eq.UnitId == obj.Id).ToList();
                        return items;
                    });

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
                Log.AddException(new Exception("Ошибка загрузки страницы неуникального оборудования", exception));
            }
        }

        #endregion

        private void gvUnit_FocusedRowChanged(
            object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var obj = new Unit();
            if (IsFirstLoading)
            {
                var items = DalContainer.WcfDataManager.ServiceOperationClient.GetAllUnit();
                obj = items[0];

                IsFirstLoading = false;
            }
            else
            {
                obj = gvUnit.GetFocusedRow() as Unit;
            }
            if (obj != null)
            {
                var listUniq =
                    DalContainer.WcfDataManager.ServiceOperationClient.GetAllUniqEquipmentObject()
                        .Where(eq => eq.UnitId == obj.Id)
                        .ToList();
                var listNonUniq =
                    DalContainer.WcfDataManager.ServiceOperationClient.GetAllNonUniqEquipmentObject()
                        .Where(eq => eq.UnitId == obj.Id)
                        .ToList();

                gcUniqEquipment.DataSource = listUniq.ToList();
                gcNonUniqEquipment.DataSource = listNonUniq.ToList();
            }
        }
    }
}