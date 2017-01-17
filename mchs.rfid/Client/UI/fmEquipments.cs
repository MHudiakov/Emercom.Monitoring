using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Init.Tools.DevExpress;
using Init.Tools.DevExpress.UI.Components;
using DAL.WCF.ServiceReference;
using DAL.WCF;
using Client.UI.EditForms;
using Init.Tools;
using Init.Tools.UI;

namespace Client.UI
{
    public partial class fmEquipments : DevExpress.XtraEditors.XtraForm
    {
        public fmEquipments()
        {
            InitializeComponent();
        }

        public static bool Execute()
        {
            using (var fm = new fmEquipments())
            {
                return fm.ShowDialog() == DialogResult.OK;
            }
        }

        private bool IsFirstLoading = true;
        
        private void fmEquipments_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show(@"Вы хотите закрыть раздел оборудования?", @"Внимание", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                this.DialogResult = DialogResult.OK;
            else if (result == DialogResult.No)
                e.Cancel = true;
        }

        private void fmEquipments_Load(object sender, EventArgs e)
        {
            EquipmentStrategy();
            MovementStrategy();
        }

        #region Equipment page

        /// <summary>
        /// Ссылка на объект реализации интерфейса грида
        /// </summary>
        private GridHelperWF<Equipment> helperEquipment = null;

        /// <summary>
        /// Метод заполнения страницы хранение на складе
        /// </summary>
        private void EquipmentStrategy()
        {
            var filterBindHelperEquipment = new GridFilterBindHelper(teFindEquipment, gvEquipment);
            filterBindHelperEquipment.Init();

            try
            {
                var strategy = new PlainStrategy<Equipment>(
                    (equipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.AddEquipment(equipment);
                        helperEquipment.AsyncUpdateData();

                    },
                    (equipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.EditEquipment(equipment);
                        helperEquipment.AsyncUpdateData();
                    },
                    (equipment) =>
                    {
                        var listUniq = DalContainer.WcfDataManager.ServiceOperationClient.GetAllUniqEquipmentObject().Where(e => e.EquipmentId == equipment.Id).ToList();
                        var listMovement = DalContainer.WcfDataManager.ServiceOperationClient.GetAllMovement().Where(e => e.EquipmentId == equipment.Id).ToList();

                        if ((listUniq.Count != 0) || (listMovement.Count != 0))
                            xMsg.Information("Невозможно удалить оборудование: " + equipment.EquipmentName + ", т.к. списки движения и уникальных объектов, ссылающиеся на него, не пусты!");
                        else
                            DalContainer.WcfDataManager.ServiceOperationClient.DeleteEquipment(equipment);

                        helperEquipment.AsyncUpdateData();
                    },
                    () =>
                    {
                        var item = new Equipment();
                        return item;
                    },
                    () =>
                    {
                        var items = DalContainer.WcfDataManager.ServiceOperationClient.GetAllEquipment();
                        var groupItems = new List<Equipment>();
                        return items;
                    });

                if (helperEquipment == null)
                    helperEquipment = new GridHelperWF<Equipment>(
                        gcEquipment,
                        sbAddEquipment,
                        sbEditEquipment,
                        sbDeleteEquipment,
                        fmEditEquipment.Execute,
                        strategy);

                helperEquipment.AsyncUpdateData();
            }
            catch (Exception exception)
            {
                Log.AddException(new Exception("Ошибка загрузки страницы хранения оборудования", exception));
            }
        }

        #endregion

        #region Movement page

        /// <summary>
        /// Ссылка на объект реализации интерфейса грида
        /// </summary>
        private GridHelperWF<Movement> helperMovement = null;

        /// <summary>
        /// Метод заполнения страницы перемещений на складе
        /// </summary>
        private void MovementStrategy()
        {
            var filterBindHelperMovement = new GridFilterBindHelper(teFindMovement, gvMovement);
            filterBindHelperMovement.Init();

            try
            {
                var strategy = new PlainStrategy<Movement>(
                    (movement) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.AddMovement(movement);
                        helperMovement.AsyncUpdateData();
                    },
                    (movement) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.EditMovement(movement);
                        helperMovement.AsyncUpdateData();
                    },
                    (movement) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.DeleteMovement(movement);
                        helperMovement.AsyncUpdateData();
                    },
                    () =>
                    {
                        var item = new Movement();
                        return item;
                    },
                    () =>
                    {
                        var equipment = gvEquipment.GetFocusedRow() as Equipment;
                        var items = DalContainer.WcfDataManager.ServiceOperationClient.GetAllMovement().Where(m => m.EquipmentId == equipment.Id).ToList();

                        if ((deFrom.EditValue != null) && (deTo.EditValue != null))
                            items = items.Where(e => e.DateOfMovement.Date >= deFrom.EditValue.ToDateTime().Date && e.DateOfMovement.Date <= deTo.EditValue.ToDateTime().Date).ToList();
                        
                        else if (deFrom.EditValue != null)
                            items = items.Where(e => e.DateOfMovement.Date >= deFrom.EditValue.ToDateTime().Date).ToList();
                        
                        else if (deTo.EditValue != null)
                            items = items.Where(e => e.DateOfMovement.Date <= deTo.EditValue.ToDateTime().Date).ToList();
                        
                        return items;
                    });

                if (helperMovement == null)
                    helperMovement = new GridHelperWF<Movement>(
                        gcMovement,
                        sbAddMovement,
                        sbEditMovement,
                        sbDeleteMovement,
                        fmEditMovement.Execute,
                        strategy);

                helperMovement.AsyncUpdateData();
            }
            catch (Exception exception)
            {
                Log.AddException(new Exception("Ошибка загрузки страницы движения оборудования", exception));
            }
        }

        #endregion

        private void sbShow_Click(object sender, EventArgs e)
        {
            if (CheckFilter())
                MovementStrategy();
            else
                xMsg.Information("В фильтре заданы неверные данные!");
        }

        private bool CheckFilter()
        {
            var check = true;

            if (deFrom.EditValue == null && deTo.EditValue == null)
                check = false;

            if ((deFrom.EditValue != null && deTo.EditValue != null) && (deFrom.DateTime >= deTo.DateTime))
                check = false;

            return check;
        }
        
        private void gvEquipment_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var equipment = new Equipment();
            if (IsFirstLoading)
            {
                var items = DalContainer.WcfDataManager.ServiceOperationClient.GetAllEquipment();
                equipment = items[0];
            
                IsFirstLoading = false;
            }
            else
            {
                equipment = gvEquipment.GetFocusedRow() as Equipment;
            }
            
            var list = DalContainer.WcfDataManager.ServiceOperationClient.GetAllMovement().Where(m => m.EquipmentId == equipment.Id).ToList();            
            gcMovement.DataSource = list.ToList();
        }



    }
}