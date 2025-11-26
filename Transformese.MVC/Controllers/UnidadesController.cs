using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Transformese.MVC.Services;
using Transformese.Domain.Entities;

namespace Transformese.MVC.Controllers
{
    public class UnidadesController : Controller
    {
        private readonly IUnidadeApiClient _service;

        public UnidadesController(IUnidadeApiClient service)
        {
            _service = service;
        }

        // GET: Unidades (Público - Catálogo de ONGs)
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // Busca todas as unidades da API
            var unidades = await _service.GetAllAsync();
            return View(unidades);
        }

        // --- ÁREA ADMINISTRATIVA (Só Admin pode Criar/Editar) ---

        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Unidade unidade)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(unidade);
                return RedirectToAction(nameof(Index));
            }
            return View(unidade);
        }

        // Você pode adicionar Edit/Delete aqui depois seguindo a mesma lógica
    }
}