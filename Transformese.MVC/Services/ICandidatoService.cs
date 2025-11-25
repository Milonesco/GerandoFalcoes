using Transformese.MVC.ViewModels;

namespace Transformese.MVC.Services
{
    public interface ICandidatoService
    {
        Task<List<CandidatoListaViewModel>> BuscarTodos();
        Task<CandidatoDetalhesViewModel> BuscarPorId(int id); 
        Task AtualizarTriagem(int id, CandidatoTriagemViewModel model); 
    }
}
}