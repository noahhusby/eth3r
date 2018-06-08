using Eth3r.Interfaces;
using System;
using System.Windows.Forms;

namespace Eth3r
{
    static class Eth3r
    { 
        public static Master masterWindow; 
        public static Home home;
        public static Graphics graphics;
        public static CFM cfm;

        private static void initilizeSubsystems()
        {
            masterWindow = new Master();
            home = new Home();
            graphics = new Graphics();
            cfm = new CFM();
        }

        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(true);
            initilizeSubsystems();
            // Application.EnableVisualStyles();
            graphics.setWindow(home);
            Application.Run(masterWindow);
        }
    }
}
