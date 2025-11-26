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
        public string? FotoPerfil { get; set; }
        public DateTime DataNascimento { get; set; }
        public int? UnidadeId { get; set; }
        public Unidade? Unidade { get; set; }
        public int TipoUsuarioId { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}
