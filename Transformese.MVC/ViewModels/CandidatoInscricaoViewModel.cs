using System.ComponentModel.DataAnnotations;

namespace Transformese.MVC.ViewModels
{
    public class CandidatoInscricaoViewModel
    {
        // ===== Dados Pessoais =====
        [Required(ErrorMessage = "Nome completo é obrigatório")]
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

        [Required(ErrorMessage = "Turno preferencial é obrigatório")]
        [Display(Name = "Turno Preferencial")]
        public string TurnoPreferencial { get; set; } = string.Empty;

        [Display(Name = "Já estudou antes?")]
        public bool JaEstudouAntes { get; set; }

        [Display(Name = "Indicado Por")]
        public string? IndicadoPor { get; set; }

        // ===== Termos =====
        [Required(ErrorMessage = "É necessário concordar com os termos do programa")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "É necessário concordar com os termos")]
        [Display(Name = "Concordo com os termos do programa")]
        public bool ConcordaPrograma { get; set; }

        [Display(Name = "Aceito receber novidades")]
        public bool AceitaNovidades { get; set; }

        // ===== Campos Legados (mantidos para compatibilidade) =====
        [Display(Name = "Possui Computador?")]
        public bool PossuiComputador { get; set; }

        [Display(Name = "Possui Internet?")]
        public bool PossuiInternet { get; set; }

        [Display(Name = "Perfil LinkedIn")]
        public string? PerfilLinkedin { get; set; }
    }
}