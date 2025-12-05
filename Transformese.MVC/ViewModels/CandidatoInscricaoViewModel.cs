using System;
using System.ComponentModel.DataAnnotations;

namespace Transformese.MVC.ViewModels
{
    public class CandidatoInscricaoViewModel
    {
        // --- DADOS PESSOAIS ---
        [Required(ErrorMessage = "Nome é obrigatório")]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; } = string.Empty;

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
        public string RacaCor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Identidade de Gênero é obrigatória")]
        [Display(Name = "Identidade de Gênero")]
        public string IdentidadeGenero { get; set; } = string.Empty;

        [Display(Name = "Você é uma pessoa transgênero?")]
        public bool PessoaTransgenero { get; set; }

        [Required(ErrorMessage = "Orientação Sexual é obrigatória")]
        [Display(Name = "Orientação Sexual")]
        public string OrientacaoSexual { get; set; } = string.Empty;

        [Display(Name = "Possui Deficiência?")]
        public bool PossuiDeficiencia { get; set; }

        [Display(Name = "Descrição da Deficiência")]
        public string? Deficiencia { get; set; }

        // --- CONTATO E ENDEREÇO ---
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

        // --- DADOS SOCIOECONÔMICOS ---
        [Required(ErrorMessage = "Escolaridade é obrigatória")]
        [Display(Name = "Escolaridade")]
        public string Escolaridade { get; set; } = string.Empty;

        [Display(Name = "Trabalha Atualmente?")]
        public bool TrabalhaAtualmente { get; set; }

        [Required(ErrorMessage = "Renda familiar é obrigatória")]
        [Display(Name = "Renda Familiar")]
        public decimal RendaFamiliar { get; set; }

        [Required(ErrorMessage = "Quantidade de pessoas no domicílio é obrigatória")]
        [Range(1, 20, ErrorMessage = "Informe um número válido entre 1 e 20")]
        [Display(Name = "Pessoas no Domicílio")]
        public int PessoasNaCasa { get; set; }

        [Display(Name = "Possui Computador?")]
        public bool PossuiComputador { get; set; }

        [Display(Name = "Possui Internet?")]
        public bool PossuiInternet { get; set; }

        // --- DADOS DO CURSO/PROGRAMA ---
        [Required(ErrorMessage = "Curso desejado é obrigatório")]
        [Display(Name = "Curso de Interesse")]
        public string CursoInteresse { get; set; } = string.Empty;

        [Display(Name = "Turno Preferido")]
        public string? TurnoPreferido { get; set; }

        [Display(Name = "Já estudou na Gerando Falcões?")]
        public bool JaEstudouNaGF { get; set; }

        [Display(Name = "Nome de quem indicou (opcional)")]
        public string? NomeIndicacao { get; set; }

        [Display(Name = "Perfil LinkedIn")]
        public string? PerfilLinkedin { get; set; }

        // --- TERMOS E CONSENTIMENTO ---
        [Range(typeof(bool), "true", "true", ErrorMessage = "Você precisa aceitar os termos")]
        [Display(Name = "Aceito os termos e condições")]
        public bool AceitouTermos { get; set; }

        [Display(Name = "Aceito receber novidades")]
        public bool AceitouNovidades { get; set; }
    }
}