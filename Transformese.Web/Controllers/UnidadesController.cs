using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Transformese.Web.Services;

namespace Transformese.Web.Controllers
{
    public class UnidadesController : Controller
    {
        private readonly ApiService _service;
        public UnidadesController(ApiService service) { _service = service; }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAsync<List<object>>("api/unidades");
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var obj = await _service.GetAsync<object>($"api/unidades/{id}");
            return View(obj);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            var data = new { Nome = form["Nome"].ToString(), Endereco = form["Endereco"].ToString() };
            var resp = await _service.PostJsonAsync("api/unidades", data);
            if (resp.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Erro");
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var obj = await _service.GetAsync<JsonElement>($"api/unidades/{id}");
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, IFormCollection form)
        {
            var data = new { Nome = form["Nome"].ToString(), Endereco = form["Endereco"].ToString() };
            var resp = await _service.PutJsonAsync($"api/unidades/{id}", data);
            if (resp.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Erro");
            var obj = await _service.GetAsync<JsonElement>($"api/unidades/{id}");
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync($"api/unidades/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
