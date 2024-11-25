namespace Interfaces;
using Model;
public interface IAdminService
{
    Task<IEnumerable<Admin>> GetAllAdmin();
    Task<Admin> GetAdminById(int id);
    Task<Admin> CreateAdmin(Admin admin);
    Task<Admin> UpdateAdmin(Admin admin);
    Task<Admin> RemoveAdmin(int id);    

    
}