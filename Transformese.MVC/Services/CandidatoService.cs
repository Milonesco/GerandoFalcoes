using System.Text;
using System.Text.Json;
using Transformese.Domain.Enums; // Importante para o StatusCandidato
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

        // --- Métodos Padrão (BuscarTodos, BuscarPorId, AtualizarTriagem) ---

        public async Task<List<CandidatoListaViewModel>> BuscarTodos()
        {
            var response = await _httpClient.GetAsync("/api/Candidatos");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            // Se vier nulo, retorna lista vazia para não quebrar
            return JsonSerializer.Deserialize<List<CandidatoListaViewModel>>(content, _options) ?? new List<CandidatoListaViewModel>();
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
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/Candidatos/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        // ---------------------------------------------------------
        // IMPLEMENTAÇÃO DO FLUXO 3 (PAINEL ONG) - CORRIGIDO
        // ---------------------------------------------------------

        public async Task<List<CandidatoListaViewModel>> GetPendentesPorUnidade(int unidadeId)
        {
            // 1. Busca todos os candidatos
            var todos = await BuscarTodos();

            // 2. Filtra na memória
            // OBS: Certifique-se que CandidatoListaViewModel tem 'UnidadeId' (int) e 'Status' (int)
            var filtrados = todos
                .Where(c => c.UnidadeId == unidadeId &&
                           (c.Status == (int)StatusCandidato.EncaminhadoONG ||
                            c.Status == (int)StatusCandidato.EntrevistaAgendada))
                .ToList();

            return filtrados;
        }

        public async Task AtualizarStatus(int id, StatusCandidato novoStatus)
        {
            await AtualizarStatus(id, novoStatus, null);
        }

        public async Task AtualizarStatus(int id, StatusCandidato novoStatus, string observacao = null)
        {
            var candidato = await BuscarPorId(id);

            if (candidato != null)
            {
                candidato.Status = novoStatus;

                // Se o parâmetro observacao for fornecido, atualiza o campo correspondente
                if (observacao != null)
                {
                    candidato.ObservacoesONG = observacao;
                }

                var json = JsonSerializer.Serialize(candidato);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                await _httpClient.PutAsync($"/api/Candidatos/{id}", content);
            }
        }

        public async Task RegistrarResultadoEntrevista(int id, bool aprovado, string observacoes)
        {
            // Busca o candidato
            var candidato = await BuscarPorId(id);

            if (candidato != null)
            {
                // CORREÇÃO: Lógica de decisão e atribuição direta do Enum
                candidato.Status = aprovado ? StatusCandidato.AprovadoEntrevista : StatusCandidato.ReprovadoEntrevista;

                // Preenche a observação (Certifique-se que CandidatoDetalhesViewModel tem esse campo)
                candidato.ObservacoesONG = observacoes;

                var json = JsonSerializer.Serialize(candidato);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Salva
                await _httpClient.PutAsync($"/api/Candidatos/{id}", content);
            }
        }
    }
}