namespace Interfaces;
using Model;
public interface IStaffService
{
    Task<IEnumerable<Staff>> GetAllStaff();
     Task<Staff> GetStaffById(int id);
    
}