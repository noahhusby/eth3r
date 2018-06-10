using Newtonsoft.Json;
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

        static void ExecuteCommand(string command)
        {
            int exitCode;
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;

            process = Process.Start(processInfo);
            process.WaitForExit();

            process.Close();
        } 


        string instDir = @"c:/Eth3r";
        static void LineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }


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

            //here coems the real stuff
            //set the selected device and version and stuff as a string
            device = comboBox1.SelectedItem.ToString();
            string iosversion = comboBox2.SelectedItem.ToString();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
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
                if (device != "iPhone2,1")
                {
                    MessageBox.Show("Removing Setup.app is currently only supported on the iPhone2,1", "Eth3r", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goodToGo = false;
                }
                if (iosversion != "6.1.3")
                {
                    MessageBox.Show("Removing Setup.app is currently only supported on iOS 6.1.3", "Eth3r", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goodToGo = false;
                }
            }

            //define all the stuff
            if (device == "iPhone2,1")
            {
                if (iosversion == "6.1.3")
                {
                    bool doCustomStrings = true;
                    rootfskey = "4bcdd29f167775f32fd7c6bfec2e1f2ffec9b8d7bf72832092a8be71501e347c459e9bc5";
                    buildid = "10B329";
                    rootfs = "048-2484-005.dmg";
                    updateRamdisk = "048-2506-005.dmg";
                }
            }

            if (checkBox3.Checked)
            {
                
                if (string.IsNullOrWhiteSpace(richTextBox2.Text))
                {
                    MessageBox.Show("You checked the 'custom strings' box, but you didn't fill out the box!");
                    goodToGo = false;
                }
                if (richTextBox2.Text == "Enter custom slide to unlock text if custom strings selected")
                {
                    MessageBox.Show("You selected the 'custom strings' option, but you left the box empty!");
                    goodToGo = false;
                }
                if (iosversion != "6.1.3")
                {
                    MessageBox.Show("Setting custom slide to unlock text is only supported on iPhone2,1 6.1.3");
                    goodToGo = false;
                }
                if (device != "iPhone2,1")
                {
                    MessageBox.Show("Setting custom slide to unlock text is only supported on iPhone2,1 6.1.3");
                    goodToGo = false;
                }
            }

            string CustomSlide = richTextBox2.Text;
            if (CustomSlide.Contains('"'))
            {
                MessageBox.Show("Your custom slide to unlock text appears to contain a quotation mark (\"). This could break a lot of stuff, so please try again without the quote.");
                goodToGo = false;
            }



            if (goodToGo == true)
            {
                //we are good to go, so let's generate the ipsw, shall we?

                LineChanger("\"AWAY_LOCK_LABEL\" = \"" + CustomSlide + "\";", instDir + "/SpringBoard.strings", 1);
                LineChanger("\"AWAY_LOCK_BUDDY_LABEL\" = \"" + CustomSlide + "\";" , instDir + "/SpringBoard.strings", 5);
                    


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
                string home = Directory.GetCurrentDirectory();
                    string extract = "7za.exe x -oIPSW " + "\"" + stockPath + "\"";
                    string prepRootFS = "cd IPSW && rename " + rootfs + " rootfs.dmg";
                    string decrypt = "dmg.exe extract \"IPSW/rootfs.dmg\" \"IPSW/decrootfs.dmg\" -k " + rootfskey;
                    string resizeRootFS = "hfsplus.exe \"IPSW/decrootfs.dmg\" grow 1920783616";
                    string rebuildRootFilesystem = "dmg.exe build \"IPSW/decrootfs.dmg\" \"IPSW/" + rootfs + "\"";
                    string extractSn0wbreezeIPSW = "7za.exe x -oSN0 " + "\"" + sn0Path + "\"";
                    //string removeSn0wbreezeRootFS = "cd SN0 && del " + rootfs;
                //MessageBox.Show(removeSn0wbreezeRootFS);
                    string copyEth3rDMG = "cd IPSW && copy " + rootfs + " " + "../SN0";
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
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "Resizing RootFS";
                    ExecuteCommand(resizeRootFS);

                if (checkBox1.Checked) //check if the user wants jailbreak or not
                {
                    if (device == "iPhone2,1")
                    {
                        if (iosversion == "6.1.3")
                        {
                            richTextBox1.Text += Environment.NewLine;
                            richTextBox1.Text += "Installing Cydia";
                            string instCydia = "hfsplus.exe \"IPSW/decrootfs.dmg\" untar \"Cydia.tar\" \"/\"";
                            string jailbreakTheOS = "hfsplus.exe \"IPSW/decrootfs.dmg\" untar \"p0sixspwn.tar\" \"/\"";
                            ExecuteCommand(instCydia);
                            richTextBox1.Text += Environment.NewLine;
                            richTextBox1.Text += "Jailbreaking";
                            ExecuteCommand(jailbreakTheOS);
                        }
                    }
                }

                if (checkBox3.Checked)
                {
                    if (device == "iPhone2,1")
                    {
                        if (iosversion == "6.1.3")
                        {
                            string instDir = "\"c:/Program Files (x86)/Eth3r\"";
                            richTextBox1.Text += Environment.NewLine;
                            richTextBox1.Text += "Changing 'slide to unlock' to " + "\""+ CustomSlide + "\"";
                            string changeSlide = "hfsplus \"IPSW/decrootfs.dmg\" rm \"System/Library/CoreServices/SpringBoard.app/English.lproj/SpringBoard.strings\" &&   hfsplus \"IPSW/decrootfs.dmg\" add " + instDir + "/SpringBoard.strings \"System/Library/CoreServices/SpringBoard.app/English.lproj/SpringBoard.strings\"";
                            ExecuteCommand(changeSlide);
                        }
                    }
                }

                if (checkBox2.Checked)
                {
                    if (device == "iPhone2,1")
                    {
                        if (iosversion == "6.1.3")
                        {
                            richTextBox1.Text += Environment.NewLine;
                            richTextBox1.Text += "Removing Setup.app";
                            string removeSetupApp = "hfsplus \"IPSW/decrootfs.dmg\" rm \"Applications/Setup.app/Setup\" && hfsplus \"IPSW/decrootfs.dmg\" rm \"Applications/Setup.app/PkgInfo\" && hfsplus \"IPSW/decrootfs.dmg\" rm \"Applications/Setup.app/warranty.plist\" && hfsplus \"IPSW/decrootfs.dmg\" rm \"Applications/Setup.app/Info.plist\" && hfsplus \"IPSW/decrootfs.dmg\" rm \"Applications/Setup.app/CountryAlias.plist\" && hfsplus \"IPSW/decrootfs.dmg\" rm \"Applications/Setup.app/_CodeSignature/CodeResources\" && hfsplus \"IPSW/decrootfs.dmg\" rm \"Applications/Setup.app/LanguagesByCountry.plist\"";
                            ExecuteCommand(removeSetupApp);
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
                File.Delete(home + "/SN0/" + rootfs);
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

        private void GenerateFirmware_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Master mstr = new Master();
            mstr.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }

    /**

string instDir = "\"c:/Program Files (x86)/Eth3r\"";
static void LineChanger(string newText, string fileName, int line_to_edit)
{
        string[] arrLine = File.ReadAllLines(fileName);
        arrLine[line_to_edit - 1] = newText;
        File.WriteAllLines(fileName, arrLine);

}

private void button3_Click(object sender, EventArgs e)
{
        string customSlideToUnlockText = textBox1.Text;
        MessageBox.Show("\"AWAY_LOCK_LABEL\" = \"" + customSlideToUnlockText + "\";");
                //LineChanger("\"AWAY_LOCK_LABEL\" = \"" + customSlideToUnlockText + "\";" , instDir + "/SpringBoard.strings" , 1);
        **/
}