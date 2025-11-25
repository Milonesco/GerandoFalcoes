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
        public async Task<IActionResult> Get() => Ok(await _db.TipoUsuarios.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var t = await _db.TipoUsuarios.FindAsync(id);
            if (t == null) return NotFound();
            return Ok(t);
        }
    }
}
