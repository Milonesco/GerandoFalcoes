using System.ComponentModel.DataAnnotations;

namespace Transformese.Domain.Entities
{
    public class Curso
    {
        [Key]
        public int IdCurso { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string? Imagem { get; set; }
        public int UnidadeId { get; set; }
        public Unidade? Unidade { get; set; }
    }
}
