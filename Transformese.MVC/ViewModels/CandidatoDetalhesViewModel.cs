using Transformese.Domain.Enums;

namespace Transformese.MVC.ViewModels
{
    public class CandidatoDetalhesViewModel
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public bool PossuiComputador { get; set; }
        public bool PossuiInternet { get; set; }
        public string? PerfilLinkedin { get; set; }

        // Campos de Controle (Estes serão editáveis pelo Admin)
        public int? UnidadeId { get; set; }
        public StatusCandidato Status { get; set; }
        public string? ObservacoesGF { get; set; }
    }
}