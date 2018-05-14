using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eth3r
{
    public partial class GenerateFirmware : Form
    {
        public GenerateFirmware()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Check everything is filled out
            if (comboBox1.SelectedIndex < -1)
                {
                MessageBox.Show("You did not select an item in the 'Device' dropdown list.");
            }
            if (comboBox2.SelectedIndex < -1)
            {
                MessageBox.Show("You did not select an item in the 'Version' dropdown list.");
            }

            //Start generating the IPSW

        }
    }
}
