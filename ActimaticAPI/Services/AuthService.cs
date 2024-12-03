namespace Services;
using System.ComponentModel.DataAnnotations;
using Interfaces;
using Model;
using Storage;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }
    public Task<User> ValidateUser(int id, string password)
    {
        User? existingUser = _context.Users.FirstOrDefault(s => s.Id == id) ?? throw new Exception("User not found");
        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

    public async Task RegisterUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }


}