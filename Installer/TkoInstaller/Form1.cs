using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.Diagnostics;
using System.IO.Compression;

namespace TkoInstaller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool dlDone = false;
        public string home;

        static void ExecuteCommand(string command)
        {
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = true;

            process = Process.Start(processInfo);
            process.WaitForExit();
            process.Close();
        } //thanks steinar of StackOverflow (https://stackoverflow.com/questions/5519328/executing-batch-file-in-c-sharp)

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            console.Visible = true;

            System.Threading.Thread.Sleep(800);

            console.Text += Environment.NewLine;
            console.AppendText("Getting Info");
                home = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            console.Text += Environment.NewLine;
            console.AppendText("Making directories");
                Directory.CreateDirectory("C:/Eth3r");

            console.Text += Environment.NewLine;
            console.AppendText("Downloading 7za");
                using (WebClient Wc = new WebClient())
                {
                    Wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
                    Wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
                    Wc.DownloadFileAsync(new System.Uri("https://github.com/TKO-Cuber/Eth3r_Host/raw/master/7za.exe"),
                    "c:/Eth3r/7za.exe");
                }
        }

        void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("An error ocurred while trying to download the file.");

                return;
            }
            else
            {
                console.Text += Environment.NewLine;
                console.AppendText("Downloading Assets");
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadProgressChanged += client_DownloadProgressChanged;
                        client.DownloadFileCompleted += client_DownloadFileCompleted;
                        client.DownloadFileAsync(new System.Uri("https://github.com/TKO-Cuber/Eth3r_Host/raw/master/Eth3r_Host.zip"),
                        "c:/Eth3r/assets.zip");
                    }
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            console.Text += Environment.NewLine;
            console.AppendText("Extracting assets");
                ZipFile.ExtractToDirectory("C:/Eth3r/assets.zip", "C:/Eth3r");

            console.Text += Environment.NewLine;
            console.AppendText("Downloading Launcher");
                using (WebClient launcher = new WebClient())
                {
                    progressBar1.Value = 0;
                    launcher.DownloadProgressChanged += launcher_DownloadProgressChanged;
                    launcher.DownloadFileCompleted += launcher_DownloadFileCompleted;
                    launcher.DownloadFileAsync(new System.Uri("https://github.com/TKO-Cuber/Eth3r_Host/raw/master/LaunchEth3r.exe"),
                    home + "/Eth3r.exe");
                }
        }

        void launcher_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void launcher_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Eth3r has been installed to your pc. Please launch Eth3r from Desktop to begin.");
        }
    }
}
