using Microsoft.AspNetCore.Mvc;
using Transformese.MVC.ViewModels;
using System.Text;
using System.Text.Json;

namespace Transformese.MVC.Controllers
{
    public class InscricaoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public InscricaoController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]);
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
                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Candidatos/inscrever", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Sucesso");
                }
                else
                {
                    // Se der erro (ex: CPF duplicado), mostra na tela
                    var erro = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Erro na inscrição: {erro}");
                    return View("Index", model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro de conexão com o servidor.");
                return View("Index", model);
            }
        }

        public IActionResult Sucesso()
        {
            return View();
        }
    }
}