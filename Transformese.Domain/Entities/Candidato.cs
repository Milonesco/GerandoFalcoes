using System;
using Transformese.Domain.Enums;

namespace Transformese.Domain.Entities
{
    public class Candidato
    {
        public int Id { get; set; }

        // --- Dados Pessoais ---
        public string NomeCompleto { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty; // Novo
        public DateTime DataNascimento { get; set; }
        public string IdentidadeGenero { get; set; } = string.Empty;
        public string OrientacaoSexual { get; set; } = string.Empty;
        public string RacaCor { get; set; } = string.Empty;
        public string Deficiencia { get; set; } = string.Empty;

        // --- Contato e Local ---
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; }

        // --- Socioeconômico ---
        public string Escolaridade { get; set; } = string.Empty;
        public bool TrabalhaAtualmente { get; set; }
        public decimal RendaFamiliar { get; set; }
        public int PessoasNaCasa { get; set; }

        // --- Curso e Institucional ---
        public string CursoInteresse { get; set; } = string.Empty;
        public string TurnoPreferido { get; set; } = string.Empty;
        public bool JaEstudouNaGF { get; set; }
        public string NomeIndicacao { get; set; } = string.Empty;

        // --- Controle ---
        public bool AceitouTermos { get; set; }
        public bool AceitouNovidades { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public StatusCandidato Status { get; set; } = StatusCandidato.Inscrito;

        // --- Propriedades Extras / Triagem ---
        public bool PossuiComputador { get; set; }
        public bool PossuiInternet { get; set; }
        public string? PerfilLinkedin { get; set; }

        // Campos usados na ViewTriagem (Devem bater com o Banco de Dados)
        public string? NomeOngResponsavel { get; set; }
        public string ObservacoesONG { get; set; } // Mapeado para txtAnotacoesRH
        public string VagaEncaminhada { get; set; }

        // --- CAMPO FALTANTE (Opcional) ---
        // Adicione este se quiser salvar o valor do numPontuacao
        public int? Pontuacao { get; set; }
        public DateTime? DataEntrevista { get; set; }

    }
}