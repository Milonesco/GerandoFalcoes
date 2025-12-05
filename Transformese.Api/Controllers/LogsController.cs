using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transformese.Data;
using Transformese.Domain.Entities;

namespace Transformese.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lista os últimos 100 logs (do mais recente para o antigo)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogSistema>>> GetLogs()
        {
            return await _context.LogsSistema
                                 .OrderByDescending(l => l.DataHora)
                                 .Take(100)
                                 .ToListAsync();
        }

        // POST: Salva um novo log
        [HttpPost]
        public async Task<ActionResult<LogSistema>> PostLog(LogSistema log)
        {
            log.DataHora = DateTime.Now; // Garante a hora do servidor
            _context.LogsSistema.Add(log);
            await _context.SaveChangesAsync();
            return Ok(log);
        }
    }
}