using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Transformese.Domain.Entities
{
    public class TipoUsuario
    {
        [Key]
        public int IdTipoUsuario { get; set; }
        public string DescricaoTipoUsuario { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Usuario> Usuarios { get; set; }

    }
}
