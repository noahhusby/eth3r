using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eth3r.Interfaces
{
    class Graphics
    {
        public void setWindow(Control control)
        {
            control.Dock = DockStyle.Fill;
            control.Padding = Padding.Empty;
            Eth3r.masterWindow.Controls.Clear();
            Eth3r.masterWindow.Controls.Add(control);
        }
    }
}
