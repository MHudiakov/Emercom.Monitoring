using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Init.Tools.DevExpress
{
    public interface IBaseControl
    {
        void Activate();
        void Deactivate();

        FormHelper FormHelper { get; set; }
        string Header { get; }

        Control.ControlCollection Controls { get; }

        event KeyEventHandler KeyDown;
        event EventHandler GotFocus;
    }
}
