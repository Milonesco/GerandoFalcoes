using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Transformese.Domain.Entities
{
    public class Unidade
    {
        [Key]
        public int IdUnidade { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public List<Curso>? Cursos { get; set; }
    }
}
