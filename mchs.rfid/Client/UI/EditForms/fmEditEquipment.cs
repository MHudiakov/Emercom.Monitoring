// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmEditEquipment.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Defines the fmEditEquipment type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Client.UI.EditForms
{
    using System.Windows.Forms;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using DevExpress.XtraLayout.Utils;
    using Init.Tools.DevExpress.UI;
    using DAL.WCF;
    using DAL.WCF.ServiceReference;
    using Init.Tools.UI;
    using Init.Tools;

    /// <summary>
    /// Форма редактирования оборудования
    /// </summary>
    public partial class fmEditEquipment : fmBaseEditForm
    {
        /// <summary>
        /// Оборудование
        /// </summary>
        public Equipment Equipment { get; private set; }

        /// <summary>
        /// Конструктор формы редактирования оборудования
        /// </summary>
        public fmEditEquipment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание формы редактирования оборудования
        /// </summary>
        /// <returns>Передаваемый экземпляр оборудование</returns>
        public static bool Execute(Equipment equipment)
        {
            Log.Add("main", "Создание формы редактирования оборудования");
            var TagList = DalContainer.WcfDataManager.TagList.Where(t => t.EquipmentId == null || t.EquipmentId == equipment.Id).ToList();
            var kEquipmentList = DalContainer.WcfDataManager.kEquipmentList;

            if (!kEquipmentList.Any())
            {
                xMsg.Information("В базе нет типов оборудования! Заполните соответствующий справочник.");
                return false;
            }

            if (equipment.Id == 0 && !TagList.Any())
            {
                xMsg.Information("Нет свободных тегов для добавления!");
                return false;
            }

            using (var fm = new fmEditEquipment())
            {
                fm.Equipment = equipment;

                return fm.ShowDialog() == DialogResult.OK;
            }
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        protected override void LoadData()
        {
            var listkEquipment = Equipment.Group.kEquipmentsList;
            luekEquipment.Properties.DataSource = listkEquipment;

            var listTag = DalContainer.WcfDataManager.TagList.Where(t => t.EquipmentId == null || t.EquipmentId == Equipment.Id).ToList();
            lueFRId.Properties.DataSource = listTag;

            if (Equipment.Id != 0)
            {
                var kequipment = listkEquipment.FirstOrDefault(e => e.Id == Equipment.kEquipmentId);
                luekEquipment.EditValue = kequipment.Id;

                var tag = listTag.FirstOrDefault(e => e.Id == Equipment.TagId);
                lueFRId.EditValue = tag.Id;

                teDescription.Text = Equipment.Description;
            }
            else
            {
                luekEquipment.EditValue = listkEquipment[0].Id;
                lueFRId.EditValue = listTag[0].Id;
            }

            base.LoadData();
        }

        /// <summary>
        /// Сохранение данных формы
        /// </summary>
        protected override void SaveData()
        {
            if (luekEquipment.EditValue == null)
                xMsg.Information("Нужно выбрать тип оборудования!");

            if (lueFRId.EditValue == null)
                xMsg.Information("Нужно ввести уникальный номер оборудования!");

            var kequipment = DalContainer.WcfDataManager.kEquipmentList.FirstOrDefault(eq => eq.Id == (int)luekEquipment.EditValue);
            Equipment.kEquipmentId = kequipment.Id;

            var tag = DalContainer.WcfDataManager.TagList.FirstOrDefault(t => t.Id == (int)lueFRId.EditValue);
            Equipment.TagId = tag.Id;

            Equipment.Description = teDescription.Text;

            base.SaveData();
        }

        /// <summary>
        /// Удаление свободных тэгов
        /// </summary>
        private void sbClear_Click(object sender, EventArgs e)
        {
            if (xMsg.MsgWithConfirmCustomText("Все свободные тэги будут удалены. Продолжить?", @"Внимание!"))
            {
                try
                {
                    lueFRId.Properties.DataSource = null;

                    var list = DalContainer.WcfDataManager.TagList.Where(t => t.EquipmentId == null).ToList();
                    foreach (var tag in list)
                        DalContainer.WcfDataManager.ServiceOperationClient.DeleteTag(tag);

                    var movList = DalContainer.WcfDataManager.ServiceOperationClient.GetMovementListByEquipmentId(null);

                    foreach (var mov in movList)
                        DalContainer.WcfDataManager.ServiceOperationClient.DeleteMovement(mov);

                    if (Equipment.Id != 0)
                    {
                        var listTag = DalContainer.WcfDataManager.TagList.Where(t => t.EquipmentId == Equipment.Id).ToList();
                        lueFRId.Properties.DataSource = listTag;
                        lueFRId.EditValue = listTag[0].Id;
                    }

                    xMsg.Information("Свободные тэги удалены.");
                }
                catch (Exception exception)
                {
                    Log.AddException("error", new Exception("Ошибка при удалении тэгов", exception));
                }
            }
        }

        /// <summary>
        /// Обновление списка тэгов
        /// </summary>
        private void sbRefresh_Click(object sender, EventArgs e)
        {
            DalContainer.WcfDataManager.RefreshCashLists();
            var listTag = DalContainer.WcfDataManager.TagList.Where(t => t.EquipmentId == null || t.EquipmentId == Equipment.Id).ToList();
            lueFRId.Properties.DataSource = listTag;

            if (Equipment.Id != 0)
            {
                var tag = listTag.FirstOrDefault(t => t.Id == Equipment.TagId);
                lueFRId.EditValue = tag.Id;
            }
        }

        /// <summary>
        /// Нажатие спец-клавиш
        /// </summary>
        /// <param name="e">Аргумент события</param>
        private void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sbSave.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                sbCancel.PerformClick();
            }
        }

        private void luekEquipment_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void lueFRId_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void teDescription_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

    }
}