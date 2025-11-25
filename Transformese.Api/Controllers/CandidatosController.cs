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

        // ==========================================
        // 1. LISTAGEM (Painel Admin)
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // PROJEÇÃO MANUAL (.Select):
            // Isso impede que o EF Core carregue relacionamentos desnecessários ou cíclicos.
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
                    // Convertendo Enum para String aqui mesmo
                    Status = c.Status.ToString(),
                    // Tratamento de nulo seguro
                    Unidade = c.Unidade != null ? c.Unidade.Nome : "Não Atribuída",
                    DataCadastro = c.DataCadastro
                })
                .ToListAsync();

            return Ok(lista);
        }

        // ==========================================
        // 2. DETALHES (Tela de Triagem)
        // ==========================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Buscamos a entidade para ter os dados
            var c = await _context.Candidatos
                .Include(u => u.Unidade)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (c == null) return NotFound();

            // Mapeamos MANUALMENTE para um DTO seguro antes de enviar
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

                // Campos Administrativos
                UnidadeId = c.UnidadeId,
                Status = c.Status, // Aqui mandamos o Enum (int) para o Dropdown funcionar
                ObservacoesGF = c.ObservacoesGF
            };

            return Ok(dto);
        }

        // ==========================================
        // 3. INSCRIÇÃO (Portal do Aluno)
        // ==========================================
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

            // Log simplificado
            _context.CandidatoLogs.Add(new CandidatoLog
            {
                CandidatoId = candidato.Id,
                Acao = "Inscrição",
                StatusAnterior = StatusCandidato.Inscrito,
                NovoStatus = StatusCandidato.Inscrito,
                DataHora = DateTime.Now
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = candidato.Id }, candidato.Id);
        }

        // ==========================================
        // 4. ATUALIZAÇÃO (Salvar Triagem)
        // ==========================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CandidatoTriagemDto dto)
        {
            var candidato = await _context.Candidatos.FindAsync(id);
            if (candidato == null) return NotFound();

            // Auditoria da Mudança
            if (candidato.Status != dto.Status || candidato.UnidadeId != dto.UnidadeId)
            {
                var log = new CandidatoLog
                {
                    CandidatoId = candidato.Id,
                    StatusAnterior = candidato.Status,
                    NovoStatus = dto.Status,
                    Acao = "Triagem Admin",
                    Observacao = $"Status alterado para {dto.Status}. Unidade ID: {dto.UnidadeId}",
                    DataHora = DateTime.Now
                };
                _context.CandidatoLogs.Add(log);
            }

            candidato.UnidadeId = dto.UnidadeId;
            candidato.Status = dto.Status;
            candidato.ObservacoesGF = dto.ObservacoesGF;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    // ==========================================
    // DTOs (Objetos de Transferência Seguros)
    // ==========================================

    public class CandidatoResumoDto
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public string Status { get; set; } // Texto (Ex: "Inscrito")
        public string Unidade { get; set; } // Texto (Ex: "Gerando Falcões")
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

        // Dados de Edição
        public int? UnidadeId { get; set; }
        public StatusCandidato Status { get; set; } // Enum puro
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
}