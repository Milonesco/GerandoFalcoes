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
                NomeSocial = dto.NomeSocial ?? string.Empty, // Trata null
                CPF = cpfLimpo,
                DataNascimento = dto.DataNascimento,
                Email = dto.Email,
                Telefone = dto.Telefone,
                CEP = dto.CEP,
                Cidade = dto.Cidade,
                Estado = dto.Estado,

                RaçaEtnia = dto.RaçaEtnia,
                IdentidadeGenero = dto.IdentidadeGenero,
                PessoaTransgenero = dto.PessoaTransgenero,
                OrientacaoSexual = dto.OrientacaoSexual,

                Escolaridade = dto.Escolaridade,
                TrabalhaAtualmente = dto.TrabalhaAtualmente,
                RendaFamiliar = dto.RendaFamiliar,
                PessoasNoDomicilio = dto.PessoasNoDomicilio,
                PossuiDeficiencia = dto.PossuiDeficiencia,
                DescricaoDeficiencia = dto.DescricaoDeficiencia,

                CursoDesejado = dto.CursoDesejado,
                TurnoPreferencial = dto.TurnoPreferencial,
                JaEstudouAntes = dto.JaEstudouAntes,
                IndicadoPor = dto.IndicadoPor,

                ConcordaPrograma = dto.ConcordaPrograma,
                AceitaNovidades = dto.AceitaNovidades,

                PossuiComputador = dto.PossuiComputador,
                PossuiInternet = dto.PossuiInternet,
                PerfilLinkedin = dto.PerfilLinkedin,

                Status = StatusCandidato.Inscrito,
                DataCadastro = DateTime.Now
            };

            _context.Candidatos.Add(candidato);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Inscrição realizada!", id = candidato.Id });
        }

        public class CandidatoInscricaoDto
        {
            // Dados Pessoais
            public string NomeCompleto { get; set; } = string.Empty;
            public string? NomeSocial { get; set; } // ✅ AGORA É NULLABLE
            public DateTime DataNascimento { get; set; }
            public string CPF { get; set; } = string.Empty;
            public string RaçaEtnia { get; set; } = string.Empty;
            public string IdentidadeGenero { get; set; } = string.Empty;
            public bool PessoaTransgenero { get; set; }
            public string OrientacaoSexual { get; set; } = string.Empty;

            // Contato e Endereço
            public string Email { get; set; } = string.Empty;
            public string Telefone { get; set; } = string.Empty;
            public string CEP { get; set; } = string.Empty;
            public string Estado { get; set; } = string.Empty;
            public string Cidade { get; set; } = string.Empty;

            // Dados Socioeconômicos
            public string Escolaridade { get; set; } = string.Empty;
            public bool TrabalhaAtualmente { get; set; }
            public string RendaFamiliar { get; set; } = string.Empty;
            public int PessoasNoDomicilio { get; set; }
            public bool PossuiDeficiencia { get; set; }
            public string? DescricaoDeficiencia { get; set; }

            // Dados do Curso/Programa
            public string CursoDesejado { get; set; } = string.Empty;
            public string TurnoPreferencial { get; set; } = string.Empty;
            public bool JaEstudouAntes { get; set; }
            public string? IndicadoPor { get; set; }

            // Termos
            public bool ConcordaPrograma { get; set; }
            public bool AceitaNovidades { get; set; }

            // Campos Legados
            public bool PossuiComputador { get; set; }
            public bool PossuiInternet { get; set; }
            public string? PerfilLinkedin { get; set; }
        }
    }
}