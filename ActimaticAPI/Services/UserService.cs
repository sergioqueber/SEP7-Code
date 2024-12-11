using Interfaces;
using Storage;
using Microsoft.EntityFrameworkCore.Storage;
using Model;
using Microsoft.EntityFrameworkCore;

namespace Services;
public class UserService(ApplicationDbContext context) : IUserService
{
    private readonly ApplicationDbContext _context = context ;

//private static List<User> UserList = new List<User>();

    static UserService()
    {

    }

    public async Task<User> CreateUser(User user)
    {
        using ApplicationDbContext _context = new();
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await Task.FromResult( _context.Users.AsEnumerable());
    }

    public async Task<User?> GetUserById(int id)
    {
        return await Task.FromResult(await _context.Users.FirstOrDefaultAsync(x => x.Id == id));
    }

    public async Task<User> RemoveUser(int id)
    {
        var user =  await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user != null)
        {
            _context.Remove(user);
        }
        await _context.SaveChangesAsync();
        return await Task.FromResult(user);
    }

    public async Task<User?> UpdateUser(User user)
    {
        var userToUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
        if (userToUpdate != null)
        {
            userToUpdate.Points = user.Points;
            userToUpdate.Role = user.Role;
            userToUpdate.Team = user.Team;
            userToUpdate.Name = user.Name;
            userToUpdate.Surname = user.Surname;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
            userToUpdate.TeamId = user.TeamId;
            _context.Users.Update(userToUpdate);
            await _context.SaveChangesAsync();
        }
        return await Task.FromResult(userToUpdate);
    }

}