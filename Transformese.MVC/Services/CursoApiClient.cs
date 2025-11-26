using System.Net.Http.Headers;
using System.Text.Json;
using Transformese.Domain.Entities;
using Transformese.MVC.Services;

public class CursoApiClient : ICursoApiClient
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public CursoApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<CursoDto>> GetAllAsync()
    {
        var resp = await _http.GetAsync("api/Cursos");
        resp.EnsureSuccessStatusCode();

        return await resp.Content.ReadFromJsonAsync<IEnumerable<CursoDto>>(_jsonOptions)
               ?? Enumerable.Empty<CursoDto>();
    }

    public async Task<CursoDto?> GetByIdAsync(int id)
    {
        var resp = await _http.GetAsync($"api/Cursos/{id}");

        if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<CursoDto>(_jsonOptions);
    }

    private MultipartFormDataContent BuildForm(Curso curso, Stream? fileStream, string? fileName)
    {
        var content = new MultipartFormDataContent();

        content.Add(new StringContent(curso.Nome ?? ""), "Nome");
        content.Add(new StringContent(curso.Descricao ?? ""), "Descricao");
        content.Add(new StringContent(curso.UnidadeId.ToString()), "UnidadeId");

        if (fileStream != null && fileName != null)
        {
            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            content.Add(streamContent, "arquivo", fileName);
        }

        return content;
    }

    public async Task<HttpResponseMessage> CreateAsync(Curso curso, Stream? fileStream = null, string? fileName = null)
    {
        using var content = BuildForm(curso, fileStream, fileName);
        return await _http.PostAsync("api/Cursos", content);
    }

    public async Task<HttpResponseMessage> UpdateAsync(int id, Curso curso, Stream? fileStream = null, string? fileName = null)
    {
        using var content = BuildForm(curso, fileStream, fileName);
        return await _http.PutAsync($"api/Cursos/{id}", content);
    }

    public async Task<HttpResponseMessage> DeleteAsync(int id)
    {
        return await _http.DeleteAsync($"api/Cursos/{id}");
    }
}
