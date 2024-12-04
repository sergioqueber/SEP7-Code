using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace AppServices
{
    public class TeamService
    {
        private readonly HttpClient _httpClient;

        public TeamService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Team>>("api/Team") ?? new List<Team>();
        }

        public async Task<Team?> GetTeamById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Team>($"api/Team/{id}");
        }

        public async Task<Team> CreateTeam(Team team)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Team", team);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Team>();
        }

        public async Task<Team> UpdateTeam(int id, Team team)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Team/{id}", team);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Team>();
        }

        public async Task DeleteTeam(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Team/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
