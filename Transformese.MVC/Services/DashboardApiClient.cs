using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Transformese.MVC.ViewModels;

namespace Transformese.MVC.Services
{
    public class DashboardApiClient : IDashboardApiClient
    {
        private readonly HttpClient _http;

        public DashboardApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<DashboardCountsDto> GetCountsAsync()
        {
            var result = await _http.GetFromJsonAsync<DashboardCountsDto>("api/dashboard/counts");
            return result ?? new DashboardCountsDto();
        }
    }
}
