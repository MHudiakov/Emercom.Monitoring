// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmAdministration.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Defines the fmAdministration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Client.UI.Administration
{
    using System;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.Linq;

    using DevExpress.XtraEditors;

    using DAL.WCF;
    using DAL.WCF.ServiceReference;

    using Init.Tools;
    using Init.Tools.UI;
    using Init.Tools.DevExpress;
    using Init.Tools.DevExpress.UI.Components;

    using Client.UI.EditForms;

    public partial class fmAdministration : XtraForm
    {
        public fmAdministration()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание формы администрирования
        /// </summary>
        public static bool Execute()
        {
            using (var fm = new fmAdministration())
            {
                return fm.ShowDialog() == DialogResult.OK;
            }
        }

        private void fmAdministrationLoad(object sender, EventArgs e)
        {
            //установка значений по умолчанию для фильтра по дате
            deLastDate.EditValue = System.DateTime.Today.Date;
            deFirstDate.EditValue = DateTime.Today.AddMonths(-1);
            isDefaultData = false;

            GroupStrategy();

            //стратегия загрузки страниц
            LoadPagesStrategy();
        }

        private void tlGroupAfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            chbShowAllkEquipment.Checked = false;
            chbShowAllEquipment.Checked = false;
            chbShowAllMovement.Checked = false;

            isAllItemsShow = false;

            LoadPagesStrategy();
        }

        /// <summary>
        /// Изменение флага для отображения всего списка оборудования
        /// </summary>
        private void chbShowAllkEquipmentCheckedChanged(object sender, EventArgs e)
        {
            if (chbShowAllkEquipment.Checked)
                isAllItemsShow = true;
            else
                isAllItemsShow = false;

            kEquipmentStrategy();
        }

        /// <summary>
        /// Изменение флага для отображения всего списка складов оборудования
        /// </summary>
        private void chbShowAllEquipmentCheckedChanged(object sender, EventArgs e)
        {
            if (chbShowAllEquipment.Checked)
                isAllItemsShow = true;
            else
                isAllItemsShow = false;

            EquipmentStrategy();
        }

        /// <summary>
        /// Изменение флага для отображения всего списка движений
        /// </summary>
        private void chbShowAllMovementCheckedChanged(object sender, EventArgs e)
        {
            if (chbShowAllMovement.Checked)
                isAllItemsShow = true;
            else
                isAllItemsShow = false;

            MovementStrategy();
        }

        /// <summary>
        /// Метод загрузки стратегий заполнения страниц 
        /// </summary>
        private void LoadPagesStrategy()
        {
            kEquipmentStrategy();

            EquipmentStrategy();

            MovementStrategy();
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
                                xMsg.Information("Невозможно удалить группу: " + group.Name + ", т.к. список оборудования в ней не пуст!");
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
                    () => DalContainer.WcfDataManager.GroupRepository.List());

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
            var filterBindHelperkEquipment = new GridFilterBindHelper(teFilterkEquipment, gvkEquipment);
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
                        foreach (var movement in DalContainer.WcfDataManager.ServiceOperationClient.GetAllMovement().Where(e => e.kEquipment.Id == kequipment.Id).ToList())
                        {
                            DalContainer.WcfDataManager.ServiceOperationClient.DeleteMovement(movement);
                        }

                        foreach (var equipment in DalContainer.WcfDataManager.ServiceOperationClient.GetAllEquipment().Where(e => e.kEquipmentId == kequipment.Id).ToList())
                        {
                            DalContainer.WcfDataManager.ServiceOperationClient.DeleteEquipment(equipment);
                        }

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

                        var groupItems = new List<kEquipment>();

                        if (isAllItemsShow)
                        return items;

                        else
                        {
                            foreach (var group in helperGroup.SelectedItem.AllInnerGroups)
                            {
                                groupItems.AddRange(items.Where(e => e.GroupId == group.Id).ToList());
                            }
                                
                            return groupItems;
                        }
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
            }
            catch (Exception exception)
            {
                Log.AddException(new Exception("Ошибка загрузки страницы оборудования", exception));
            }
        }

        #endregion

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
            var filterBindHelperEquipment = new GridFilterBindHelper(teFilterEquipment, gvEquipment);
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
                        foreach (var movement in DalContainer.WcfDataManager.ServiceOperationClient.GetAllMovement().Where(e => e.EquipmentId == equipment.Id).ToList())
                        {
                            DalContainer.WcfDataManager.ServiceOperationClient.DeleteMovement(movement);
                        }

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

                        //if (isAllItemsShow)
                        return items;

                        // else
                        // {
                        //     foreach (var group in helperGroup.SelectedItem.AllInnerGroups)
                        //     {
                        //         groupItems.AddRange(items.Where(e => e.kEquipment.GroupId == group.Id).ToList());
                        //     }                               
                        //
                        //     return groupItems;
                        // }
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
            var filterBindHelperMovement = new GridFilterBindHelper(teFilterMovement, gvMovement);
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
                        var items = DalContainer.WcfDataManager.ServiceOperationClient.GetAllMovement();

                        if ((deFirstDate.EditValue != null) && (deLastDate.EditValue != null))
                            items = items.Where(e => e.DateOfMovement.Date >= deFirstDate.EditValue.ToDateTime().Date && e.DateOfMovement.Date <= deLastDate.EditValue.ToDateTime().Date).ToList();

                        else if (deFirstDate.EditValue != null)
                            items = items.Where(e => e.DateOfMovement.Date >= deFirstDate.EditValue.ToDateTime().Date).ToList();

                        else if (deLastDate.EditValue != null)
                            items = items.Where(e => e.DateOfMovement.Date <= deLastDate.EditValue.ToDateTime().Date).ToList();

                        var groupItems = new List<Movement>();

                        //if (isAllItemsShow)
                        return items;
                        // else
                        // {
                        //     foreach (var group in helperGroup.SelectedItem.AllInnerGroups)
                        //     {
                        //         groupItems.AddRange(items.Where(e => e.Equipment.kEquipment.GroupId == group.Id).ToList());
                        //     }
                        // 
                        //     return groupItems;
                        // }
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
                Log.AddException(new Exception("Ошибка загрузки страницы хранения оборудования", exception));
            }
        }

        #endregion


        /// <summary>
        /// Фильтрация движений по складу за выбранный период
        /// </summary>
        private void sbShowDataFilterClick(object sender, EventArgs e)
        {
            if (!isDefaultData)
            {
                var filterBindHelper = new GridFilterBindHelper(teFilterMovement, gvMovement);
                filterBindHelper.Init();

                MovementStrategy();
            }
        }

        private void xtcPagesSelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (e.Page.Tag.ToInt())
            {
                case 0:
                    if (chbShowAllkEquipment.Checked)
                        chbShowAllkEquipment.Checked = false;
                    else
                        kEquipmentStrategy();

                    break;

                case 1:
                    if (chbShowAllEquipment.Checked)
                        chbShowAllEquipment.Checked = false;
                    else
                        EquipmentStrategy();

                    break;

                case 2:
                    if (chbShowAllMovement.Checked)
                        chbShowAllMovement.Checked = false;
                    else
                        MovementStrategy();
                    ;
                    break;
            }
        }

        private void fmAdministrationFormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
            {
                // Спросить и продолжить или нет..
                var result = MessageBox.Show(@"Завершить редактирование?", @"Внимание", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    this.DialogResult = DialogResult.OK;
                else if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private bool isAllItemsShow = true;
        private bool isDefaultData = true;
    }
}
