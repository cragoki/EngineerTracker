using System.Net.Http.Json;

namespace EngineerTracker.Clients
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET Request TODO: HANDLE CACHING HERE IF THE STATUS CODE IS THAT THE API IS NOT REACHABLE, RETRIEVE FROM CACHE
        public async Task<T> GetAsync<T>(string endpoint)
        {
            return await _httpClient.GetFromJsonAsync<T>(endpoint);
        }

        // POST Request
        public async Task PostNoResponseAsync<TRequest>(string endpoint, TRequest content)
        {
            await _httpClient.PostAsJsonAsync(endpoint, content);
        }

        // PUT Request
        public async Task PutAsync<TRequest>(string endpoint, TRequest content)
        {
            await _httpClient.PutAsJsonAsync(endpoint, content);
        }
    }
}
