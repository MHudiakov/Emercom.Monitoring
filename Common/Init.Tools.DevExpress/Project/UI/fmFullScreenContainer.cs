using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Init.Tools.DevExpress.UI
{
    public partial class fmFullScreenContainer : XtraForm
    {
        public fmFullScreenContainer()
        {
            InitializeComponent();
        }

        public UserControl UserControl { get; private set; }
        public IBaseControl BaseControl { get { return UserControl as IBaseControl; } }
        
        public void SetUserControl(UserControl userControl)
        {
            var type = userControl.GetType();
            UserControl = Activator.CreateInstance(type) as UserControl;

            if (BaseControl != null)
            {
                FormHelper.SetControlEvents(BaseControl);
                BaseControl.Activate();
            }

            UserControl.Dock = DockStyle.Fill;
            this.Text = BaseControl.Header;
            this.Controls.Add(UserControl);
        }

        public void Open()
        {
            this.Show();
        }

        private void fmFullScreenContainer_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (BaseControl != null)
                BaseControl.Deactivate();
        }
    }
}