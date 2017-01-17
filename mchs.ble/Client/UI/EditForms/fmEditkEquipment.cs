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
    using System.Linq;

    using Init.Tools.DevExpress.UI;
    using DAL.WCF;
    using DAL.WCF.ServiceReference;

    using Init.Tools;
    using Init.Tools.UI;

    /// <summary>
    /// Форма редактирования оборудования
    /// </summary>
    public partial class fmEditkEquipment : fmBaseEditForm
    {
        /// <summary>
        /// Оборудование
        /// </summary>
        public kEquipment kEquipment { get; private set; }

        /// <summary>
        /// Конструктор формы редактирования
        /// </summary>
        public fmEditkEquipment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание формы редактирования оборудования
        /// </summary>
        /// <returns>Оборудование</returns>
        public static bool Execute(kEquipment kequipment)
        {
            Log.Add("main", "Создание формы редактирования оборудования");
            using (var fm = new fmEditkEquipment())
            {
                fm.kEquipment = kequipment;
                return fm.ShowDialog() == DialogResult.OK;
            }
        }

        /// <summary>
        /// Загрузка данных
        /// </summary>
        protected override void LoadData()
        {
            //Добавляем группы в lueGroup
            var listGroups = DalContainer.WcfDataManager.GroupList; // ServiceOperationClient.GetAllGroup();
            lueGroup.Properties.DataSource = listGroups;

            var group = listGroups.FirstOrDefault(e => e.Id == kEquipment.GroupId);
            lueGroup.EditValue = group;

            if (kEquipment.Id != 0)
            {
                teName.Text = kEquipment.Name;
                teDescription.Text = kEquipment.Description;
                ceIsUniq.Checked = kEquipment.IsUniq;
            }

            base.LoadData();
        }

        /// <summary>
        /// Сохранение данных
        /// </summary>
        protected override void SaveData()
        {
            if (string.IsNullOrEmpty(teName.Text))
                xMsg.Information("Нужно ввести название оборудования");

            if (lueGroup.EditValue == null)
                xMsg.Information("Нужно выбрать группу оборудования");

            kEquipment.Name = teName.Text;
            kEquipment.Description = teDescription.Text != null ? teDescription.Text : "";

            var group = lueGroup.EditValue as Group;
            kEquipment.GroupId = group.Id;
            kEquipment.IsUniq = ceIsUniq.Checked;

            base.SaveData();
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

        private void teName_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void teDescription_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void lueGroup_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void ceIsUniq_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }
    }
}