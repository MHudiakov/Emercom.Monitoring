using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using System.Windows.Forms;
using Init.Tools.DevExpress.UI;

namespace Init.Tools.DevExpress
{
    public class FormHelper
    {
        IBaseControl _BaseControl;
        IBaseControl BaseControl { get { return _BaseControl; } set { _BaseControl = value; } }
        GridView Gv { get; set; }
        public static string HelpFile { get; set; }

        public IEnumerable<PropertyAndValue> FilledProperties { get; set; }
        public event EventHandler AfterOpenedInNewWindow;

        public static void SetControlEvents(IBaseControl usc)
        {
            SetControlEvents(usc, null);
        }

        public static FormHelper GetFormHelper(IBaseControl cntrl)
        {
            var result = cntrl == null ? null : cntrl.FormHelper;

            if (result == null)
            {
                result = new FormHelper();

                if (cntrl != null)
                {
                    result.BaseControl = cntrl;
                    cntrl.FormHelper = result;
                }
            }

            return result;
        }


        public static void SetControlEvents(IBaseControl cntrl, IEnumerable<PropertyAndValue> filledProperties)
        {
            var fh = GetFormHelper(cntrl);

            fh.BaseControl = cntrl;
            fh.FilledProperties = filledProperties;
            fh.SetBaseControlEvents();
        }


        private void SetBaseControlEvents()
        {
            if (BaseControl == null)
                return;

            BaseControl.GotFocus += new EventHandler(IBaseControl_GotFocus);

            foreach (Control control in BaseControl.Controls)
            {
                control.KeyDown += new KeyEventHandler(IBaseControl_KeyDown);
            }
        }

        void IBaseControl_GotFocus(object sender, EventArgs e)
        {
            if (Gv != null)
                Gv.RefreshData();
        }

        public void IBaseControl_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F1)
                ShowHelp();
            if (e.KeyCode == Keys.F11)
                SwitchFullScreenMode();
            if (e.KeyCode == Keys.F12)
                OpenInNewWindow();
            else
            {
            }
        }

        public fmFullScreenContainer OpenInNewWindow()
        {
            var fm = new fmFullScreenContainer();
            fm.SetUserControl(BaseControl as UserControl);
            fm.Open();

            if (AfterOpenedInNewWindow != null)
                AfterOpenedInNewWindow(fm, new EventArgs());

            return fm;
        }

        private void SwitchFullScreenMode()
        {
        }

        public void ShowHelp()
        {
            System.Diagnostics.Process.Start(HelpFile);
        }
    }
}
