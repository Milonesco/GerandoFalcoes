using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Transformese.Web.Services;

namespace Transformese.Web.Controllers
{
    public class CursosController : Controller
    {
        private readonly ApiService _service;

        public CursosController(ApiService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var cursos = await _service.GetAsync<List<object>>("api/cursos");
            return View(cursos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var curso = await _service.GetAsync<object>($"api/cursos/{id}");
            return View(curso);
        }

        public async Task<IActionResult> Create()
        {
            // carregar unidades para select
            var unidades = await _service.GetAsync<List<object>>("api/unidades");
            ViewBag.Unidades = unidades;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection form, IFormFile? imagem)
        {
            var fields = new Dictionary<string, string>
            {
                { "Nome", form["Nome"].ToString() ?? "" },
                { "Descricao", form["Descricao"].ToString() ?? "" },
                { "UnidadeId", form["UnidadeId"].ToString() ?? "0" }
            };

            HttpResponseMessage resp;
            if (imagem != null && imagem.Length > 0)
            {
                using var ms = new MemoryStream();
                await imagem.CopyToAsync(ms);
                ms.Position = 0;
                resp = await _service.PostFormAsync("api/cursos", fields, ms, imagem.FileName, "arquivo");
            }
            else
            {
                resp = await _service.PostFormAsync("api/cursos", fields);
            }

            if (resp.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            // em caso de erro, voltar à view com mensagem
            ModelState.AddModelError("", "Erro ao criar curso");
            var unidades = await _service.GetAsync<List<object>>("api/unidades");
            ViewBag.Unidades = unidades;
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var curso = await _service.GetAsync<JsonElement>($"api/cursos/{id}");
            var unidades = await _service.GetAsync<List<object>>("api/unidades");
            ViewBag.Unidades = unidades;
            return View(curso);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, IFormCollection form, IFormFile? imagem)
        {
            var fields = new Dictionary<string, string>
            {
                { "Nome", form["Nome"].ToString() ?? "" },
                { "Descricao", form["Descricao"].ToString() ?? "" },
                { "UnidadeId", form["UnidadeId"].ToString() ?? "0" }
            };

            HttpResponseMessage resp;
            if (imagem != null && imagem.Length > 0)
            {
                using var ms = new MemoryStream();
                await imagem.CopyToAsync(ms);
                ms.Position = 0;
                resp = await _service.PutFormAsync($"api/cursos/{id}", fields, ms, imagem.FileName, "arquivo");
            }
            else
            {
                resp = await _service.PutFormAsync($"api/cursos/{id}", fields);
            }

            if (resp.IsSuccessStatusCode || resp.StatusCode == System.Net.HttpStatusCode.NoContent)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Erro ao atualizar curso");
            var unidades = await _service.GetAsync<List<object>>("api/unidades");
            ViewBag.Unidades = unidades;
            var curso = await _service.GetAsync<JsonElement>($"api/cursos/{id}");
            return View(curso);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var resp = await _service.DeleteAsync($"api/cursos/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
