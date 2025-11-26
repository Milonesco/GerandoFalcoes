using System.ComponentModel.DataAnnotations;

namespace Transformese.Domain.Entities
{
    public class Unidade : BaseEntity
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string? Responsavel { get; set; }
        public bool Ativo { get; set; }
        public int UnidadeId { get; set; }

    }
}