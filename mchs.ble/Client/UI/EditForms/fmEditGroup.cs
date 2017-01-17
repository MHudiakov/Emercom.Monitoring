// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmEditGroup.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Defines the fmEditGroup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Client.UI.EditForms
{
    using System.Windows.Forms;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using DevExpress.XtraLayout.Utils;

    using Init.Tools.DevExpress.UI;
    using DAL.WCF;
    using DAL.WCF.ServiceReference;

    using Init.Tools;
    using Init.Tools.UI;

    /// <summary>
    /// Форма редактирования группы оборудования
    /// </summary>
    public partial class fmEditGroup : fmBaseEditForm
    {
        /// <summary>
        /// Группа оборудования
        /// </summary>
        public Group Group { get; private set; }

        private string rootGroupName = @"Корневая группа";

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public fmEditGroup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание формы редактирования группы оборудования
        /// </summary>
        /// <returns>Группа оборудования</returns>
        public static bool Execute(Group group)
        {
            Log.Add("main", "Создание формы редактирования группы оборудования");
            using (var fm = new fmEditGroup())
            {
                fm.Group = group;
                return fm.ShowDialog() == DialogResult.OK;
            }
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        protected override void LoadData()
        {
            var listGroups = new List<Group>();

            //Добавление корневой группы к списку
            var rootGroup = new Group();
            rootGroup.Name = rootGroupName;
            listGroups.Add(rootGroup);

            if (Group.Id != 0)
            {
                teName.Text = Group.Name;
                teDescription.Text = Group.Description;

                //Формирование списка групп для выбора
                listGroups.AddRange(DalContainer.WcfDataManager.GroupList); // ServiceOperationClient.GetAllGroup());
                DeleteInnerGroups(listGroups, Group);
            }
            else
            {
                //Формирование списка групп для выбора
                listGroups.AddRange(DalContainer.WcfDataManager.GroupList); // ServiceOperationClient.GetAllGroup());
            }

            lueParentGroup.Properties.DataSource = listGroups;
            var group = listGroups.FirstOrDefault(e => e.Id == Group.ParentId);
            lueParentGroup.EditValue = group;

            base.LoadData();
        }

        /// <summary>
        /// Составление выпадающего списка групп
        /// </summary>
        /// <param name="listGroups"></param>
        /// <param name="group"></param>
        public void DeleteInnerGroups(List<Group> listGroups, Group group)
        {
            group = listGroups.FirstOrDefault(e => e.Id == group.Id);
            listGroups.Remove(group);
            foreach (var innerGroup in group.InnerGroups)
            {
                DeleteInnerGroups(listGroups, innerGroup);
            }
        }

        /// <summary>
        /// Сохранение данных формы
        /// </summary>
        protected override void SaveData()
        {
            if (string.IsNullOrEmpty(teName.Text))
                xMsg.Information("Необходимо ввести название группы оборудования");

            if (lueParentGroup.EditValue == null)
                xMsg.Information("Необходимо выбрать родительскую группу");

            Group.Name = teName.Text;
            Group.Description = teDescription.Text != null ? teDescription.Text : "";

            var group = lueParentGroup.EditValue as Group;

            if (group.Id != 0)
                Group.ParentId = group.Id;
            else
                Group.ParentId = null;

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

        private void lueParentGroup_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

    }
}