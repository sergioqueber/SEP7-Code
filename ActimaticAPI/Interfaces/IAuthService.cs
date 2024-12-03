using Model;
namespace Interfaces;

public interface IAuthService
{
    //Task<User> Register(User user);
    Task<User> ValidateUser (int id, string password);
    Task RegisterUser(User user);
}