using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Transformese.Domain.Entities;
using Transformese.MVC.Services;

namespace TransformeSeMVC.Web.Controllers
{
    public class CursoController : Controller
    {
        private readonly ICursoApiClient _api;

        public CursoController(ICursoApiClient api)
        {
            _api = api;
        }

        // LISTA
        public async Task<IActionResult> Index()
        {
            var cursos = await _api.GetAllAsync();

            if (cursos == null)
                throw new Exception("Erro: API retornou NULL para listagem de cursos.");

            return View(cursos);
        }

        // DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var curso = await _api.GetByIdAsync(id);
            if (curso == null) return NotFound();

            return View(curso);
        }

        // MODAL (AJAX)
        [HttpGet]
        public async Task<IActionResult> DetalhesModal(int id)
        {
            var curso = await _api.GetByIdAsync(id);
            if (curso == null) return NotFound();

            var imagem = !string.IsNullOrEmpty(curso.Imagem)
                ? $"/images/cursos/{curso.Imagem}"
                : "/images/default-course.jpg";

            return Json(new
            {
                id = curso.IdCurso,
                titulo = curso.Nome,
                descricao = curso.Descricao,
                unidade = curso.UnidadeNome ?? "Não informada",
                imagem
            });
        }

        // CREATE
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Create(Curso curso, IFormFile? arquivo)
        {
            Stream? stream = null;
            string? fileName = null;

            if (arquivo != null && arquivo.Length > 0)
            {
                stream = arquivo.OpenReadStream();
                fileName = arquivo.FileName;
            }

            var response = await _api.CreateAsync(curso, stream, fileName);

            if (!response.IsSuccessStatusCode)
                return BadRequest("Erro ao criar curso.");

            return RedirectToAction("Index");
        }



        // EDIT
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var curso = await _api.GetByIdAsync(id);
            if (curso == null) return NotFound();

            return View(curso);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Curso curso, IFormFile? arquivo)
        {
            Stream? stream = null;
            string? fileName = null;

            if (arquivo != null && arquivo.Length > 0)
            {
                stream = arquivo.OpenReadStream();
                fileName = arquivo.FileName;
            }

            var resp = await _api.UpdateAsync(id, curso, stream, fileName);

            if (!resp.IsSuccessStatusCode)
                return BadRequest("Erro ao atualizar curso.");

            return RedirectToAction("Index");
        }


        // DELETE
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _api.DeleteAsync(id);

            if (!result.IsSuccessStatusCode)
                return BadRequest("Erro ao excluir o curso.");

            return RedirectToAction(nameof(Index));
        }
    }
}
