using System;

namespace Transformese.Domain.Entities
{
    public class LogSistema
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; } = DateTime.Now;
        public string Usuario { get; set; }
        public string Acao { get; set; }
        public string Detalhes { get; set; }
    }
}