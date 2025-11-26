using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transformese.Data;
using Transformese.Domain.Entities;

namespace Transformese.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnidadesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public UnidadesController(ApplicationDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _db.Unidades.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var u = await _db.Unidades.FindAsync(id);
            if (u == null) return NotFound();
            return Ok(u);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Unidade unidade)
        {
            _db.Unidades.Add(unidade);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = unidade.UnidadeId }, unidade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Unidade unidade)
        {
            var u = await _db.Unidades.FindAsync(id);
            if (u == null) return NotFound();
            u.Nome = unidade.Nome;
            u.Endereco = unidade.Endereco;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var u = await _db.Unidades.FindAsync(id);
            if (u == null) return NotFound();
            _db.Unidades.Remove(u);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
