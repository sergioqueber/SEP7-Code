namespace Interfaces;
using Model;
public interface IStaffService
{
    Task<IEnumerable<Staff>> GetAllStaff();
    Task<Staff> GetStaffById(int id);
    Task<Staff> CreateStaff(Staff staff);
    Task<Staff> UpdateStaff(Staff staff);
    Task<Staff> RemoveStaff(int id);    

    
}