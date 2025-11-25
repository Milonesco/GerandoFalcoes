using System;
using System.ComponentModel.DataAnnotations;

namespace Transformese.Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string? FotoPerfil { get; set; }
        public int TipoUsuarioId { get; set; }

        // Removido [JsonIgnore] para permitir que a API serializa o TipoUsuario.
        public TipoUsuario? TipoUsuario { get; set; }
    }
}
