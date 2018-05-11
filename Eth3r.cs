using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eth3r.Interfaces;

namespace Eth3r
{
     static class Eth3r
     {
        public static Master masterWindow;
        public static Welcome welcome;

        private static void initilizeSubsystems()
        {
            masterWindow = new Master();
            welcome = new Welcome();
        }

        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(true);
            initilizeSubsystems();
           // Application.EnableVisualStyles();
            Application.Run(new Master());
        }
    }
}
