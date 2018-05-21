using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
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
        public string iosversion;
        public string buildid;
        private string rootfs;
        public string rootfskey;
        private bool stockFound;
        public string stockPath;
        public bool IsAdministrator;
        public static StringBuilder output = new StringBuilder();
        public static Process process;
        public string sn0Path;
        public string updateRamdisk;

        public GenerateFirmware()
        {
            InitializeComponent();
        }


        public static void ExecuteCommand(string command)
        {
            int exitCode;
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = true;
            // *** Redirect the output ***
            //processInfo.RedirectStandardError = true;
            //processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            // Warning: This approach can lead to deadlocks, see Edit #2
            //string output = process.StandardOutput.ReadToEnd();
            //string error = process.StandardError.ReadToEnd();

            //exitCode = process.ExitCode;

            //Console.WriteLine("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            //Console.WriteLine("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            //Console.WriteLine("ExitCode: " + exitCode.ToString(), "ExecuteCommand");
            process.Close();

        } //this code courtesy of steinar of StackOverflow
          // https://stackoverflow.com/questions/5519328/executing-batch-file-in-c-sharp

        public void button1_Click_1(object sender, EventArgs e)
        {
            ipswWasPressed = true; //set this for later during the ipsw generation
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "iPhone Software Files (IPSW)|*.ipsw";
            openFileDialog1.Title = "Select your Sn0wbreeze IPSW";
            DialogResult dialogSelection = openFileDialog1.ShowDialog();
            sn0Path = openFileDialog1.FileName;
        }

        public void button3_Click(object sender, EventArgs e)
        {
            stockFound = true; //set this for later during the ipsw generation
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Filter = "iPhone Software Files (IPSW)|*.ipsw";
            openFileDialog2.Title = "Select your Stock IPSW";
            DialogResult dialogSelection = openFileDialog2.ShowDialog();
            stockPath = openFileDialog2.FileName;
            //MessageBox.Show(stockPath);
        }

        public void button2_Click(object sender, EventArgs e)
        {

            //here comes the real stuff


            //set the selected device and version and stuff as a string
            string device = comboBox1.SelectedItem.ToString();
            string iosversion = comboBox2.SelectedItem.ToString();
            //MessageBox.Show(device  + " " + version + " " + "is the info");

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
                MessageBox.Show("You did not select a Sn0wbreeze IPSW!");
                goodToGo = false;
            }
            if (stockFound == false)
            {
                MessageBox.Show("You did not select a Stock IPSW!");
                goodToGo = false;
            }
                if (dialogSelection == DialogResult.Cancel)
            {
                MessageBox.Show("You pressed the select IPSW button, but did not select an IPSW!");
                goodToGo = false;
            }
            if (checkBox1.Checked)
            {
                if (device != "iPhone2,1")
                {
                    MessageBox.Show("Jailbreaking is currently only supported on the iPhone2,1", "Eth3r", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goodToGo = false;
                }
                if (iosversion != "6.1.3")
                {
                    MessageBox.Show("Jailbreaking is currently only supported on iOS 6.1.3", "Eth3r", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goodToGo = false;
                }
            }
            if (checkBox2.Checked)
            {
                MessageBox.Show("Bypassing Setup.app will be coming to Eth3r shortly. Proceeding without bypassing Setup.app", "Eth3r");
            }


            //define all the stuff
            if (device == "iPhone2,1")
            {
                if (iosversion == "6.1.3")
                {
                    bool doCustomStrings = true;
                    bool doDeleteUpdateRamdisk = true;
                    rootfskey = "4bcdd29f167775f32fd7c6bfec2e1f2ffec9b8d7bf72832092a8be71501e347c459e9bc5";
                    buildid = "10B329";
                    rootfs = "048-2484-005.dmg";
                    updateRamdisk = "048-2506-005.dmg";
                }
            }

            if (goodToGo == true)
            {
                //we are good to go, so let's generate the ipsw, shall we?
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "Beginning IPSW generation...";
                richTextBox1.Text += Environment.NewLine;

                //first we are going to create our temp path, then define the commands.
                string temp = @"c:\EthTemp";
                Directory.CreateDirectory(temp);
                string del = "IPSW";
                bool directoryExists = Directory.Exists(del);
                string getrid = "SN0";
                bool Sn0exists = Directory.Exists(getrid);
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string home = Directory.GetCurrentDirectory();
                    string extract = "7za.exe x -oIPSW " + "\"" + stockPath + "\"";
                    string prepRootFS = "cd IPSW && rename " + rootfs + " rootfs.dmg";
                    string decrypt = "dmg.exe extract \"IPSW/rootfs.dmg\" \"IPSW/decrootfs.dmg\" -k " + rootfskey;
                    string rebuildRootFilesystem = "dmg.exe build \"IPSW/decrootfs.dmg\" \"IPSW/" + rootfs + "\"";
                    string extractSn0wbreezeIPSW = "7za.exe x -oSN0 " + "\"" + sn0Path + "\"";
                    string removeSn0wbreezeRootFS = "cd SN0 && del " + rootfs;
                    string copyEth3rDMG = "cd IPSW && copy " + rootfs + " " + "../SN0 && pause";
                    string deleteUnrequiredFiles = "cd SN0 && del BuildManifest.plist";
                    string move7zip = "copy 7za.exe SN0";
                    string createIPSW = "cd SN0 && 7za.exe u -tzip -mx0 \"Eth3r_" + device + "_" + iosversion + "_" + buildid + ".ipsw\" -x!7za.exe";
                    string moveTheFinalProduct = "cd SN0 && move \"Eth3r_" + device + "_" + iosversion + "_" + buildid + ".ipsw\" " + "\"" + desktopPath + "\"";
                    string cleanUpCleanUpEverybodyEverywhereCleanUpCleanUpEverybodyDoYourShare = "del SN0 /y && del IPSW /y";

                //MessageBox.Show(extract + "   " + prepRootFS + "   " + decrypt + "   " + deleteUpdateRamdisk);

  
                    if (directoryExists)
                    {
                        Directory.Delete(del, true);
                    }
                    if (Sn0exists)
                    {
                        Directory.Delete(getrid, true);
                    }
                //MessageBox.Show(extract + Environment.NewLine + prepRootFS + Environment.NewLine + decrypt + Environment.NewLine + rebuildRootFilesystem + Environment.NewLine + extractSn0wbreezeIPSW + Environment.NewLine + removeSn0wbreezeRootFS + Environment.NewLine + copyEth3rDMG + Environment.NewLine + deleteUnrequiredFiles + Environment.NewLine + move7zip + Environment.NewLine + createIPSW + Environment.NewLine + moveTheFinalProduct + Environment.NewLine + cleanUpCleanUpEverybodyEverywhereCleanUpCleanUpEverybodyDoYourShare + Environment.NewLine);
                richTextBox1.Text += "extracting IPSW";
                    ExecuteCommand(extract);
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "Preparing RootFS";
                    ExecuteCommand(prepRootFS);
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "Decrypting RootFS (may take a while)";
                    ExecuteCommand(decrypt);

                if (checkBox1.Checked) //check if the user wants jailbreak or not
                {
                    if (device == "iPhone2,1")
                    {
                        if (iosversion == "6.1.3")
                        {
                            richTextBox1.Text += Environment.NewLine;
                            richTextBox1.Text += "Installing Cydia";
                            string instCydia = "hfsplus.exe \"IPSW/decrootfs.dmg\" untar \"Cydia.tar\" \"/\"";
                            ExecuteCommand(instCydia);
                            richTextBox1.Text += Environment.NewLine;
                            richTextBox1.Text += "Jailbreaking";
                            string jailbreakTheOS = "hfsplus.exe \"IPSW/decrootfs.dmg\" untar \"p0sixspwn.tar\" \"/\"";
                            ExecuteCommand(jailbreakTheOS);
                        }
                    }
                }
                        


                //now run all these commands
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "Rebuilding Root Filesystem";
                    ExecuteCommand(rebuildRootFilesystem);
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "Extracting Sn0wbreeze IPSW";
                    ExecuteCommand(extractSn0wbreezeIPSW);
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "Preparing to insert Eth3r DMG";
                    ExecuteCommand(removeSn0wbreezeRootFS);
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "Inserting Eth3r DMG";
                    string oldLocation = home + "/IPSW/" + rootfs;
                    string newLocation = home + "/SN0/" + rootfs;
                    File.Copy(oldLocation, newLocation);
                    //ExecuteCommand(copyEth3rDMG);
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "Removing unrequired Files";
                    ExecuteCommand(deleteUnrequiredFiles);
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "Moving Files";
                    ExecuteCommand(move7zip);
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "Creating IPSW";
                    ExecuteCommand(createIPSW);
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "Moing the final product";
                    ExecuteCommand(moveTheFinalProduct);
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "Cleaning Up";
                    ExecuteCommand(cleanUpCleanUpEverybodyEverywhereCleanUpCleanUpEverybodyDoYourShare);
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "Your IPSW has been generated and placed on your Desktop. Thank you for using Eth3r by TKO-Cuber";
            }

        }
    }
}
