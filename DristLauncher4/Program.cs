using System;
using System.Windows.Forms;

namespace DristLauncher4
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 256;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new exectServers());
        }
    }
}
