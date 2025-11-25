using Transformese.Domain.Enums;

namespace Transformese.MVC.ViewModels
{
    public class CandidatoTriagemViewModel
    {
        public int? UnidadeId { get; set; }
        public StatusCandidato Status { get; set; }
        public string? ObservacoesGF { get; set; }
    }
}