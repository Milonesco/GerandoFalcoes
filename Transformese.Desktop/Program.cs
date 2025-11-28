using System;
using System.Windows.Forms;

namespace Transformese.Desktop
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Views.Dashboard());
        }
    }
}
