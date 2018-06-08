using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eth3r
{
    public partial class Form3 : Form
    {
        public bool doCustomStrings;
        public string rootfskey;
        public string buildid;
        public string rootfs;
        public string updateRamdisk;

        public Form3()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            //download the firmware... first- get strings.
            string DLdevice = comboBox1.SelectedItem.ToString();
            string DLiosversion = comboBox2.SelectedItem.ToString();

            if (DLdevice == "iPhone2,1")
            {
                if (DLiosversion == "6.1.3")
                {
                    doCustomStrings = true;
                    rootfskey = "4bcdd29f167775f32fd7c6bfec2e1f2ffec9b8d7bf72832092a8be71501e347c459e9bc5";
                    buildid = "10B329";
                    rootfs = "048-2484-005.dmg";
                    updateRamdisk = "048-2506-005.dmg";
                }
            }

            //get desktop location
            string DLPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            using (WebClient wc = new WebClient())
            {
                string dl = "https://api.ipsw.me/v4/ipsw/download/" + DLdevice + "/" + buildid;
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileAsync(new System.Uri(dl),
                DLPath + "/" + DLdevice + "_" + DLiosversion + "_" + buildid + "_Restore.ipsw");
                wc.DownloadFileCompleted += wc_DownloadFileCompleted;
            }
        }

        private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Your IPSW has been downloaded and saved to your desktop.");
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
          progressBar1.Value = e.ProgressPercentage;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 frm2 = new Form2();
            frm2.Show();
        }
    }
}
