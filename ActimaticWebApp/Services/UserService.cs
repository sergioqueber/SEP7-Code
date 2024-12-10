using System.Collections.Generic;
using System.Linq;
using Model;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AppServices;

public class UserService : IUserService
{

    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _httpClient.GetFromJsonAsync<List<User>>("http://localhost:5205/api/user")
                   ?? new List<User>();
    }
    public async Task<User?> GetUserById(int id)
    {
        return await _httpClient.GetFromJsonAsync<User>($"api/user/{id}");
    }
    public async Task<User> CreateUser(User user)
    {
        var response = await _httpClient.PostAsJsonAsync("api/user", user);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>();
    }
    public async Task<User> RemoveUser(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/user/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>();
    }
    public async Task<User?> UpdateUser(User user)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/user/{user.Id}", user);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>();
    }
}
