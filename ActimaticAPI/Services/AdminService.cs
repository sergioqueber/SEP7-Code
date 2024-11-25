using Interfaces;
using Model;

namespace Services;
public class AdminService : IAdminService
{

   private static List<Admin> adminList = new List<Admin>();

   /*  static AdminService()
    {

    } */

    public async Task<Admin> CreateAdmin(Admin admin)
    {
        using ApplicationDbContext context = new();
        await context.Admins.AddAsync(admin);
        await context.SaveChangesAsync();
        return admin;
    }

    public Task<IEnumerable<Admin>> GetAllAdmin()
    {
        return Task.FromResult(adminList.AsEnumerable());
    }

    public Task<Admin> GetAdminById(int id)
    {
        return Task.FromResult(adminList.FirstOrDefault(x => x.Id == id));
    }

    public Task<Admin> RemoveAdmin(int id)
    {
        var admin = adminList.FirstOrDefault(x => x.Id == id);
        adminList.Remove(admin);
        return Task.FromResult(admin);
    }

    public Task<Admin> UpdateAdmin(Admin admin)
    {
        var adminToUpdate = adminList.FirstOrDefault(x => x.Id == admin.Id);
        adminToUpdate.Name = admin.Name;
        adminToUpdate.Surname = admin.Surname;
        adminToUpdate.Email = admin.Email;
        adminToUpdate.Password = admin.Password;
        adminToUpdate.ToApprove = admin.ToApprove;
        return Task.FromResult(adminToUpdate);
    }

}