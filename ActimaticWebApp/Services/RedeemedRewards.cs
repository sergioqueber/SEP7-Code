using Interfaces;
using Model;
namespace AppServices;

public class RedeemedRewardsService : IRedeemedRewardService
{
    private readonly HttpClient _httpClient;

    public RedeemedRewardsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<RedeemedReward> CreateRedeemedReward(RedeemedReward redeemedReward)
    {
        var response = await _httpClient.PostAsJsonAsync("api/redeemedReward", redeemedReward);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RedeemedReward>();
    }

    public async Task<IEnumerable<RedeemedReward>> GetAllRedeemedRewards()
    {
        return await _httpClient.GetFromJsonAsync<List<RedeemedReward>>("api/redeemedReward")
                   ?? new List<RedeemedReward>();
    }

    public async Task<RedeemedReward?> GetRedeemedRewardById(int id)
    {
        return await _httpClient.GetFromJsonAsync<RedeemedReward>($"api/redeemedReward/{id}");
    }

    public async Task<RedeemedReward> RemoveRedeemedReward(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/redeemedReward/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RedeemedReward>();
    }

    public async Task<RedeemedReward> UpdateRedeemedReward(RedeemedReward redeemedReward)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/redeemedReward/{redeemedReward.Id}", redeemedReward);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RedeemedReward>();
    }

    public async Task<IEnumerable<RedeemedReward>> GetRedeemedRewardsByUserId(int userId)
    {
        return await _httpClient.GetFromJsonAsync<List<RedeemedReward>>($"api/redeemedReward/user/{userId}")
                   ?? new List<RedeemedReward>();
    }

}
