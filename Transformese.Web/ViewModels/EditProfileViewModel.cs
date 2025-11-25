using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TransformeseMVC.Web.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Foto de Perfil")]
        public IFormFile? FotoPerfil { get; set; }   // arquivo

        public string? FotoPerfilUrl { get; set; }   // caminho salvo
    }
}
