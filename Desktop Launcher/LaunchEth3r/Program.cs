/**
 *   LaunchEth3r
 *     by TKO-Cuber
 *     
 *     Literally the simplest C# program ever.
 *     Launches Eth3r from ProgramFiles to mitigate
 *     issues with launching from a shortcut.
 * 
 *     Literally no way this thing has bugs... right?
**/

using System.Diagnostics;

namespace LaunchEth3r
{
    class Program
    {
        public static void ExecuteAsAdmin(string fileName)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();
        } //thanks drgmak on StackOverflow for this snippet
          // https://stackoverflow.com/questions/2532769/how-to-start-a-process-as-administrator-mode-in-c-sharp

        static void Main()
        {
            ExecuteAsAdmin("C:/Eth3r/Eth3r.exe");
        }
    }
}