using System;
using Transformese.Domain.Enums;

namespace Transformese.Domain.Entities
{
    public class CandidatoLog : BaseEntity
    {
        public int CandidatoId { get; set; }
        public Candidato Candidato { get; set; }

        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public StatusCandidato StatusAnterior { get; set; }
        public StatusCandidato NovoStatus { get; set; }

        public string Acao { get; set; }
        public string? Observacao { get; set; }

        public DateTime DataHora { get; set; } = DateTime.Now;
    }
}