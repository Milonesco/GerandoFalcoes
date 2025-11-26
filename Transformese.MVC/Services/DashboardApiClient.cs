using System.Text.Json;
using Transformese.MVC.ViewModels;

namespace Transformese.MVC.Services
{
    public class DashboardApiClient : IDashboardApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public DashboardApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<DashboardCountsDto> GetCountsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Dashboard/kpis");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DashboardCountsDto>(content, _options);
            }
            catch
            {
                return new DashboardCountsDto();
            }
        }
    }
}