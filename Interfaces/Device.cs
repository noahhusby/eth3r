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
            //psi.Arguments = arguments;
            processInfo.CreateNoWindow = true;
            //psi.RedirectStandardError = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            // Warning: This approach can lead to deadlocks, see Edit #2
            string output = process.StandardOutput.ReadToEnd();
            //string error = process.StandardError.ReadToEnd();

            exitCode = process.ExitCode;

            //Console.WriteLine("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            //Console.WriteLine("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            //Console.WriteLine("ExitCode: " + exitCode.ToString(), "ExecuteCommand");
            process.Close();
            return output.ToString();
        }

        /*private void Device_Load(object sender, System.EventArgs e)
        {
            MessageBox.Show("test");
            deviceinfo = ExecuteCommand("ideviceinfo -h");
            MessageBox.Show(deviceinfo);
        } */

        private void Device_Load_1(object sender, EventArgs e)
        {
            name= ExecuteCommand("c:/Eth3r/getinfo/name.bat");
            label2.Text = "Name: " + name;

            model = ExecuteCommand("c:/Eth3r/getinfo/model.bat");
            label3.Text = "Model: " + model;

            iosversion = ExecuteCommand("c:/Eth3r/getinfo/version.bat");
            label4.Text = "iOS Version: " + iosversion;
        }
    }
}
