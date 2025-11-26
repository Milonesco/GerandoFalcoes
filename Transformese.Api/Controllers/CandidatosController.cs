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

        // 1. LISTAGEM GERAL (ADMIN)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _context.Candidatos
                .Include(c => c.Unidade)
                .OrderByDescending(c => c.DataCadastro)
                .Select(c => new CandidatoResumoDto
                {
                    Id = c.Id,
                    NomeCompleto = c.NomeCompleto,
                    CPF = c.CPF,
                    Cidade = c.Cidade,
                    Estado = c.Estado,
                    Telefone = c.Telefone,
                    Status = c.Status.ToString(),
                    Unidade = c.Unidade != null ? c.Unidade.Nome : "Não Atribuída",
                    DataCadastro = c.DataCadastro
                })
                .ToListAsync();

            return Ok(lista);
        }

        // 2. LISTAGEM POR UNIDADE (ONG)
        [HttpGet("unidade/{unidadeId}")]
        public async Task<IActionResult> GetByUnidade(int unidadeId)
        {
            var lista = await _context.Candidatos
                .Where(c => c.UnidadeId == unidadeId)
                .OrderByDescending(c => c.DataCadastro)
                .Select(c => new CandidatoResumoDto
                {
                    Id = c.Id,
                    NomeCompleto = c.NomeCompleto,
                    CPF = c.CPF,
                    Cidade = c.Cidade,
                    Estado = c.Estado,
                    Telefone = c.Telefone,
                    Status = c.Status.ToString(),
                    Unidade = c.Unidade.Nome,
                    DataCadastro = c.DataCadastro
                })
                .ToListAsync();

            return Ok(lista);
        }

        // 3. DETALHES
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _context.Candidatos
                .Include(u => u.Unidade)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (c == null) return NotFound();

            var dto = new CandidatoDetalhesDto
            {
                Id = c.Id,
                NomeCompleto = c.NomeCompleto,
                CPF = c.CPF,
                Email = c.Email,
                Telefone = c.Telefone,
                Cidade = c.Cidade,
                Estado = c.Estado,
                PossuiComputador = c.PossuiComputador,
                PossuiInternet = c.PossuiInternet,
                PerfilLinkedin = c.PerfilLinkedin,
                UnidadeId = c.UnidadeId,
                Status = c.Status,
                ObservacoesGF = c.ObservacoesGF
            };

            return Ok(dto);
        }

        // 4. INSCRIÇÃO
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

            // _context.CandidatoLogs.Add(...) 

            return CreatedAtAction(nameof(GetById), new { id = candidato.Id }, candidato.Id);
        }

        // 5. ATUALIZAR TRIAGEM (ADMIN)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CandidatoTriagemDto dto)
        {
            var candidato = await _context.Candidatos.FindAsync(id);
            if (candidato == null) return NotFound();

            candidato.UnidadeId = dto.UnidadeId;
            candidato.Status = dto.Status;
            candidato.ObservacoesGF = dto.ObservacoesGF;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 6. ATUALIZAR STATUS RÁPIDO (ONG)
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> PatchStatus(int id, [FromBody] StatusDto dto)
        {
            var candidato = await _context.Candidatos.FindAsync(id);
            if (candidato == null) return NotFound();

            candidato.Status = dto.NovoStatus;
            if (!string.IsNullOrEmpty(dto.Observacao)) candidato.ObservacoesONG = dto.Observacao;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    // DTOs
    public class CandidatoResumoDto
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public string Status { get; set; }
        public string Unidade { get; set; }
        public DateTime DataCadastro { get; set; }
    }
    public class CandidatoDetalhesDto
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public bool PossuiComputador { get; set; }
        public bool PossuiInternet { get; set; }
        public string? PerfilLinkedin { get; set; }
        public int? UnidadeId { get; set; }
        public StatusCandidato Status { get; set; }
        public string? ObservacoesGF { get; set; }
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
        public int? CursoId { get; set; }
    }
    public class CandidatoTriagemDto
    {
        public int? UnidadeId { get; set; }
        public StatusCandidato Status { get; set; }
        public string? ObservacoesGF { get; set; }
    }
    public class StatusDto
    {
        public StatusCandidato NovoStatus { get; set; }
        public string? Observacao { get; set; }
    }
}