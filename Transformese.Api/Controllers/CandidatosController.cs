using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transformese.Data;
using Transformese.Domain.Entities;
using Transformese.Domain.Enums;

namespace Transformese.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CandidatosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("inscrever")]
        public async Task<IActionResult> Inscrever([FromBody] CandidatoInscricaoDto dto)
        {
            if (string.IsNullOrEmpty(dto.CPF)) return BadRequest("CPF Obrigatório");
            var cpfLimpo = dto.CPF.Replace(".", "").Replace("-", "").Trim();

            if (await _context.Candidatos.AnyAsync(c => c.CPF == cpfLimpo))
            {
                return Conflict(new { message = "CPF já cadastrado." });
            }

            var candidato = new Candidato
            {
                NomeCompleto = dto.NomeCompleto,
                CPF = cpfLimpo,
                Email = dto.Email,
                Telefone = dto.Telefone,
                DataNascimento = dto.DataNascimento,
                Cidade = dto.Cidade,
                Estado = dto.Estado,
                PossuiComputador = dto.PossuiComputador,
                PossuiInternet = dto.PossuiInternet,
                PerfilLinkedin = dto.PerfilLinkedin,
                Status = StatusCandidato.Inscrito,
                DataCadastro = DateTime.Now
            };

            _context.Candidatos.Add(candidato);
            await _context.SaveChangesAsync();

            // ✅ CORREÇÃO: Adicionei o retorno de sucesso aqui
            return Ok(new { message = "Inscrição realizada com sucesso!", id = candidato.Id });
        }

        public class CandidatoInscricaoDto
        {
            public string NomeCompleto { get; set; }
            public string CPF { get; set; }
            public string Email { get; set; }
            public string Telefone { get; set; }
            public DateTime DataNascimento { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }
            public bool PossuiComputador { get; set; }
            public bool PossuiInternet { get; set; }
            public string? PerfilLinkedin { get; set; }
        }
    }
}