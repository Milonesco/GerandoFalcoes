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

        // POST: api/Candidatos/inscrever
        // Endpoint público para o formulário do site
        [HttpPost("inscrever")]
        public async Task<IActionResult> Inscrever([FromBody] CandidatoInscricaoDto dto)
        {
            // 1. Limpeza e Validação Básica
            if (string.IsNullOrEmpty(dto.CPF)) return BadRequest("CPF é obrigatório");

            var cpfLimpo = dto.CPF.Replace(".", "").Replace("-", "").Trim();

            // 2. REGRA DE OURO: Anti-Duplicidade
            var existe = await _context.Candidatos.AnyAsync(c => c.CPF == cpfLimpo);
            if (existe)
            {
                return Conflict(new { message = "Já existe um cadastro para este CPF." });
            }

            // 3. Criação da Entidade
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
                CursoId = dto.CursoId,

                // Definições Automáticas do Sistema
                Status = StatusCandidato.Inscrito,
                DataCadastro = DateTime.Now,
                UnidadeId = null // Será definido na triagem do Admin
            };

            // 4. Persistência (Usando Transação para garantir consistência)
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Candidatos.Add(candidato);
                await _context.SaveChangesAsync(); // Salva para gerar o ID do candidato

                // 5. Auditoria Automática
                var log = new CandidatoLog
                {
                    CandidatoId = candidato.Id,
                    StatusAnterior = StatusCandidato.Inscrito,
                    NovoStatus = StatusCandidato.Inscrito,
                    Acao = "Auto-Inscrição",
                    Observacao = "Candidato realizou cadastro pelo portal.",
                    DataHora = DateTime.Now,
                    UsuarioId = null // Foi o sistema/usuário anônimo
                };

                _context.CandidatoLogs.Add(log);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return CreatedAtAction(nameof(GetStatus), new { cpf = candidato.CPF }, new { protocolo = candidato.Id });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, "Erro ao processar inscrição: " + ex.Message);
            }
        }

        // GET: api/Candidatos/status/{cpf}
        // Para o aluno consultar "Como está meu processo?"
        [HttpGet("status/{cpf}")]
        public async Task<IActionResult> GetStatus(string cpf)
        {
            var cpfLimpo = cpf.Replace(".", "").Replace("-", "").Trim();

            var candidato = await _context.Candidatos
                .FirstOrDefaultAsync(c => c.CPF == cpfLimpo);

            if (candidato == null) return NotFound(new { message = "Candidato não encontrado." });

            return Ok(new
            {
                candidato.NomeCompleto,
                Status = candidato.Status.ToString(),
                Mensagem = "Seu cadastro está em análise pela nossa equipe."
            });
        }
    }

    // DTO (Data Transfer Object) - O que vem do formulário
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
}