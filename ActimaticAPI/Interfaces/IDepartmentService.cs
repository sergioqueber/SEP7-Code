namespace Interfaces;
using System.Security.Principal;
using Model;

public interface IDepartmentService{
    Task<IEnumerable<Department>> GetAllDepartments();
    Task<Department> GetDepartmentById(int id);
    Task<Department> Remove(int id);
    Task<Department> Create(Department department);
    Task<Department> Update(Department department);

}