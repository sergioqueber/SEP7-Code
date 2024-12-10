using System.Collections.Generic;
using System.Linq;
using Model;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AppServices;

public class TeamService : ITeamService
{

    private readonly HttpClient _httpClient;

    public TeamService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<Team>> GetAllTeams()
    {
        return await _httpClient.GetFromJsonAsync<List<Team>>("api/team")
                   ?? new List<Team>();
    }

    public async Task<Team?> GetTeamById(int id)
    {
        return await _httpClient.GetFromJsonAsync<Team>($"api/team/{id}");
    }
    public async Task<Team> CreateTeam(Team team)
    {
        var response = await _httpClient.PostAsJsonAsync("api/team", team);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Team>();
    }
    public async Task<Team> RemoveTeam(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/team/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Team>();
    }
    public async Task<Team?> UpdateTeam(Team team)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/team/{team.Id}", team);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Team>();
    }
}
