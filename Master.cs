using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;

namespace Eth3r
{
    public partial class Master : Form
    {
        public Master()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            GenerateFirmware gen = new GenerateFirmware();
            gen.Show();
            Master mstr = new Master();
            //Process p = new Process();
            //ProcessStartInfo ps = new ProcessStartInfo("\\bat.bat");
            //ps.UseShellExecute = false;
            //ps.RedirectStandardInput = true;
            //p.StartInfo = ps;
            //p.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Eth3r is a concept I thought of a while back," +
                "when I first got interested in custom firmwares. Eth3r was originally" +
                "designed to be similar to Sn0wbreeze and PwnageTool, just supporting more" +
                "devices (theoretically) and with more options. Originally written in batch," +
                "the only 'language' I knew at the time, Eth3r was a fun sideproject. After" +
                "months of trial and error, I finally got a custom RootFS to work correctly when" +
                "placed in a Sn0wbreeeze IPSW. After evening out a few things, I enlisted the" +
                "help of ond of my friends NX_Master to help me get started in rewriting Eth3r in" +
                "C#, and here we are now. I plan to add support for custom bootlogos, verbose boot," +
                "full IPSW generation, and more in the future, but as I am a new developer these things" +
                "won't have an ETA. These things require time on my part, learning Asembly for" +
                "iBEC, iBSS, iBoot, ASR, etc. patches and verbose boot and the like. Eth3r is a lot of" +
                "fun to make for me, and I hope things stay this way so I continue to make this tool better." +
                "5/13/18 TKO-Cuber", "About Eth3r", MessageBoxButtons.OK);
        }

        public void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Create a Sn0wbreeze IPSW for the desired iOS version and device **WHICH HAS A ROOT PARTITION SIZE OF 2500MB**" + Environment.NewLine + Environment.NewLine + "2. Open Eth3r and create a custom firmware." + Environment.NewLine + Environment.NewLine + "3. Use Sn0wbreeze to enter pwned DFU mode." + Environment.NewLine + Environment.NewLine + "4. Restore to the custom Eth3r IPSW located on your desktop using iTunes" + Environment.NewLine + Environment.NewLine + "5. After the restore goes through, boot into Linux and use ipwndfu by Axi0mX to boot up" + Environment.NewLine + Environment.NewLine + "6. Enjoy your custom firmware :)", "Instructions for Eth3r", MessageBoxButtons.OK);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //this.Hide();
            //AboutBox1 abt = new AboutBox1();
            //abt.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature will be coming to Eth3r shortly");
        }
    }
}

//pictureBox1.Hide();
         //   label2.Hide();
         //   label1.Hide();
         //   label3.Hide();
          //  button1.Hide();
           // button2.Hide();
           // button3.Hide();
           // button4.Hide();
            
            //this.Close();
            //string input;
            //input = textBox1.Text;
            //MessageBox.Show(input);