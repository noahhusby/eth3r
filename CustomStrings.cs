using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eth3r
{
    public partial class CustomStrings : Form
    {
        public CustomStrings()
        {
            InitializeComponent();
        }

        string instDir = "\"c:/Eth3r\"";
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
        }

        //the above is used later for editing .strings file
        //basic approach used for string editing is:
        // 1. Have template .strings file
        // 2. Ask user for custom strings text; set 'em as variable
        // 3. rewrite template using user's custom strings
        // 4. upload customized template to rootFS

    }
}
