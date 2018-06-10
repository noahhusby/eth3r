using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eth3r.Interfaces
{
    public partial class Splashcs : Form
    {
        public Splashcs()
        {
            InitializeComponent();
            progress();

        }

        private void Splashcs_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Splashcs_Load(object sender, EventArgs e)
        {
            
        }
        public async void progress()
        {
            await Task.Delay(1200);
            int x = 0;
            while (x < 800)
            {
                Progress.Size = new Size(x, 5);
                x++;
            }
            await Task.Delay(1000);
            this.Close();
        }
    }
}
