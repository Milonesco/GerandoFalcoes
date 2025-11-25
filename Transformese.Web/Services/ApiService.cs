using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Transformese.Web.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        public ApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            return await _http.GetFromJsonAsync<T>(url);
        }

        public async Task<HttpResponseMessage> PostJsonAsync<T>(string url, T data)
        {
            return await _http.PostAsJsonAsync(url, data);
        }

        public async Task<HttpResponseMessage> PutJsonAsync<T>(string url, T data)
        {
            return await _http.PutAsJsonAsync(url, data);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await _http.DeleteAsync(url);
        }

        // POST multipart/form-data (fields + optional file)
        public async Task<HttpResponseMessage> PostFormAsync(string url, Dictionary<string, string> fields, Stream? fileStream = null, string? fileName = null, string fileFieldName = "arquivo")
        {
            using var content = new MultipartFormDataContent();

            foreach (var kv in fields)
                content.Add(new StringContent(kv.Value ?? ""), kv.Key);

            if (fileStream != null && fileName != null)
            {
                var streamContent = new StreamContent(fileStream);
                // option: set content type if needed: streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                content.Add(streamContent, fileFieldName, fileName);
            }

            return await _http.PostAsync(url, content);
        }

        // PUT multipart/form-data
        public async Task<HttpResponseMessage> PutFormAsync(string url, Dictionary<string, string> fields, Stream? fileStream = null, string? fileName = null, string fileFieldName = "arquivo")
        {
            using var content = new MultipartFormDataContent();

            foreach (var kv in fields)
                content.Add(new StringContent(kv.Value ?? ""), kv.Key);

            if (fileStream != null && fileName != null)
            {
                var streamContent = new StreamContent(fileStream);
                content.Add(streamContent, fileFieldName, fileName);
            }

            return await _http.PutAsync(url, content);
        }

        // Helper: Parse JSON document to dynamic if needed
        public async Task<JsonDocument?> GetJsonDocumentAsync(string url)
        {
            var stream = await _http.GetStreamAsync(url);
            return await JsonDocument.ParseAsync(stream);
        }
    }
}
