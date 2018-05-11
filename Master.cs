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
    public partial class Master : Form
    {
        public Master()
        {
            InitializeComponent();
            
        }

        private void Master_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Master_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            var si = new System.Diagnostics.ProcessStartInfo();
            si.CreateNoWindow = true;
            si.FileName = "C://bat.bat";
            System.Diagnostics.Process.Start(si);
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
