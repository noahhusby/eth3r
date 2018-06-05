using System;
using System.Windows.Forms;

namespace Eth3r
{
    static class Eth3r
     {
        public static Master masterWindow;

        private static void initilizeSubsystems()
        {
            masterWindow = new Master();
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
