using System.Threading.Tasks;
using Transformese.Domain.Entities;

namespace Transformese.MVC.Services
{
    public interface IUsuarioApiClient
    {
        Task<(string token, string nome, string tipo)> AuthenticateAsync(string email, string senha);

        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task<HttpResponseMessage> RegisterAsync(Usuario usuario, Stream? fileStream = null, string? fileName = null);
        Task<HttpResponseMessage> UpdateAsync(int id, Usuario usuario, Stream? fileStream = null, string? fileName = null);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
