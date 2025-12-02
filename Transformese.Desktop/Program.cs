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

            // Inicia com o formulário de Login, que será recriado no próximo passo
            Application.Run(new frmLogin());
        }
    }
}