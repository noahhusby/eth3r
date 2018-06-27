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
        public static Splashcs splash;

        private static void initilizeSubsystems()
        {
            masterWindow = new Master();
            home = new Home();
            graphics = new Graphics();
            cfm = new CFM();
            splash = new Splashcs();
        }

        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(true);
            initilizeSubsystems();
            Application.EnableVisualStyles();
            //graphics.setWindow(cfm);
            Application.Run(splash);
            Application.Run(masterWindow);
        }
    }
}
