namespace Transformese.MVC.ViewModels
{
    public class CandidatoListaViewModel
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public int Status { get; set; }
        public string Unidade { get; set; }
        public DateTime DataCadastro { get; set; }
        public int UnidadeId { get; internal set; }
    }
}