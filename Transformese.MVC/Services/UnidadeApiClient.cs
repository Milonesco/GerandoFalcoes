using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Transformese.Domain.Entities;

namespace Transformese.MVC.Services
{
    public class UnidadeApiClient : IUnidadeApiClient
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public UnidadeApiClient(HttpClient http) => _http = http;

        public async Task<Usuario?> AuthenticateAsync(string email, string senha)
        {
            var resp = await _http.PostAsJsonAsync("api/auth/login", new
            {
                Email = email,
                Senha = senha
            });

            if (!resp.IsSuccessStatusCode)
                return null;

            return await resp.Content.ReadFromJsonAsync<Usuario>();
        }


        public async Task<List<Unidade>> GetAllAsync()
        {
            var resp = await _http.GetAsync("api/Unidades");
            resp.EnsureSuccessStatusCode();
            var data = await resp.Content.ReadFromJsonAsync<List<Unidade>>(_jsonOptions);
            return data ?? new List<Unidade>();
        }

        public async Task<Unidade?> GetByIdAsync(int id)
        {
            var resp = await _http.GetAsync($"api/Unidades/{id}");
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Unidade>(_jsonOptions);
        }

        public async Task CreateAsync(Unidade unidade)
        {
            var resp = await _http.PostAsJsonAsync("api/Unidades", unidade);

            resp.EnsureSuccessStatusCode();
        }
    }
}