using System.Threading.Tasks;
using Transformese.MVC.ViewModels;

namespace Transformese.MVC.Services
{
    public interface IDashboardApiClient
    {
        Task<DashboardCountsDto> GetCountsAsync();
    }
}
