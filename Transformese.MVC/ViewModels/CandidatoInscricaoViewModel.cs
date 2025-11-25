using System.ComponentModel.DataAnnotations;

namespace Transformese.MVC.ViewModels
{
    public class CandidatoInscricaoViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string Estado { get; set; }

        public bool PossuiComputador { get; set; }
        public bool PossuiInternet { get; set; }
        public string? PerfilLinkedin { get; set; }
    }
}