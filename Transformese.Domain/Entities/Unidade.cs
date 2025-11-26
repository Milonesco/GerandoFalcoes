namespace Transformese.Domain.Entities
{
    public class Unidade : BaseEntity
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        // Adicione esta linha se ainda não adicionou
        public string? Responsavel { get; set; }
        public int UnidadeId { get; set; }
    }
}