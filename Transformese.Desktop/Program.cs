using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transformese.Desktop
{
    internal static class Program
    {
        [STAThread]
        static async Task Main()
        {
            ApplicationConfiguration.Initialize();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Example: attempt to call API on startup (won't block UI)
            _ = Task.Run(async () =>
            {
                try
                {
                    using var client = new HttpClient { BaseAddress = new Uri("https://localhost:5001/") };
                    var unidades = await client.GetFromJsonAsync<object[]>("api/unidades");
                }
                catch { }
            });

            Application.Run(new Form { Text = "Transformese - Desktop", Width = 900, Height = 600 });
        }
    }
}
