// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ucCatalogkObject.cs" company="ИНИТ-центр">
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

    /// <summary>
    /// Справочник объектов
    /// </summary>
    public partial class ucCatalogkObject : XtraUserControl, IBaseControl
    {
        public FormHelper FormHelper { get; set; }
        public string Header { get; set; }

        private bool IsFirstLoading = true;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public ucCatalogkObject()
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
                IsFirstLoading = false;
                kObjectStrategy();
            }
            else
            {
                gckObject.DataSource = DalContainer.WcfDataManager.kObjectList;
            }

            tmRefresh.Enabled = true;

            Log.Add("main", "Загрузка формы справочника оборудования");
        }

        /// <summary>
        /// Дезактиватор формы
        /// </summary>
        public void Deactivate()
        {
            tmRefresh.Enabled = false;
        }

        /// <summary>
        /// Ссылка на объект реализации интерфейса грида
        /// </summary>
        private GridHelperWF<kObject> helperkObject = null;

        /// <summary>
        /// Метод заполнения страницы пользователей
        /// </summary>
        private void kObjectStrategy()
        {
            try
            {
                var strategy = new PlainStrategy<kObject>(
                    (kobject) =>
                    {
                        DalContainer.WcfDataManager.ServiceOperationClient.AddkObject(kobject);
                        helperkObject.AsyncUpdateData();

                        DalContainer.WcfDataManager.kObjectList.Add(kobject);
                        gvkObject.RefreshData();
                    },
                    (kobject) =>
                    {

                        var listObject = DalContainer.WcfDataManager.UnitList.Where(e => e.kObjectId == kobject.Id).ToList();

                        if (listObject.Count != 0)
                            xMsg.Information("Невозможно редактировать тип объектов: " + kobject.Type + ", т.к. список объектов, ссылающихся на него, не пуст!");
                        else
                        {
                            DalContainer.WcfDataManager.ServiceOperationClient.EditkObject(kobject);

                            var index = DalContainer.WcfDataManager.kObjectList.FindIndex(c => c.Id == kobject.Id);
                            DalContainer.WcfDataManager.kObjectList[index] = kobject;
                            gvkObject.RefreshData();
                        }

                        helperkObject.AsyncUpdateData();
                    },
                    (kobject) =>
                    {
                        var listObject = DalContainer.WcfDataManager.UnitList.Where(e => e.kObjectId == kobject.Id).ToList();

                        if (listObject.Count != 0)
                            xMsg.Information("Невозможно удалить тип объектов: " + kobject.Type + ", т.к. список объектов, ссылающихся на него, не пуст!");
                        else
                        {
                            DalContainer.WcfDataManager.ServiceOperationClient.DeletekObject(kobject);

                            DalContainer.WcfDataManager.kObjectList.RemoveAll(e => e.Id == kobject.Id);
                            gvkObject.RefreshData();
                        }

                        helperkObject.AsyncUpdateData();
                    },
                    () =>
                    {
                        var item = new kObject();
                        return item;
                    },
                    () =>
                    {
                        var items = DalContainer.WcfDataManager.kObjectList;
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
                Log.AddException("error", new Exception("Ошибка загрузки страницы типов объектов", exception));
            }
        }

        /// <summary>
        /// Обновление страницы
        /// </summary>
        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            var focused = gvkObject.GetFocusedRow() as kObject;
            gckObject.DataSource = DalContainer.WcfDataManager.kObjectList;

            var rowHandle = focused != null ? gvkObject.LocateByValue("Id", focused.Id)
                                            : gvkObject.LocateByValue("Id", DalContainer.WcfDataManager.kObjectList[0].Id);
            if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                gvkObject.FocusedRowHandle = rowHandle;
        }

    }
}
