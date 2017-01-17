// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ucCatalogkEquipment.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Справочник типов объектов
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
    using DevExpress.XtraGrid;

    /// <summary>
    /// Справочник оборудования и его групп
    /// </summary>
    public partial class ucCatalogkEquipment : XtraUserControl, IBaseControl
    {
        public FormHelper FormHelper { get; set; }
        public string Header { get; set; }

        private bool IsFirstLoading = true;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public ucCatalogkEquipment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Активатор формы
        /// </summary>
        public void Activate()
        {
            if (IsFirstLoading)
            {
                kEquipmentStrategy();
                GroupStrategy();

                IsFirstLoading = false;
            }
            else
            {
                tlGroup.DataSource = DalContainer.WcfDataManager.GroupList;
                tlGroup.ExpandAll();

                gckEquipment.DataSource = GetkEquipmentList(tlGroup.GetDataRecordByNode(tlGroup.FocusedNode) as Group);
            }

            tmRefresh.Enabled = true;
            Log.Add("main", "Запуск формы справочника оборудования");
        }

        /// <summary>
        /// Дезактиватор формы
        /// </summary>
        public void Deactivate()
        {
            tmRefresh.Enabled = false;
        }

        #region kEquipment

        /// <summary>
        /// Ссылка на объект реализации интерфейса грида
        /// </summary>
        private GridHelperWF<kEquipment> helperkEquipment = null;

        /// <summary>
        /// Метод заполнения страницы пользователей
        /// </summary>
        private void kEquipmentStrategy()
        {
            try
            {
                var strategy = new PlainStrategy<kEquipment>(
                    (kequipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.AddkEquipment(kequipment);
                        helperkEquipment.AsyncUpdateData();

                        DalContainer.WcfDataManager.kEquipmentList.Add(kequipment);
                        gvkEquipment.RefreshData();
                    },
                    (kequipment) =>
                    {
                        var listEquipment = DalContainer.WcfDataManager.EquipmentList.Where(e => e.kEquipmentId == kequipment.Id).ToList();
                        var listNonquipment = DalContainer.WcfDataManager.NonUniqEquipmentList.Where(e => e.kEquipmentId == kequipment.Id).ToList();

                        if (listEquipment.Count != 0 && listNonquipment.Count != 0)
                            xMsg.Information("Невозможно редактировать тип оборудования: " + kequipment.Name + ", т.к. список оборудования, ссылающегося на него, не пуст! (проверьте также список неуникального оборудования)");
                        else
                        {
                            DalContainer.WcfDataManager.ServiceOperationClient.EditkEquipment(kequipment);

                            var index = DalContainer.WcfDataManager.kEquipmentList.FindIndex(c => c.Id == kequipment.Id);
                            DalContainer.WcfDataManager.kEquipmentList[index] = kequipment;
                            gvkEquipment.RefreshData();

                            helperkEquipment.AsyncUpdateData();
                        }
                    },
                    (kequipment) =>
                    {
                        var listEquipment = DalContainer.WcfDataManager.EquipmentList.Where(e => e.kEquipmentId == kequipment.Id).ToList();
                        var listNonEquipment = DalContainer.WcfDataManager.NonUniqEquipmentList.Where(e => e.kEquipmentId == kequipment.Id).ToList();

                        if (listEquipment.Count != 0 || listNonEquipment.Count != 0)
                            xMsg.Information("Невозможно удалить тип оборудования: " + kequipment.Name + ", т.к. список оборудования, ссылающегося на него, не пуст! (проверьте также список неуникального оборудования)");
                        else
                        {
                            DalContainer.WcfDataManager.ServiceOperationClient.DeletekEquipment(kequipment);

                            DalContainer.WcfDataManager.kEquipmentList.RemoveAll(e => e.Id == kequipment.Id);
                            gvkEquipment.RefreshData();

                            helperkEquipment.AsyncUpdateData();
                        }
                    },
                    () =>
                    {
                        var group = tlGroup.GetDataRecordByNode(tlGroup.FocusedNode) as Group;
                        var item = new kEquipment { GroupId = group.Id };
                        return item;
                    },
                    () =>
                    {
                        return GetkEquipmentList(tlGroup.GetDataRecordByNode(tlGroup.FocusedNode) as Group);
                    });

                if (helperkEquipment == null)
                    helperkEquipment = new GridHelperWF<kEquipment>(
                        gckEquipment,
                        sbAddkEquipment,
                        sbEditkEquipment,
                        sbDeletekEquipment,
                        fmEditkEquipment.Execute,
                        strategy);

                helperkEquipment.AsyncUpdateData();
                Log.Add("main", "Загрузка формы групп оборудлования");
            }
            catch (Exception exception)
            {
                Log.AddException("error", new Exception("Ошибка загрузки страницы типов оборудования", exception));
            }
        }

        #endregion

        #region Group

        /// <summary>
        /// Ссылка на объект реализации интерфейса TreeView
        /// </summary>
        private TreeHelperWF<Group> helperGroup;

        /// <summary>
        /// Метод формирования групп оборудования
        /// </summary>
        private void GroupStrategy()
        {
            try
            {
                var strategy = new PlainStrategy<Group>(
                    (group) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.AddGroup(group);
                        helperGroup.AsyncUpdateData();
                    },
                    (group) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.EditGroup(group);
                        helperGroup.AsyncUpdateData();
                    },
                    (group) =>
                    {
                        if (group.InnerGroups.Count == 0)
                        {
                            if (group.ListkEquipment.Count == 0)
                            {
                                DalContainer.WcfDataManager.ServiceOperationClient.DeleteGroup(group);
                                helperGroup.AsyncUpdateData();
                            }
                            else
                                xMsg.Information("Невозможно удалить группу: " + group.Name + ", т.к. список оборудования, ссылающегося на неё, не пуст!");
                        }
                        else
                            xMsg.Information("Невозможно удалить группу: " + group.Name + ", т.к. она содержит вложенные группы");
                    },
                    () =>
                    {
                        var group = tlGroup.GetDataRecordByNode(tlGroup.FocusedNode) as Group;
                        var item = new Group { ParentId = group.Id };
                        return item;
                    },
                    () =>
                    {
                        DalContainer.WcfDataManager.RefreshCashLists();
                        return DalContainer.WcfDataManager.ServiceOperationClient.GetAllGroup();
                    });

                helperGroup = new TreeHelperWF<Group>(
                    tlGroup,
                    sbAddGroup,
                    sbEditGroup,
                    sbDeleteGroup,
                    fmEditGroup.Execute,
                    strategy);

                helperGroup.AsyncUpdateData();
                Log.Add("main", "Загрузка формы типов оборудования");
            }
            catch (Exception exception)
            {
                Log.AddException("error", new Exception("Ошибка загрузки списка групп оборудования", exception));
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
        /// Обновление таблицы типов оборудования при выборе другой группы оборудования 
        /// </summary>
        private void tlGroup_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (!IsFirstLoading)
                gckEquipment.DataSource = GetkEquipmentList(tlGroup.GetDataRecordByNode(tlGroup.FocusedNode) as Group);
        }

        private void Refresh()
        {
            DalContainer.WcfDataManager.RefreshCashLists();

            var focusedkEquipment = gvkEquipment.GetFocusedRow() as kEquipment;
            var focusedGroup = tlGroup.GetDataRecordByNode(tlGroup.FocusedNode) as Group;
            var nodeGroupId = tlGroup.FocusedNode.Id;

            tlGroup.DataSource = DalContainer.WcfDataManager.GroupList;
            tlGroup.FocusedNode = tlGroup.FindNode(n => n.Id == nodeGroupId);
            tlGroup.ExpandAll();

            gckEquipment.DataSource = GetkEquipmentList(focusedGroup);
            var rowHandle = focusedkEquipment != null ? gvkEquipment.LocateByValue("Id", focusedkEquipment.Id)
                                                      : gvkEquipment.LocateByValue("Id", DalContainer.WcfDataManager.kEquipmentList[0].Id);
            if (rowHandle != GridControl.InvalidRowHandle)
                gvkEquipment.FocusedRowHandle = rowHandle;
        }

        /// <summary>
        /// Обновление страницы
        /// </summary>
        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

    }
}
