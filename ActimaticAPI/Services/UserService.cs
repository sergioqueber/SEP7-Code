using Interfaces;
using Model;

namespace Services;
public class UserService : IUserService
{

    private static List<User> UserList = new List<User>();

    static UserService()
    {

    }

    public Task<User> CreateUser(User user)
    {
        UserList.Add(user);
        return Task.FromResult(user);
    }

    public Task<IEnumerable<User>> GetAllUsers()
    {
        return Task.FromResult(UserList.AsEnumerable());
    }

    public Task<User> GetUserById(int id)
    {
        return Task.FromResult(UserList.FirstOrDefault(x => x.Id == id));
    }

    public Task<User> RemoveUser(int id)
    {
        var user = UserList.FirstOrDefault(x => x.Id == id);
        UserList.Remove(user);
        return Task.FromResult(user);
    }

    public Task<User> UpdateUser(User user)
    {
        var userToUpdate = UserList.FirstOrDefault(x => x.Id == user.Id);
        userToUpdate.Name = user.Name;
        userToUpdate.Surname = user.Surname;
        userToUpdate.Email = user.Email;
        userToUpdate.Password = user.Password;
        return Task.FromResult(userToUpdate);
    }

}