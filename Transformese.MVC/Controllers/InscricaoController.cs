using Microsoft.AspNetCore.Mvc;
using Transformese.MVC.ViewModels;
using System.Text;
using System.Text.Json;

namespace Transformese.MVC.Controllers
{
    public class InscricaoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl;

        public InscricaoController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;

            // Tenta pegar a URL do appsettings. Se não achar, usa o padrão da API local.
            // IMPORTANTE: Verifique se sua API roda na porta 7001, 5000 ou 5001.
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7001";
        }

        // GET: Exibe o formulário
        public IActionResult Index()
        {
            return View();
        }

        // POST: Recebe os dados e manda para a API
        [HttpPost]
        public async Task<IActionResult> Criar(CandidatoInscricaoViewModel model)
        {
            if (!ModelState.IsValid) return View("Index", model);

            try
            {
                // 1. Cria o cliente HTTP da forma correta (via fábrica)
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_apiBaseUrl);

                // 2. Prepara os dados
                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // 3. Envia para a API
                // A rota aqui deve bater com o [HttpPost("inscrever")] da sua API
                var response = await client.PostAsync("api/Candidatos/inscrever", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Sucesso");
                }
                else
                {
                    // Erro da API (Ex: CPF duplicado)
                    var erroMsg = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Erro ao inscrever: {erroMsg}");
                    return View("Index", model);
                }
            }
            catch (Exception ex)
            {
                // Erro de conexão (API desligada ou URL errada)
                ModelState.AddModelError("", $"Erro de conexão com o servidor: {ex.Message}");
                return View("Index", model);
            }
        }

        public IActionResult Sucesso()
        {
            return View();
        }
    }
}