using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eth3r
{
    public partial class GenerateFirmware : Form
    {
        public bool ipswWasPressed;
        public DialogResult dialogSelection;
        public bool goodToGo = true;
        public string device;
        public string version;
        public string buildid;
        public string rootfskey;


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

        public void button2_Click(object sender, EventArgs e)
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

            //set the selected device and version and stuff as a string
            string device = comboBox1.SelectedItem.ToString();
            string version = comboBox2.SelectedItem.ToString();
            //MessageBox.Show(device  + " " + version + " " + "is the info");


            if (device == "iPhone2,1")
            {
                if (version == "6.1.3")
                {
                    bool doCustomStrings = true;
                    rootfskey = "4bcdd29f167775f32fd7c6bfec2e1f2ffec9b8d7bf72832092a8be71501e347c459e9bc5";
                    buildid = "10B329";
                    rootfskey = "048-2484-005.dmg";
                }
            }



            //Aaaand... start generating the IPSW if we are good to go
            if (goodToGo == true)
            {
                //we are good to go, so let's generate the ipsw, shall we?
                richTextBox1.Text += "/r/n";
                richTextBox1.Text += "Beginning IPSW generation...";
                richTextBox1.Text += "/r/n";
                richTextBox1.Text += "extracting IPSW";
                System.IO.Directory.CreateDirectory(C:\EthTemp)
                string extract = ""7za.exe", "x -oIPSW" + " " + device + "_" + version + "_" + buildid + "_Restore.ipsw""; //ESCAPE THE QUOTES HERE!
                System.IO.File.WriteAllText(@"")
            }

        }
    }
}
