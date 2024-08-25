using FE_AgentGuard.Interfaces;
using FE_AgentGuard.InterFaces;
using FE_AgentGuard.Models.Models;

namespace FE_AgentGuard.ServerHandling
{
    public class TargetServer : IServer<Target>
    {

        private readonly HttpClient _httpClient;
        private readonly string Url;
        public TargetServer(HttpClient httpClient,string url)
        {
            _httpClient = httpClient;
            Url = url;
        }
        public async Task<List<Target>> GetObjectsAsync()
        {
            var response = await _httpClient.GetAsync(Url);
            response.EnsureSuccessStatusCode();
            var Agents = await response.Content.ReadFromJsonAsync<List<Target>>();
            return Agents;
        }
        public async Task<Target> GetObjectAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{Url}/{id}");
            response.EnsureSuccessStatusCode();
            var agent = await response.Content.ReadFromJsonAsync<Target>();
            return agent;
        }

        public async Task<Target> CreateObjectAsync(Target person)
        {
            var response = await _httpClient.PostAsJsonAsync(Url, person);
            response.EnsureSuccessStatusCode();
            var createdPost = await response.Content.ReadFromJsonAsync<Target>();
            return createdPost;
        }

        public async Task UpdateObjectAsync(Target person,int id)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Url}/{id}", person);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteObjectAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Url}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
