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
    public class UsuariosController : Controller
    {
        private readonly ApiService _service;
        public UsuariosController(ApiService service) { _service = service; }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAsync<List<object>>("api/usuarios");
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var obj = await _service.GetAsync<object>($"api/usuarios/{id}");
            return View(obj);
        }

        public async Task<IActionResult> Create()
        {
            var tipos = await _service.GetAsync<List<object>>("api/tipousuarios");
            ViewBag.Tipos = tipos;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection form, IFormFile? foto)
        {
            var fields = new Dictionary<string, string>
            {
                { "Nome", form["Nome"].ToString() },
                { "Email", form["Email"].ToString() },
                { "Senha", form["Senha"].ToString() },
                { "DataNascimento", form["DataNascimento"].ToString() },
                { "TipoUsuarioId", form["TipoUsuarioId"].ToString() ?? "3" }
            };

            HttpResponseMessage resp;
            if (foto != null && foto.Length > 0)
            {
                using var ms = new MemoryStream();
                await foto.CopyToAsync(ms);
                ms.Position = 0;
                resp = await _service.PostFormAsync("api/usuarios/register", fields, ms, foto.FileName, "arquivo");
            }
            else
            {
                resp = await _service.PostFormAsync("api/usuarios/register", fields);
            }

            if (resp.IsSuccessStatusCode) return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Erro ao criar usuário");
            var tipos = await _service.GetAsync<List<object>>("api/tipousuarios");
            ViewBag.Tipos = tipos;
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _service.GetAsync<JsonElement>($"api/usuarios/{id}");
            var tipos = await _service.GetAsync<List<object>>("api/tipousuarios");
            ViewBag.Tipos = tipos;
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, IFormCollection form, IFormFile? foto)
        {
            var fields = new Dictionary<string, string>
            {
                { "Nome", form["Nome"].ToString() },
                { "Email", form["Email"].ToString() },
                { "Senha", form["Senha"].ToString() },
                { "DataNascimento", form["DataNascimento"].ToString() },
                { "TipoUsuarioId", form["TipoUsuarioId"].ToString() ?? "3" }
            };

            HttpResponseMessage resp;
            if (foto != null && foto.Length > 0)
            {
                using var ms = new MemoryStream();
                await foto.CopyToAsync(ms);
                ms.Position = 0;
                resp = await _service.PutFormAsync($"api/usuarios/{id}", fields, ms, foto.FileName, "arquivo");
            }
            else
            {
                resp = await _service.PutFormAsync($"api/usuarios/{id}", fields);
            }

            if (resp.IsSuccessStatusCode || resp.StatusCode == System.Net.HttpStatusCode.NoContent)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Erro");
            var usuario = await _service.GetAsync<JsonElement>($"api/usuarios/{id}");
            var tipos = await _service.GetAsync<List<object>>("api/tipousuarios");
            ViewBag.Tipos = tipos;
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync($"api/usuarios/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
