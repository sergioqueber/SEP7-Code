namespace AppInterfaces;
using System.Security.Claims;
using Model;
public interface IAuthService
{
    public Task LoginAsync(int id, string password);
    public Task LogoutAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Task<User> RegisterUserAsync(User user);

}