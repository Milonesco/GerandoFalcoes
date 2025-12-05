using System;

namespace Transformese.Domain.Entities
{
    public class UnidadeParceira
    {
        public int Id { get; set; }
        public string NomeUnidade { get; set; }
        public string NomeResponsavel { get; set; }
        public string Email { get; set; }
        public string Cidade { get; set; }
        public string NomeVaga { get; set; }
        public int QuantidadeVagas { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}