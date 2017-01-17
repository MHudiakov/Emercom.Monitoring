// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmMain.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Главная форма
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Init.Tools;
using DevExpress.XtraNavBar;
using System.Reflection;
using System.IO;
using Init.Tools.DevExpress;
using Init.Tools.UI;
using Client.UI.Controls;

namespace Client.UI
{
    public partial class fmMain : XtraForm
    {

        protected UserControl activeControl = null;
        public static fmMain Active = null;

        public fmMain()
        {
            Active = this;
            InitializeComponent();

            foreach (NavBarGroup item in navBarControl.Groups)
                item.Visible = true;

            foreach (NavBarItem item in navBarControl.Items)
                item.Visible = true;
        }

        private void SetCaption()
        {
            try
            {
                var date = new DateTime(2016, 5, 10).ToShortDateString().ToString();
                Text = "МЧС.RFId (Версия от " + date + ")";
            }
            catch (Exception ex)
            {
                Log.AddException(ex);
            }
        }

        private void fmMain_Load(object sender, EventArgs e)
        {
            try
            {
                RegisterControl();
                SetCaption();
            }
            catch (Exception ex)
            {
                Log.AddException(ex);
            }
        }

        private void RegisterControl()
        {
            // Мониторинг, реальное время
            var mainControl = RegisterControl(typeof(ucRealTime), nbiRealTime);

            // Мониторинг на карте
            RegisterControl(typeof(ucTripRealTime), nbiTripRealTime);

            // Объекты
            RegisterControl(typeof(ucUnits), nbiUnits);

            // История поездок
            RegisterControl(typeof(ucTripHistory), nbiTripHistory);

            // Оборудование
            RegisterControl(typeof(ucEquipments), nbiEquipments);

            // Журнал жвижения
            RegisterControl(typeof(ucMovementLog), nbiMovementLog);

            // Справочник групп оборудования
            //RegisterControl(typeof(ucCatalogkGroup), nbiCatalogkGroup);

            // Справочник оборудования
            RegisterControl(typeof(ucCatalogkEquipment), nbiCatalogkEquipment);

            // Справочник типов объектов
            RegisterControl(typeof(ucCatalogkObject), nbiCatalogkObject);

            SwitchTo(mainControl);
        }

        public UserControl RegisterControl(Type type, NavBarItem nbitem)
        {
            try
            {

                nbitem.Visible = true;

                var control = Activator.CreateInstance(type);
                nbitem.Tag = control;
                nbitem.Hint = nbitem.Caption;

                return control as UserControl;
            }
            catch (Exception ex)
            {
                Log.AddException(ex);
                return null;
            }
        }

        public void SwitchTo(UserControl control)
        {
            var baseControl = control as IBaseControl;

            if (activeControl != null)
            {
                this.splitContainerControl.Panel2.Controls.Remove(activeControl);
                var cntr = activeControl as IBaseControl;
                cntr.Deactivate();
            }

            this.splitContainerControl.Panel2.Controls.Add(control);
            control.Dock = DockStyle.Fill;

            if (baseControl != null)
                baseControl.Activate();

            ActiveControl = control;
            activeControl = control;

        }

        private void NavBarItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (e.Link.Item.Tag == null)
                    xMsg.Information("Нет данных для отображения " + e.Link.Item.Caption);
                else
                {
                    e.Link.NavBar.SelectedLink = e.Link;
                    SwitchTo(e.Link.Item.Tag as UserControl);
                }
            }
            catch (Exception ex)
            {
                Log.AddException(ex);
            }
        }

        private void splitContainerControl_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (splitContainerControl.SplitterPosition == 80)
                    splitContainerControl.SplitterPosition = 235;
                else
                    splitContainerControl.SplitterPosition = 80;
            }
            catch (Exception ex)
            {
                Log.AddException(ex);
            }
        }
    }
}