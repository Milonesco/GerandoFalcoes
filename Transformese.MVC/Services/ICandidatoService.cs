using Transformese.Domain.Enums; // Certifique-se que o projeto tem referência ao Domain
using Transformese.MVC.ViewModels;

namespace Transformese.MVC.Services
{
    public interface ICandidatoService
    {
        // Métodos existentes (Funil 1 e 2)
        Task<List<CandidatoListaViewModel>> BuscarTodos();
        Task<CandidatoDetalhesViewModel> BuscarPorId(int id);
        Task AtualizarTriagem(int id, CandidatoTriagemViewModel model);
        Task AtualizarStatus(int id, StatusCandidato novoStatus, string observacao = null);

        // --- NOVOS MÉTODOS (Fluxo 3 - Painel ONG) ---

        // Busca candidatos filtrados para a tela da ONG (Pendente/Agendado)
        Task<List<CandidatoListaViewModel>> GetPendentesPorUnidade(int unidadeId);

        // Para o botão "Agendar"
        Task AtualizarStatus(int id, StatusCandidato novoStatus);

        // Para a Automação 4: Aprovar/Reprovar + Observações
        Task RegistrarResultadoEntrevista(int id, bool aprovado, string observacoes);
    }
}