using System.Collections.Generic;
using System.Threading.Tasks;
using Transformese.Domain.Entities;

namespace Transformese.MVC.Services
{
    public interface IUnidadeApiClient
    {
        Task<IEnumerable<Unidade>> GetAllAsync();
        Task<Unidade?> GetByIdAsync(int id);

        // MÉTODO NOVO: Adicionado para permitir o cadastro de novas ONGs
        Task CreateAsync(Unidade unidade);
    }
}