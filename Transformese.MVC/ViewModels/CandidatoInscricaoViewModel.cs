using System;
using System.ComponentModel.DataAnnotations;

namespace Transformese.MVC.ViewModels
{
    public class CandidatoInscricaoViewModel
    {
        // --- DADOS PESSOAIS ---
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string NomeCompleto { get; set; }

        [Display(Name = "Nome Social")]
        public string? NomeSocial { get; set; } = string.Empty;

        [Required(ErrorMessage = "CPF é obrigatório")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido")]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Raça/Etnia é obrigatória")]
        [Display(Name = "Raça/Etnia")]
        public string RaçaEtnia { get; set; } = string.Empty;

        [Required(ErrorMessage = "Identidade de Gênero é obrigatória")]
        [Display(Name = "Identidade de Gênero")]
        public string IdentidadeGenero { get; set; } = string.Empty;

        [Display(Name = "Você é uma pessoa transgênero?")]
        public bool PessoaTransgenero { get; set; }

        [Required(ErrorMessage = "Orientação Sexual é obrigatória")]
        [Display(Name = "Orientação Sexual")]
        public string OrientacaoSexual { get; set; } = string.Empty;

        // ===== Contato e Endereço =====
        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [Display(Name = "Telefone/Celular")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "CEP é obrigatório")]
        public string CEP { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cidade é obrigatória")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "Estado é obrigatório")]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = string.Empty;

        // ===== Dados Socioeconômicos =====
        [Required(ErrorMessage = "Escolaridade é obrigatória")]
        [Display(Name = "Escolaridade")]
        public string Escolaridade { get; set; } = string.Empty;

        [Display(Name = "Trabalha Atualmente?")]
        public bool TrabalhaAtualmente { get; set; }

        [Required(ErrorMessage = "Renda familiar é obrigatória")]
        [Display(Name = "Renda Familiar")]
        public string RendaFamiliar { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantidade de pessoas no domicílio é obrigatória")]
        [Range(1, 10, ErrorMessage = "Informe um número válido")]
        [Display(Name = "Pessoas no Domicílio")]
        public int PessoasNoDomicilio { get; set; }

        [Display(Name = "Possui Deficiência?")]
        public bool PossuiDeficiencia { get; set; }

        [Display(Name = "Descrição da Deficiência")]
        public string? DescricaoDeficiencia { get; set; }

        // ===== Dados do Curso/Programa =====
        [Required(ErrorMessage = "Curso desejado é obrigatório")]
        [Display(Name = "Curso Desejado")]
        public string CursoDesejado { get; set; } = string.Empty;

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

        [Display(Name = "Possui Internet?")]
        public bool PossuiInternet { get; set; }

        [Display(Name = "Perfil LinkedIn")]
        public string? PerfilLinkedin { get; set; }
    }
}