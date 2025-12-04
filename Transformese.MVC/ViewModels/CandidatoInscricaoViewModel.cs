using System;
using System.ComponentModel.DataAnnotations;

namespace Transformese.MVC.ViewModels
{
    public class CandidatoInscricaoViewModel
    {
        // --- DADOS PESSOAIS ---
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        // Novos campos de Diversidade
        public string IdentidadeGenero { get; set; }
        public string OrientacaoSexual { get; set; }
        public string RacaCor { get; set; }
        public string Deficiencia { get; set; }

        // --- CONTATO E LOCALIZAÇÃO ---
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Telefone { get; set; }

        public string CEP { get; set; } // Novo

        [Required(ErrorMessage = "Cidade é obrigatória")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório")]
        public string Estado { get; set; }

        // --- SOCIOECONÔMICO (NOVOS) ---
        public string Escolaridade { get; set; }

        public bool TrabalhaAtualmente { get; set; }

        public decimal RendaFamiliar { get; set; }

        public int PessoasNaCasa { get; set; }

        // --- INSTITUCIONAL / CURSO (NOVOS) ---
        [Required(ErrorMessage = "Selecione o curso de interesse")]
        public string CursoInteresse { get; set; }

        public string TurnoPreferido { get; set; }

        public bool JaEstudouNaGF { get; set; }

        public string? NomeIndicacao { get; set; } // Pode ser nulo

        // --- TERMOS E CONTROLE ---
        // O Range obriga o checkbox a ser marcado (True)
        [Range(typeof(bool), "true", "true", ErrorMessage = "Você precisa aceitar os termos.")]
        public bool AceitouTermos { get; set; }

        public bool AceitouNovidades { get; set; }

        // --- LEGADO (MANTIDOS) ---
        public bool PossuiComputador { get; set; }
        public bool PossuiInternet { get; set; }
        public string? PerfilLinkedin { get; set; }
    }
}