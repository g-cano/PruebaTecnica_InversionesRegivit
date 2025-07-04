﻿
using System.Text;
using System.Text.Json;

namespace PruebaTecnicaInversionesRegivit.FrontEnd.Services
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient;

        public Repository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        public async Task<object> DeleteByIdAsync<T>(string url, int id)
        {
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error {(int)response.StatusCode}: {response.ReasonPhrase}. {errorContent}");
                }

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(content, _jsonDefaultOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}"); 
                throw;
            }
        }

        public async Task<T> GetByIdAsync<T>(string url, int id)
        {
            var response = await _httpClient.GetAsync($"{url}/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, _jsonDefaultOptions);

        }

        public async Task<object> PostAsync<T>(string url, T entity)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error {(int)response.StatusCode}: {response.ReasonPhrase}. {errorContent}");
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                throw;
            }
        }

        public async Task<object> PutAsync<T>(string url, T entity)
        {
            var content = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public Task<object> PostByIdAsync<T>(string url, int id, T entity)
        {
            throw new NotImplementedException();
        }

       
    }
}
