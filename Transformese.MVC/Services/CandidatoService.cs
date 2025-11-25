using System.Text.Json;
using Transformese.MVC.ViewModels;

namespace Transformese.MVC.Services
{
    public class CandidatoService : ICandidatoService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public CandidatoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<CandidatoListaViewModel>> BuscarTodos()
        {
            var response = await _httpClient.GetAsync("/api/Candidatos");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<CandidatoListaViewModel>>(content, _options);
        }

        public async Task<CandidatoDetalhesViewModel> BuscarPorId(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Candidatos/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CandidatoDetalhesViewModel>(content, _options);
        }

        public async Task AtualizarTriagem(int id, CandidatoTriagemViewModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/Candidatos/{id}", content);
            response.EnsureSuccessStatusCode();
        }
    }
}