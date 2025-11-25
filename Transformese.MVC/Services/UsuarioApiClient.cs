
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Transformese.Domain.Entities;

namespace Transformese.MVC.Services
{
    public class UsuarioApiClient : IUsuarioApiClient
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public UsuarioApiClient(HttpClient http) => _http = http;

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            var resp = await _http.GetAsync("api/Usuarios");
            resp.EnsureSuccessStatusCode();
            var data = await resp.Content.ReadFromJsonAsync<IEnumerable<Usuario>>(_jsonOptions);
            return data ?? Array.Empty<Usuario>();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            var resp = await _http.GetAsync($"api/Usuarios/{id}");
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Usuario>(_jsonOptions);
        }

        public async Task<(string token, string nome, string tipo)> AuthenticateAsync(string email, string senha)
        {
            var resp = await _http.PostAsJsonAsync("api/auth/login", new { Email = email, Senha = senha });

            if (!resp.IsSuccessStatusCode) return (null, null, null);

            var obj = await resp.Content.ReadFromJsonAsync<JsonElement>();
            var token = obj.GetProperty("token").GetString();
            var nome = obj.GetProperty("nome").GetString();
            var tipo = obj.GetProperty("tipo").GetString();
            return (token, nome, tipo);
        }



        public async Task<HttpResponseMessage> RegisterAsync(Usuario usuario, Stream? fileStream = null, string? fileName = null)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(usuario.Nome ?? string.Empty), "Nome");
            content.Add(new StringContent(usuario.Email ?? string.Empty), "Email");
            content.Add(new StringContent(usuario.Senha ?? string.Empty), "Senha");
            content.Add(new StringContent(usuario.DataNascimento.ToString("o")), "DataNascimento");
            content.Add(new StringContent(usuario.TipoUsuarioId.ToString()), "TipoUsuarioId");

            if (fileStream != null && fileName != null)
            {
                var streamContent = new StreamContent(fileStream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                content.Add(streamContent, "arquivo", fileName);
            }

            return await _http.PostAsync("api/Usuarios/register", content);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, Usuario usuario, Stream? fileStream = null, string? fileName = null)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(usuario.Nome ?? string.Empty), "Nome");
            content.Add(new StringContent(usuario.Email ?? string.Empty), "Email");
            content.Add(new StringContent(usuario.Senha ?? string.Empty), "Senha");
            content.Add(new StringContent(usuario.DataNascimento.ToString("o")), "DataNascimento");
            content.Add(new StringContent(usuario.TipoUsuarioId.ToString()), "TipoUsuarioId");

            if (fileStream != null && fileName != null)
            {
                var streamContent = new StreamContent(fileStream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                content.Add(streamContent, "arquivo", fileName);
            }

            return await _http.PutAsync($"api/Usuarios/{id}", content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _http.DeleteAsync($"api/Usuarios/{id}");
        }
    }
}