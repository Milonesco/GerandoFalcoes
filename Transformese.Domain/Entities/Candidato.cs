using System;
using Transformese.Domain.Enums;

namespace Transformese.Domain.Entities
{
    public class Candidato
    {
        public int Id { get; set; }

        // --- Dados Pessoais ---
        public string NomeCompleto { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string IdentidadeGenero { get; set; } = string.Empty; // Novo
        public string OrientacaoSexual { get; set; } = string.Empty; // Novo
        public string RacaCor { get; set; } = string.Empty; // Novo
        public string Deficiencia { get; set; } = string.Empty; // Novo

        // --- Contato e Local ---
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty; // Novo
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        // --- Socioeconômico ---
        public string Escolaridade { get; set; } = string.Empty; // Novo
        public bool TrabalhaAtualmente { get; set; } // Novo
        public decimal RendaFamiliar { get; set; } // Novo
        public int PessoasNaCasa { get; set; } // Novo

        // --- Curso e Institucional ---
        public string CursoInteresse { get; set; } = string.Empty; // Novo
        public string TurnoPreferido { get; set; } = string.Empty; // Novo
        public bool JaEstudouNaGF { get; set; } // Novo
        public string NomeIndicacao { get; set; } = string.Empty; // Novo

        // --- Controle ---
        public bool AceitouTermos { get; set; }
        public bool AceitouNovidades { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public StatusCandidato Status { get; set; } = StatusCandidato.Inscrito;

        // Propriedades Legado/Extras
        public bool PossuiComputador { get; set; }
        public bool PossuiInternet { get; set; }
        public string? PerfilLinkedin { get; set; }
        public string? NomeOngResponsavel { get; set; }
    }
}