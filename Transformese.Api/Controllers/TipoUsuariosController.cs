using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transformese.Data;
using Transformese.Domain.Entities;

namespace Transformese.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoUsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public TipoUsuariosController(ApplicationDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _db.TiposUsuarios.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var t = await _db.TiposUsuarios.FindAsync(id);
            if (t == null) return NotFound();
            return Ok(t);
        }
    }
}
