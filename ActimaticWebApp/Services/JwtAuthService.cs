namespace AppServices;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using AppInterfaces;
using Dto;
using Microsoft.JSInterop;
using Model;



public class JwtAuthService(HttpClient client, IJSRuntime jsRuntime) : IAuthService
{
    // this private variable for simple caching
    public string Jwt { get; private set; } = "";

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;

    public async Task LoginAsync(int id, string password)
    {
        string userAsJson = JsonSerializer.Serialize(new UserLogInDTO { Id = id, Password = password });
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("/auth/login", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(response.IsSuccessStatusCode);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        string token = responseContent;
        Jwt = token;
        Console.WriteLine("Token Received");
        await CacheTokenAsync();
        Console.WriteLine("Token Cached");


        ClaimsPrincipal principal = await CreateClaimsPrincipal();

        OnAuthStateChanged.Invoke(principal);

    }


    private async Task<ClaimsPrincipal> CreateClaimsPrincipal()
    {
        var cachedToken = await GetTokenFromCacheAsync();
        if (string.IsNullOrEmpty(Jwt) && string.IsNullOrEmpty(cachedToken))
        {
            return new ClaimsPrincipal();
        }
        if (!string.IsNullOrEmpty(cachedToken))
        {
            Jwt = cachedToken;
        }
        if (!client.DefaultRequestHeaders.Contains("Authorization"))
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Jwt);

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);
        
        ClaimsIdentity identity = new(claims, "jwt");
        ClaimsPrincipal principal = new(identity);
        return principal;
    }

    public async Task LogoutAsync()
    {
        await ClearTokenFromCacheAsync();
        Jwt = "";
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
    }

    public async Task<User> RegisterUserAsync(User user)
    {

        string userAsJson = JsonSerializer.Serialize(user);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("api/User", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        return JsonSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync());
    }

    public async Task<ClaimsPrincipal> GetAuthAsync()
    {
        ClaimsPrincipal principal = await CreateClaimsPrincipal();
        return principal;
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes) ?? [];
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }

    private async Task<string?> GetTokenFromCacheAsync()
    {
        return await jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
    }

    private async Task CacheTokenAsync()
    {
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", "jwt", Jwt);
    }

    private async Task ClearTokenFromCacheAsync()
    {
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", "jwt", "");
    }

}