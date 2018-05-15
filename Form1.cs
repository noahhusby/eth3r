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
        private bool ipswWasPressed;
        private DialogResult dialogSelection;
        private bool goodToGo;

        public GenerateFirmware()
        {
            InitializeComponent();
        }

        public void button1_Click_1(object sender, EventArgs e)
        {
            ipswWasPressed = true; //set this for later during the ipsw generation
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "iPhone Software Files (IPSW)|*.ipsw";
            openFileDialog1.Title = "Select your Sn0wbreeze IPSW";
            DialogResult dialogSelection = openFileDialog1.ShowDialog();
            string sn0Path = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //here comes the real stuff


            //Check everything is filled out/selected
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("You did not select an item in the 'Device' dropdown list.");
                goodToGo = false;
            }
            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("You did not select an item in the 'Version' dropdown list.");
                goodToGo = false;
            }
            if (ipswWasPressed == false)
            {
                MessageBox.Show("You did not select a Sn0wbeeze IPSW!");
                goodToGo = false;
            }
            if (dialogSelection == DialogResult.Cancel)
            {
                MessageBox.Show("You pressed the select IPSW button, but did not select an IPSW!");
                goodToGo = false;
            }

            //Start generating the IPSW if we are good to go

            if (goodToGo == true)
            {
                //we are good to go, so let's generate the ipsw, shall we?

                
            }

        }
    }
}
