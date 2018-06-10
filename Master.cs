using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Drawing.Imaging;

namespace Eth3r
{
    public partial class Master : Form
    {

        public static bool configDirExists;
        public string configPath = @"C:\Program Files (x86)\Eth3r\.config";
        public string readInstructions = @"C:\Program Files (x86)\Eth3r\.config\instRead";
        public bool continueExecution = true;

        public Master()
        {
            InitializeComponent();
        }

   

        public void button3_Click(object sender, EventArgs e)
        {
            continueExecution = true;
            if (Directory.Exists(configPath) == false)
            {
                Directory.CreateDirectory(configPath);
            }
            if (File.Exists(readInstructions) == false)
            {
                File.Create(readInstructions);
            }
            MessageBox.Show("1. Create a Sn0wbreeze IPSW for the desired iOS version and device **WHICH HAS A ROOT PARTITION SIZE OF 2500MB**" + Environment.NewLine + Environment.NewLine + "2. Open Eth3r and create a custom firmware." + Environment.NewLine + Environment.NewLine + "3. Use Sn0wbreeze to enter pwned DFU mode." + Environment.NewLine + Environment.NewLine + "4. Restore to the custom Eth3r IPSW located on your desktop using iTunes" + Environment.NewLine + Environment.NewLine + "5. After the restore goes through, boot into Linux and use ipwndfu by Axi0mX to boot up" + Environment.NewLine + Environment.NewLine + "6. If Jailbreak is selected, reboot after Cydia's Stashing to fix crash." + Environment.NewLine + Environment.NewLine + "Enjoy your custom firmware :)", "Instructions for Eth3r", MessageBoxButtons.OK);
        }

     

        private void Master_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
             
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void View_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}