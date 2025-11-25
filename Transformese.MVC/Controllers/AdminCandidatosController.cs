using Microsoft.AspNetCore.Mvc;
using Transformese.MVC.Services;

namespace Transformese.MVC.Controllers
{
    // [Authorize(Roles = "Admin")] // Vamos descomentar isso depois que a Auth estiver 100%
    public class AdminCandidatosController : Controller
    {
        private readonly ICandidatoService _service;

        public AdminCandidatosController(ICandidatoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _service.BuscarTodos();
            return View(lista);
        }
    }
}