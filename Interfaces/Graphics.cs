using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eth3r.Interfaces
{
    public class Graphics
    {
        public void setWindow(Control control)
        {
            control.Dock = DockStyle.Fill;
            control.Padding = Padding.Empty;
            Eth3r.masterWindow.View.Controls.Clear();
            Eth3r.masterWindow.View.Controls.Add(control);
        }
    }
}
