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

        // ============================================================
        // 1. GET: Lista TODOS os candidatos (Para o Desktop)
        // ============================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidato>>> GetCandidatos()
        {
            return await _context.Candidatos.ToListAsync();
        }

        // ============================================================
        // 2. GET POR ID (Para a Triagem)
        // ============================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidato>> GetCandidato(int id)
        {
            var candidato = await _context.Candidatos.FindAsync(id);
            if (candidato == null) return NotFound();
            return candidato;
        }

        // ============================================================
        // 3. POST (DESKTOP): Salva cadastro completo
        // ============================================================
        [HttpPost]
        public async Task<ActionResult<Candidato>> PostCandidato(Candidato candidato)
        {
            if (string.IsNullOrEmpty(candidato.CPF)) return BadRequest("CPF é obrigatório.");
            var cpfLimpo = candidato.CPF.Replace(".", "").Replace("-", "").Trim();

            if (await _context.Candidatos.AnyAsync(c => c.CPF == cpfLimpo))
            {
                return Conflict(new { message = "Este CPF já está cadastrado." });
            }

            candidato.CPF = cpfLimpo;
            candidato.DataCadastro = DateTime.Now;
            if (candidato.Status == 0) candidato.Status = StatusCandidato.Inscrito;

            _context.Candidatos.Add(candidato);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCandidato), new { id = candidato.Id }, candidato);
        }

        // ============================================================
        // 4. POST (WEB - INSCRIÇÃO): Agora com TODOS os campos!
        // ============================================================
        [HttpPost("inscrever")]
        public async Task<IActionResult> Inscrever([FromBody] CandidatoInscricaoDto dto)
        {
            // 1. Validações Básicas
            if (string.IsNullOrEmpty(dto.CPF)) return BadRequest("CPF Obrigatório");
            if (!dto.AceitouTermos) return BadRequest("É necessário aceitar os termos.");

            var cpfLimpo = dto.CPF.Replace(".", "").Replace("-", "").Trim();

            if (await _context.Candidatos.AnyAsync(c => c.CPF == cpfLimpo))
            {
                return Conflict(new { message = "CPF já cadastrado." });
            }

            // 2. Mapeamento Completo (DTO -> Entidade)
            var candidato = new Candidato
            {
                // Dados Pessoais
                NomeCompleto = dto.NomeCompleto,
                CPF = cpfLimpo,
                DataNascimento = dto.DataNascimento,
                IdentidadeGenero = dto.IdentidadeGenero,
                OrientacaoSexual = dto.OrientacaoSexual,
                RacaCor = dto.RacaCor,
                Deficiencia = dto.Deficiencia,

                // Contato e Local
                Email = dto.Email,
                Telefone = dto.Telefone,
                CEP = dto.CEP,
                Cidade = dto.Cidade,
                Estado = dto.Estado,

                // Socioeconômico
                Escolaridade = dto.Escolaridade,
                TrabalhaAtualmente = dto.TrabalhaAtualmente,
                RendaFamiliar = dto.RendaFamiliar,
                PessoasNaCasa = dto.PessoasNaCasa,

                // Institucional
                CursoInteresse = dto.CursoInteresse,
                TurnoPreferido = dto.TurnoPreferido,
                JaEstudouNaGF = dto.JaEstudouNaGF,
                NomeIndicacao = dto.NomeIndicacao,

                // Controle e Legado
                AceitouTermos = dto.AceitouTermos,
                AceitouNovidades = dto.AceitouNovidades,
                PossuiComputador = dto.PossuiComputador,
                PossuiInternet = dto.PossuiInternet,
                PerfilLinkedin = dto.PerfilLinkedin,

                Status = StatusCandidato.Inscrito,
                DataCadastro = DateTime.Now
            };

            _context.Candidatos.Add(candidato);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Inscrição realizada com sucesso!", id = candidato.Id });
        }

        // ============================================================
        // DTO ATUALIZADO (CandidatoInscricaoDto)
        // Reflete exatamente o que o formulário Web vai mandar
        // ============================================================
        public class CandidatoInscricaoDto
        {
            // Pessoais
            public string NomeCompleto { get; set; }
            public string CPF { get; set; }
            public DateTime DataNascimento { get; set; }
            public string IdentidadeGenero { get; set; }
            public string OrientacaoSexual { get; set; }
            public string RacaCor { get; set; }
            public string Deficiencia { get; set; }

            // Contato
            public string Email { get; set; }
            public string Telefone { get; set; }
            public string CEP { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }

            // Socioeconômico
            public string Escolaridade { get; set; }
            public bool TrabalhaAtualmente { get; set; }
            public decimal RendaFamiliar { get; set; }
            public int PessoasNaCasa { get; set; }

            // Institucional
            public string CursoInteresse { get; set; }
            public string TurnoPreferido { get; set; }
            public bool JaEstudouNaGF { get; set; }
            public string NomeIndicacao { get; set; }

            // Termos
            public bool AceitouTermos { get; set; }
            public bool AceitouNovidades { get; set; }

            // Legado (Opcionais no novo form, mas mantidos por segurança)
            public bool PossuiComputador { get; set; }
            public bool PossuiInternet { get; set; }
            public string? PerfilLinkedin { get; set; }
        }
    }
}
