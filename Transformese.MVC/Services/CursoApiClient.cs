using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Transformese.Domain.Entities;

namespace Transformese.MVC.Services
{
    public class CursoApiClient : ICursoApiClient
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public CursoApiClient(HttpClient http) => _http = http;

        public async Task<IEnumerable<CursoDto>> GetAllAsync()
        {
            var resp = await _http.GetAsync("api/Cursos");
            resp.EnsureSuccessStatusCode();

            var data = await resp.Content.ReadFromJsonAsync<IEnumerable<CursoDto>>(_jsonOptions);
            return data ?? Array.Empty<CursoDto>();
        }


        public async Task<CursoDto?> GetByIdAsync(int id)
        {
            var resp = await _http.GetAsync($"api/Cursos/{id}");
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<CursoDto>(_jsonOptions);
        }


        public async Task<HttpResponseMessage> CreateAsync(Curso curso, Stream? fileStream = null, string? fileName = null)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(curso.Nome ?? string.Empty), "Nome");
            content.Add(new StringContent(curso.Descricao ?? string.Empty), "Descricao");
            content.Add(new StringContent(curso.UnidadeId.ToString()), "UnidadeId");

            if (fileStream != null && fileName != null)
            {
                var streamContent = new StreamContent(fileStream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                content.Add(streamContent, "arquivo", fileName);
            }

            var resp = await _http.PostAsync("api/Cursos", content);
            return resp;
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, Curso curso, Stream? fileStream = null, string? fileName = null)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(curso.Nome ?? string.Empty), "Nome");
            content.Add(new StringContent(curso.Descricao ?? string.Empty), "Descricao");
            content.Add(new StringContent(curso.UnidadeId.ToString()), "UnidadeId");

            if (fileStream != null && fileName != null)
            {
                var streamContent = new StreamContent(fileStream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                content.Add(streamContent, "arquivo", fileName);
            }

            var resp = await _http.PutAsync($"api/Cursos/{id}", content);
            return resp;
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/Cursos/{id}");
            return resp;
        }
    }
}
