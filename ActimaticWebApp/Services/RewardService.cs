using Interfaces;
using Model;
using System.Net.Http.Json;

namespace AppServices;

public class RewardService : IRewardService
{
    private readonly HttpClient _httpClient;

    public RewardService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Reward>> GetAllRewards()
    {
        return await _httpClient.GetFromJsonAsync<List<Reward>>("api/reward") ?? new List<Reward>();
    }

    public async Task<Reward?> GetRewardById(int id)
    {
        return await _httpClient.GetFromJsonAsync<Reward>($"api/reward/{id}");
    }

    public async Task<Reward> CreateReward(Reward reward)
    {
        var response = await _httpClient.PostAsJsonAsync("api/reward", reward);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Reward>();
    }

    public async Task<Reward> RemoveReward(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/reward/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Reward>();
    }

    public async Task<Reward?> UpdateReward(Reward reward)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/reward/{reward.Id}", reward);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Reward>();
    }
}
