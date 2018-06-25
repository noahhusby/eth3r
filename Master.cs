using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Drawing.Imaging;
using System.Text;
using Eth3r.Interfaces;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Eth3r
{
    public partial class Master : Form
    {
        public static bool configDirExists;
        public string configPath = @"C:\Program Files (x86)\Eth3r\.config";
        public string readInstructions = @"C:\Program Files (x86)\Eth3r\.config\instRead";
        public bool continueExecution = true;
        public static Master master;
        public static Interfaces.Graphics g;


        public Master()
        {
            InitializeComponent();
        }

        private static void initilizeSubsystems()
        {
            master = new Master();
            g = new Interfaces.Graphics();
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


        public static string Exec(string command)
        {
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(command);
            //psi.Arguments = arguments;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            //psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            process.StartInfo = psi;

            StringBuilder output = new StringBuilder();
            process.OutputDataReceived += (sender, e) => { output.AppendLine(e.Data); };
            process.ErrorDataReceived += (sender, e) => { output.AppendLine(e.Data); };

            // run the process
            process.Start();

            // start reading output to events
            process.BeginOutputReadLine();
            //process.BeginErrorReadLine();

            // wait for process to exit
            process.WaitForExit();

            //if (process.ExitCode != 0)
            //throw new Exception("Command " + psi.FileName + " returned exit code " + process.ExitCode);

            return output.ToString();
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //master.Controls.Add(device);
            //if (File.Exists("deleteme"))
            //{
            //File.Delete("deleteme");
            //}
            //string text = System.IO.File.ReadAllText(@"deleteme");
            //File.Delete("deleteme");

            Device dev = new Device();
            View.Controls.Clear();
            View.Controls.Add(dev);

            /*
            if (ExecuteCommand("ideviceinfo") == "No device found.. is it plugged in?")
            {

            }
            string output = ExecuteCommand("start ideviceinfo.bat");
            MessageBox.Show(output);
            */
        }
    }
}