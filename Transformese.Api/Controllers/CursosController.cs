using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transformese.Data;
using Transformese.Domain.Entities;
using Transformese.Api.DTOs;

namespace Transformese.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public CursosController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        // GET: api/Cursos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _db.Cursos
                .Include(c => c.Unidade)
                .Select(c => new CursoDto
                {
                    IdCurso = c.IdCurso,
                    Nome = c.Nome,
                    Descricao = c.Descricao,
                    Imagem = c.Imagem,
                    UnidadeId = c.UnidadeId,
                    UnidadeNome = c.Unidade != null ? c.Unidade.Nome : "Geral"
                })
                .ToListAsync();

            return Ok(list);
        }

        // GET: api/Cursos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var c = await _db.Cursos
                .Include(c => c.Unidade)
                .Where(x => x.IdCurso == id)
                .Select(c => new CursoDto
                {
                    IdCurso = c.IdCurso,
                    Nome = c.Nome,
                    Descricao = c.Descricao,
                    Imagem = c.Imagem,
                    UnidadeId = c.UnidadeId,
                    UnidadeNome = c.Unidade != null ? c.Unidade.Nome : "Geral"
                })
                .FirstOrDefaultAsync();

            if (c == null) return NotFound();

            return Ok(c);
        }

        // POST: api/Cursos
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Curso curso, IFormFile? arquivo)
        {
            // 1. Upload da Imagem
            if (arquivo != null && arquivo.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath ?? "wwwroot", "images", "cursos");
                Directory.CreateDirectory(uploads);
                var filename = Guid.NewGuid() + Path.GetExtension(arquivo.FileName);
                var filePath = Path.Combine(uploads, filename);
                using var stream = System.IO.File.Create(filePath);
                await arquivo.CopyToAsync(stream);
                curso.Imagem = filename;
            }
            else
            {
                curso.Imagem = "default-course.jpg";
            }

            // 2. Salvar no Banco
            _db.Cursos.Add(curso);
            await _db.SaveChangesAsync();

            // 3. CORREÇÃO CRÍTICA: Retornar um Objeto Simples (DTO)
            var dtoRetorno = new
            {
                id = curso.IdCurso,
                nome = curso.Nome,
                mensagem = "Curso criado com sucesso"
            };

            return CreatedAtAction(nameof(Get), new { id = curso.IdCurso }, dtoRetorno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] Curso curso, IFormFile? arquivo)
        {
            var cursoDb = await _db.Cursos.FindAsync(id);
            if (cursoDb == null) return NotFound();

            if (arquivo != null && arquivo.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath ?? "wwwroot", "images", "cursos");
                Directory.CreateDirectory(uploads);
                var filename = Guid.NewGuid() + Path.GetExtension(arquivo.FileName);
                var filePath = Path.Combine(uploads, filename);
                using var stream = System.IO.File.Create(filePath);
                await arquivo.CopyToAsync(stream);
                cursoDb.Imagem = filename;
            }

            cursoDb.Nome = curso.Nome;
            cursoDb.Descricao = curso.Descricao;
            cursoDb.UnidadeId = curso.UnidadeId;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var curso = await _db.Cursos.FindAsync(id);
            if (curso == null) return NotFound();
            _db.Cursos.Remove(curso);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }

    // DTO Local
    public class CursoDto
    {
        public int IdCurso { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string? Imagem { get; set; }
        public int UnidadeId { get; set; }
        public string UnidadeNome { get; set; }
    }
}