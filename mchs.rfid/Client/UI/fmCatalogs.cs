// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmCatalogs.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Defines the fmCatalogs type.
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
    using Init.Tools.DevExpress;
    using DAL.WCF;
    using DAL.WCF.ServiceReference;
    using Init.Tools.UI;
    using Client.UI.EditForms;
    using Init.Tools;
    using Init.Tools.DevExpress.UI.Components;

    public partial class fmCatalogs : DevExpress.XtraEditors.XtraForm
    {
        public fmCatalogs()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание формы администрирования
        /// </summary>
        public static bool Execute()
        {
            using (var fm = new fmCatalogs())
            {
                return fm.ShowDialog() == DialogResult.OK;
            }
        }

        private void fmCatalogs_Load(object sender, EventArgs e)
        {
            GroupStrategy();
            kEquipmentStrategy();
            kObjectStrategy();
        }

        private void fmCatalogs_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show(@"Вы хотите закрыть раздел справочников?", @"Внимание", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                this.DialogResult = DialogResult.OK;
            else if (result == DialogResult.No)
                e.Cancel = true;
        }

        #region Group TreeList

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
                                DalContainer.WcfDataManager.ServiceOperationClient.DeleteGroup(group);
                            else
                                xMsg.Information("Невозможно удалить группу: " + group.Name + ", т.к. список оборудования, ссылающегося на неё, не пуст!");
                        }
                        else
                            xMsg.Information("Невозможно удалить группу: " + group.Name + ", т.к. она содержит вложенные группы");

                        helperGroup.AsyncUpdateData();
                    },
                    () =>
                    {
                        var item = new Group();
                        if (helperGroup.SelectedItem != null)
                            item.ParentId = helperGroup.SelectedItem.Id;

                        return item;
                    },
                    () => DalContainer.WcfDataManager.ServiceOperationClient.GetAllGroup());

                helperGroup = new TreeHelperWF<Group>(
                    tlGroup,
                    sbAddGroup,
                    sbEditGroup,
                    sbDeleteGroup,
                    fmEditGroup.Execute,
                    strategy);

                helperGroup.AsyncUpdateData();
            }
            catch (Exception exception)
            {
                Log.AddException(new Exception("Ошибка загрузки списка групп оборудования", exception));
            }
        }

        #endregion

        #region kEquipment page

        /// <summary>
        /// Ссылка на объект реализации интерфейса грида
        /// </summary>
        private GridHelperWF<kEquipment> helperkEquipment = null;

        /// <summary>
        /// Метод заполнения страницы пользователей
        /// </summary>
        private void kEquipmentStrategy()
        {
            var filterBindHelperkEquipment = new GridFilterBindHelper(teFindEquipment, gvkEquipment);
            filterBindHelperkEquipment.Init();

            try
            {
                var strategy = new PlainStrategy<kEquipment>(
                    (kequipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.AddkEquipment(kequipment);
                        helperkEquipment.AsyncUpdateData();
                    },
                    (kequipment) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.EditkEquipment(kequipment);
                        helperkEquipment.AsyncUpdateData();
                    },
                    (kequipment) =>
                    {
                        var listNonUniq = DalContainer.WcfDataManager.ServiceOperationClient.GetAllNonUniqEquipmentObject().Where(e => e.kEquipmentId == kequipment.Id).ToList();
                        var listMovement = DalContainer.WcfDataManager.ServiceOperationClient.GetAllMovement().Where(e => e.kEquipmentId == kequipment.Id).ToList();
                        var listEquipment = DalContainer.WcfDataManager.ServiceOperationClient.GetAllEquipment().Where(e => e.kEquipmentId == kequipment.Id).ToList();

                        if ((listNonUniq.Count != 0) || (listMovement.Count != 0) || (listEquipment.Count != 0))
                            xMsg.Information("Невозможно удалить тип оборудования: " + kequipment.Name + ", т.к. список оборудования, ссылающегося на него, не пуст! (проверьте также списки движения, уникального и неуникального оборудований)");
                        else
                            DalContainer.WcfDataManager.ServiceOperationClient.DeletekEquipment(kequipment);

                        helperkEquipment.AsyncUpdateData();
                    },
                    () =>
                    {
                        var item = new kEquipment();
                        return item;
                    },
                    () =>
                    {
                        var items = DalContainer.WcfDataManager.ServiceOperationClient.GetAllkEquipment();
                        return items;
                    });

                if (helperkEquipment == null)
                    helperkEquipment = new GridHelperWF<kEquipment>(
                        gckEquipment,
                        sbAddEquipment,
                        sbEditEquipment,
                        sbDeleteEquipment,
                        fmEditkEquipment.Execute,
                        strategy);

                helperkEquipment.AsyncUpdateData();
            }
            catch (Exception exception)
            {
                Log.AddException(new Exception("Ошибка загрузки страницы типов оборудования", exception));
            }
        }

        #endregion

        #region kObject page

        /// <summary>
        /// Ссылка на объект реализации интерфейса грида
        /// </summary>
        private GridHelperWF<kObject> helperkObject = null;

        /// <summary>
        /// Метод заполнения страницы пользователей
        /// </summary>
        private void kObjectStrategy()
        {
            var filterBindHelperkObject = new GridFilterBindHelper(teFindkObject, gvkObject);
            filterBindHelperkObject.Init();

            try
            {
                var strategy = new PlainStrategy<kObject>(
                    (kobject) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.AddkObject(kobject);
                        helperkObject.AsyncUpdateData();
                    },
                    (kobject) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.EditkObject(kobject);
                        helperkObject.AsyncUpdateData();
                    },
                    (kobject) =>
                    {
                        var listObject = DalContainer.WcfDataManager.ServiceOperationClient.GetAllUnit().Where(e => e.kObjectId == kobject.Id).ToList();

                        if (listObject.Count != 0)
                            xMsg.Information("Невозможно удалить тип объектов: " + kobject.Type + ", т.к. список объектов, ссылающихся на него, не пуст!");
                        else
                            DalContainer.WcfDataManager.ServiceOperationClient.DeletekObject(kobject);

                        helperkObject.AsyncUpdateData();
                    },
                    () =>
                    {
                        var item = new kObject();
                        return item;
                    },
                    () =>
                    {
                        var items = DalContainer.WcfDataManager.ServiceOperationClient.GetAllkObject();
                        return items;
                    });

                if (helperkObject == null)
                    helperkObject = new GridHelperWF<kObject>(
                        gckObject,
                        sbAddObject,
                        sbEditObject,
                        sbDeleteObject,
                        fmEditkObject.Execute,
                        strategy);

                helperkObject.AsyncUpdateData();
            }
            catch (Exception exception)
            {
                Log.AddException(new Exception("Ошибка загрузки страницы типов объектов", exception));
            }
        }

        #endregion


    }
}