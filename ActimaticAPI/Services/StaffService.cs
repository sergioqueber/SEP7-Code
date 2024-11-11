using Interfaces;
using Model;

namespace Services;
public class StaffService : IStaffService{

    private static List<Staff> staffList = new List<Staff>();

    static StaffService(){

    }

    public Task<Staff> CreateStaff(Staff staff)
    {
        staffList.Add(staff);
        return Task.FromResult(staff);
    }
    
    public Task<IEnumerable<Staff>> GetAllStaff()
    {
        return Task.FromResult(staffList.AsEnumerable());
    }

    public Task<Staff> GetStaffById(int id)
    {
        return Task.FromResult(staffList.FirstOrDefault(x => x.Id == id));
    }

    public Task<Staff> RemoveStaff(int id)
    {
        var staff = staffList.FirstOrDefault(x => x.Id == id);
        staffList.Remove(staff);
        return Task.FromResult(staff);
    }

    public Task<Staff> UpdateStaff(Staff staff)
    {
        var staffToUpdate = staffList.FirstOrDefault(x => x.Id == staff.Id);
        staffToUpdate.Name = staff.Name;
        staffToUpdate.Surname = staff.Surname;
        staffToUpdate.Email = staff.Email;
        staffToUpdate.Password = staff.Password;
        return Task.FromResult(staffToUpdate);
    }
    
}