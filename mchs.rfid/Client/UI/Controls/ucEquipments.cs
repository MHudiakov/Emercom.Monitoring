// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ucEquipments.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Окно оборудования
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
    using DAL.WCF;
    using Init.Tools.UI;
    using DAL.WCF.ServiceReference;
    using Init.Tools.DevExpress.UI.Components;
    using Client.UI.EditForms;
    using Init.Tools;
    using System.Threading;
    using DevExpress.XtraTreeList.Nodes;
    using DevExpress.XtraGrid;

    /// <summary>
    /// Форма оборудования
    /// </summary>
    public partial class ucEquipments : XtraUserControl, IBaseControl
    {
        public FormHelper FormHelper { get; set; }

        public string Header { get; set; }

        private bool IsFirstLoading = true;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public ucEquipments()
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
                tlGroup.DataSource = DalContainer.WcfDataManager.GroupList;
                tlGroup.ExpandAll();

                if (IsFirstLoading)
                {
                    DalContainer.WcfDataManager.RefreshCashLists();
                    EquipmentStrategy();
                    IsFirstLoading = false;
                }
                else
                {
                    gcEquipment.DataSource = GetEquipmentList(tlGroup.GetDataRecordByNode(tlGroup.FocusedNode) as Group);
                }

                tmRefresh.Enabled = true;
                Log.Add("main", "Загрузка формы учета оборудования");
            }
            catch (Exception ex)
            {
                Log.AddException("error", new Exception("Ошибка загрузки раздела оборудования и движения", ex));
            }
        }

        /// <summary>
        /// Дезактиватор формы
        /// </summary>
        public void Deactivate()
        {
            tmRefresh.Enabled = false;
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
            try
            {
                var strategy = new PlainStrategy<Equipment>(
                    (equipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.AddEquipment(equipment);
                        helperEquipment.AsyncUpdateData();

                        DalContainer.WcfDataManager.EquipmentList.Add(equipment);
                        gvEquipment.RefreshData();
                    },
                    (equipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.EditEquipment(equipment);
                        helperEquipment.AsyncUpdateData();

                        var index = DalContainer.WcfDataManager.EquipmentList.FindIndex(c => c.Id == equipment.Id);
                        DalContainer.WcfDataManager.EquipmentList[index] = equipment;
                        gvEquipment.RefreshData();
                    },
                    (equipment) =>
                    {
                        var listUniq = DalContainer.WcfDataManager.UniqEquipmentList.Where(e => e.EquipmentId == equipment.Id).ToList();

                        if ((listUniq.Count != 0))
                            xMsg.Information(
                                "Невозможно удалить оборудование: " + equipment.EquipmentName
                                + ", т.к. список уникальных объектов, ссылающийся на него, не пуст!");
                        else
                        {
                            var listMovement =
                                DalContainer.WcfDataManager.ServiceOperationClient.GetMovementListByEquipmentId(equipment.Id);
                            foreach (var movement in listMovement)
                                DalContainer.WcfDataManager.ServiceOperationClient.DeleteMovement(movement);

                            var tag = DalContainer.WcfDataManager.TagList.FirstOrDefault(t => t.EquipmentId == equipment.Id);
                            tag.EquipmentId = null;
                            DalContainer.WcfDataManager.ServiceOperationClient.EditTag(tag);

                            DalContainer.WcfDataManager.ServiceOperationClient.DeleteEquipment(equipment);
                            DalContainer.WcfDataManager.EquipmentList.RemoveAll(e => e.Id == equipment.Id);
                            gvEquipment.RefreshData();

                            helperEquipment.AsyncUpdateData();
                        }
                    },
                    () =>
                    {
                        var group = tlGroup.GetDataRecordByNode(tlGroup.FocusedNode) as Group;
                        var item = new Equipment { Group = group };
                        return item;
                    },
                    () =>
                    {
                        return GetEquipmentList(tlGroup.GetDataRecordByNode(tlGroup.FocusedNode) as Group);
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
                Log.AddException("error", new Exception("Ошибка загрузки страницы оборудования", exception));
            }
        }

        #endregion

        /// <summary>
        /// Поиск всех типов оборудования для группы
        /// </summary>
        /// <param name="parentGroup">Группа оборудования</param>
        /// <returns>Список типов оборудования, вложенных в передаваемую группу</returns>
        private List<kEquipment> GetkEquipmentList(Group parentGroup)
        {
            var list = DalContainer.WcfDataManager.kEquipmentList.Where(k => k.GroupId == parentGroup.Id).ToList();

            var childList = DalContainer.WcfDataManager.GroupList.Where(g => g.ParentId == parentGroup.Id).ToList();
            foreach (var childGroup in childList)
                list.AddRange(GetkEquipmentList(childGroup));

            return list;
        }

        /// <summary>
        /// Поиск всего оборудования для группы
        /// </summary>
        /// <param name="group">Группа оборудования</param>
        /// <returns>Список оборудования, вложенного в передаваемую группу</returns>
        private List<Equipment> GetEquipmentList(Group group)
        {
            var items = new List<Equipment>();
            var kEquipmentList = GetkEquipmentList(group);

            foreach (var kequipment in kEquipmentList)
                items.AddRange(DalContainer.WcfDataManager.EquipmentList.Where(eq => eq.kEquipmentId == kequipment.Id).ToList());

            return items;
        }

        /// <summary>
        /// Обновление таблицы оборудования при выборе другой группы оборудования 
        /// </summary>
        private void tlGroup_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (!IsFirstLoading)
                gcEquipment.DataSource = GetEquipmentList(tlGroup.GetDataRecordByNode(tlGroup.FocusedNode) as Group);
        }

        /// <summary>
        /// Таймер обновления таблиц
        /// </summary>
        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            var focusedEquipment = gvEquipment.GetFocusedRow() as Equipment;
            var focusedGroup = tlGroup.GetDataRecordByNode(tlGroup.FocusedNode) as Group;
            var nodeGroupId = tlGroup.FocusedNode.Id;

            tlGroup.DataSource = DalContainer.WcfDataManager.GroupList;
            tlGroup.FocusedNode = tlGroup.FindNode(n => n.Id == nodeGroupId);
            tlGroup.ExpandAll();

            gcEquipment.DataSource = GetEquipmentList(focusedGroup);
            var rowHandle = focusedEquipment != null ? gvEquipment.LocateByValue("Id", focusedEquipment.Id)
                                                     : gvEquipment.LocateByValue("Id", DalContainer.WcfDataManager.EquipmentList[0].Id);
            if (rowHandle != GridControl.InvalidRowHandle)
                gvEquipment.FocusedRowHandle = rowHandle;
        }

        /// <summary>
        /// Вывод формы движения
        /// </summary>
        private void sbViewMovement_Click(object sender, EventArgs e)
        {
            var focused = gvEquipment.GetFocusedRow() as Equipment;
            if (focused != null)
                fmMovement.Execute(null, focused);
            else
                xMsg.Information("Выберите оборудование");
        }

    }
}
