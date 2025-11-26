using System.Collections.Generic;
using System.Threading.Tasks;
using Transformese.Domain.Entities;

namespace Transformese.MVC.Services
{
    public interface IUnidadeApiClient
    {
        Task<List<Unidade>> GetAllAsync();
        Task<Unidade?> GetByIdAsync(int id);
        Task CreateAsync(Unidade unidade);
    }
}