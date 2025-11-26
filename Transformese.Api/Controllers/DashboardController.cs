using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transformese.Api.DTOs;
using Transformese.Data;
using Transformese.Domain.Enums;


namespace Transformese.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("kpis")]
        public async Task<IActionResult> GetKpis()
        {
            var stats = await _context.Candidatos
                .GroupBy(c => 1)
                .Select(g => new DashboardCountsDto
                {
                    TotalInscritos = g.Count(),
                    PendenteTriagem = g.Count(c => c.Status == StatusCandidato.Inscrito || c.Status == StatusCandidato.EmAnaliseGF),
                    AguardandoEntrevista = g.Count(c => c.Status == StatusCandidato.EncaminhadoONG || c.Status == StatusCandidato.EntrevistaAgendada),
                    Aprovados = g.Count(c => c.Status == StatusCandidato.AprovadoEntrevista || c.Status == StatusCandidato.AprovadoFinalGF || c.Status == StatusCandidato.Matriculado),
                    Reprovados = g.Count(c => c.Status == StatusCandidato.ReprovadoEntrevista || c.Status == StatusCandidato.ReprovadoFinalGF || c.Status == StatusCandidato.Desistente)
                })
                .FirstOrDefaultAsync();

            return Ok(stats ?? new DashboardCountsDto());
        }
    }
}