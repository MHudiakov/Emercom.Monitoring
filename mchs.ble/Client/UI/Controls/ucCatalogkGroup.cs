// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ucCatalogkGroup.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Справочник групп оборудования
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
    public partial class ucCatalogkGroup : XtraUserControl, IBaseControl
    {
        public FormHelper FormHelper { get; set; }
        public string Header { get; set; }

        public ucCatalogkGroup()
        {
            InitializeComponent();
        }

        public void Activate()
        {
            GroupStrategy();
            Log.Add("main", "Загрузка формы справочника групп оборудования");
        }

        public void Deactivate()
        { }

        /// <summary>
        /// Ссылка на объект реализации интерфейса TreeView
        /// </summary>
        private TreeHelperWF<Group> helperGroup;
        
        private bool _isGroupStrategyCreated = false;
        /// <summary>
        /// Метод формирования групп оборудования
        /// </summary>
        private void GroupStrategy()
        {
            try
            {
                if (_isGroupStrategyCreated)
                    return;

                _isGroupStrategyCreated = true;

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
                Log.Add("main", "Загрузка формы групп");
            }
            catch (Exception exception)
            {
                Log.AddException("error", new Exception("Ошибка загрузки списка групп оборудования", exception));
            }
        }

    }
}
