using FE_AgentGuard.Interfaces;
using FE_AgentGuard.InterFaces;
using FE_AgentGuard.Models.Models;
using FE_AgentGuard.Models.ServerModel;
using System;
using System.Security;

namespace FE_AgentGuard.ServerHandling
{
    public class MissionServer 
    {
        private readonly HttpClient _httpClient;
        private readonly string Url;
        public MissionServer(HttpClient httpClient,string url)
        {
            _httpClient = httpClient;
            Url = url;
        }
        public async Task<List<Mission>> GetObjectsAsync()
        {
            var response = await _httpClient.GetAsync(Url);
            response.EnsureSuccessStatusCode();
            var Agents = await response.Content.ReadFromJsonAsync<List<Mission>>();
            return Agents;
        }
        public async Task<Mission> GetObjectAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{Url}/{id}");
            response.EnsureSuccessStatusCode();
            var agent = await response.Content.ReadFromJsonAsync<Mission>();
            return agent;
        }
        public async Task<Mission> CreateObjectAsync(Mission person)
        {
            var response = await _httpClient.PostAsJsonAsync(Url, person);
            response.EnsureSuccessStatusCode();
            var createdPost = await response.Content.ReadFromJsonAsync<Mission>();
            return createdPost;
        }
        public async Task UpdateObjectAsync(MissionAssigned person,int id)
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