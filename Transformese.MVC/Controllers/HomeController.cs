using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Transformese.MVC.Models;
using Transformese.MVC.Services; // IMPORTANTE

namespace Transformese.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICursoApiClient _cursoApiClient;

        public HomeController(ILogger<HomeController> logger, ICursoApiClient cursoApiClient)
        {
            _logger = logger;
            _cursoApiClient = cursoApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var cursos = await _cursoApiClient.GetAllAsync();
            return View(cursos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
