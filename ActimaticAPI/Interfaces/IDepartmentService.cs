using System.Security.Principal;
using Model;

public interface IDepartmentService{
    Task<IEnumerable<Department>> GetAllDepartments();
    Task<Department> GetDepartmentById(int id);
    Task Remove(int id);
    
}