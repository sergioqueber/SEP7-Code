namespace Interfaces;
using Model;
public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserById(int id);
    Task<User> CreateUser(User user);
    Task<User?> UpdateUser(User user);
    Task<User> RemoveUser(int id);
    Task<IEnumerable<User>> GetAllUsersByTeamId(int id);


}