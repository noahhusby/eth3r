using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eth3r.Interfaces
{
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Eth3r is a concept I thought of a while back, " +
                "when I first got interested in custom firmwares. Eth3r was originally " +
                "designed to be similar to Sn0wbreeze and PwnageTool, just supporting more " +
                "devices (theoretically) and with more options. Originally written in batch, " +
                "the only 'language' I knew at the time, Eth3r was a fun sideproject. After " +
                "a while of trial and error, I finally got a custom RootFS to work correctly when " +
                "placed in a Sn0wbreeeze IPSW. After evening out a few things, I enlisted the " +
                "help of one of my friends NX_Master to help me get started in rewriting Eth3r in " +
                "C#, and here we are now. I plan to add support for custom bootlogos, verbose boot, " +
                "full IPSW generation, and more in the future, but as I am a new developer these things " +
                "won't have an ETA. These things require time on my part, learning Asembly for " +
                "iBEC, iBSS, iBoot, ASR, etc. patches and verbose boot and the like. Eth3r is a lot of " +
                "fun to make for me, and I hope things stay this way so I continue to make this tool better. " +
                "5/13/18 TKO-Cuber", "About Eth3r", MessageBoxButtons.OK);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Create a Sn0wbreeze IPSW for the desired iOS version and device **WHICH HAS A ROOT PARTITION SIZE OF 2500MB**" + Environment.NewLine + Environment.NewLine + "2. Open Eth3r and create a custom firmware." + Environment.NewLine + Environment.NewLine + "3. Use Sn0wbreeze to enter pwned DFU mode." + Environment.NewLine + Environment.NewLine + "4. Restore to the custom Eth3r IPSW located on your desktop using iTunes" + Environment.NewLine + Environment.NewLine + "5. After the restore goes through, boot into Linux and use ipwndfu by Axi0mX to boot up" + Environment.NewLine + Environment.NewLine + "6. If Jailbreak is selected, reboot after Cydia's Stashing to fix crash." + Environment.NewLine + Environment.NewLine + "Enjoy your custom firmware :)", "Instructions for Eth3r", MessageBoxButtons.OK);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Eth3r.graphics.setWindow(Eth3r.cfm);
        }
    }
}
