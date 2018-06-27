using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Eth3r.Interfaces
{
    public partial class Device : UserControl
    {
        public static string deviceinfo;
        public Label test;
        public string name;
        public string model;
        public string iosversion;
        public bool DeviceError = false;

        public Device()
        {
            InitializeComponent();
        }

        public static string ExecuteCommand(string command)
        {
            int exitCode;
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();

            exitCode = process.ExitCode;
            process.Close();
            return output.ToString();
        }

        private void Device_Load_1(object sender, EventArgs e)
        {
            name= ExecuteCommand("c:/Eth3r/getinfo/name.bat");

            if (name == "ERROR: Could not connect to device")
            {
                MessageBox.Show("No device detected... did you plug it in?", "No device detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DeviceError = true;
            }

            if (name == "ERROR: Could not connect to lockdownd, error code -3")
            {
                MessageBox.Show("Could not connect to lockdownd..." + Environment.NewLine + "This error has been experienced when using Eth3r with an iOS 12 device (iOS 12 Beta 2). This issue is with 'idevicename' and not Eth3r.", "Could not connect to lowkdownd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DeviceError = true;
            }

            if (!DeviceError)
            {
                label2.Text = "Name: " + name;

                model = ExecuteCommand("c:/Eth3r/getinfo/model.bat");
                label3.Text = "Model: " + model;

                iosversion = ExecuteCommand("c:/Eth3r/getinfo/version.bat");
                label4.Text = "iOS Version: " + iosversion;
            }
        }
    }
}
