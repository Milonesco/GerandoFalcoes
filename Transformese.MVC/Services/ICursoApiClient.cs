using Transformese.Domain.Entities;

namespace Transformese.MVC.Services
{
    public interface ICursoApiClient
    {
        Task<IEnumerable<CursoDto>> GetAllAsync();
        Task<CursoDto?> GetByIdAsync(int id);

        Task<HttpResponseMessage> CreateAsync(Curso curso, Stream? fileStream = null, string? fileName = null);
        Task<HttpResponseMessage> UpdateAsync(int id, Curso curso, Stream? fileStream = null, string? fileName = null);
        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}