using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transformese.Data;
using Transformese.Domain.Entities;

namespace Transformese.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public UsuariosController(ApplicationDbContext db, IWebHostEnvironment env) { _db = db; _env = env; }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _db.Usuarios.Include(u => u.TipoUsuario).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var u = await _db.Usuarios.Include(x => x.TipoUsuario).FirstOrDefaultAsync(x => x.IdUsuario == id);
            if (u == null) return NotFound();
            return Ok(u);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] Usuario usuario, IFormFile? arquivo)
        {
            if (arquivo != null && arquivo.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath ?? "wwwroot", "images", "usuarios");
                Directory.CreateDirectory(uploads);
                var filename = Guid.NewGuid() + Path.GetExtension(arquivo.FileName);
                var filePath = Path.Combine(uploads, filename);
                using var stream = System.IO.File.Create(filePath);
                await arquivo.CopyToAsync(stream);
                usuario.FotoPerfil = filename;
            }
            else
            {
                usuario.FotoPerfil = "default-user.jpg";
            }

            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string senha)
        {
            var user = await _db.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
            if (user == null) return Unauthorized();
            return Ok(new { user.IdUsuario, user.Nome, user.Email, user.TipoUsuarioId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] Usuario usuario, IFormFile? arquivo)
        {
            var usuarioDb = await _db.Usuarios.FindAsync(id);
            if (usuarioDb == null) return NotFound();

            if (arquivo != null && arquivo.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath ?? "wwwroot", "images", "usuarios");
                Directory.CreateDirectory(uploads);
                var filename = Guid.NewGuid() + Path.GetExtension(arquivo.FileName);
                var filePath = Path.Combine(uploads, filename);
                using var stream = System.IO.File.Create(filePath);
                await arquivo.CopyToAsync(stream);
                usuarioDb.FotoPerfil = filename;
            }

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Email = usuario.Email;
            usuarioDb.Senha = usuario.Senha;
            usuarioDb.DataNascimento = usuario.DataNascimento;
            usuarioDb.TipoUsuarioId = usuario.TipoUsuarioId;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var u = await _db.Usuarios.FindAsync(id);
            if (u == null) return NotFound();
            _db.Usuarios.Remove(u);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
