using FE_AgentGuard.Interfaces;
using FE_AgentGuard.InterFaces;
using FE_AgentGuard.Models.Models;


namespace FE_AgentGuard.ServerHandling
{
    public class AgentServer:IServer<Agent>
    {        
        private readonly HttpClient _httpClient;
        private readonly string UrlBase;
        public AgentServer(HttpClient httpClient, string urlBase)
        {
            _httpClient = httpClient;
            UrlBase = urlBase;
        }
        public async Task<List<Agent>> GetObjectsAsync()
        {
            var response = await _httpClient.GetAsync(UrlBase);
            response.EnsureSuccessStatusCode();
            var Agents = await response.Content.ReadFromJsonAsync<List<Agent>>();
            return Agents;
        }
        public async Task<Agent> GetObjectAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{UrlBase}/{id}");
            response.EnsureSuccessStatusCode();
            var agent = await response.Content.ReadFromJsonAsync<Agent>();
            return agent;
        }
        public async Task<Agent> CheckLoginAsync(Agent person)
        {
            var response = await _httpClient.PostAsJsonAsync(UrlBase, person);
            response.EnsureSuccessStatusCode();
            var createdPost = await response.Content.ReadFromJsonAsync<Agent>();
            return createdPost;
        }
        public async Task<Agent> CreateObjectAsync(Agent person)
        {
            var response = await _httpClient.PostAsJsonAsync(UrlBase, person);
            response.EnsureSuccessStatusCode();
            var createdPost = await response.Content.ReadFromJsonAsync<Agent>();
            return createdPost;
        }
        public async Task UpdateObjectAsync(Agent person,int id)
        {
            var response = await _httpClient.PutAsJsonAsync($"{UrlBase}/{id}", person);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteObjectAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{UrlBase}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
