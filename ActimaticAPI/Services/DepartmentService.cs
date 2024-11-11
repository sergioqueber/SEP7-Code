using Interfaces;
using Model;

namespace Services;
public class DepartmentService : IDepartmentService{

    private static List<Department> departmentList = new List<Department>();

    static DepartmentService(){

    }

    public Task<Department> Create(Department department)
    {
        departmentList.Add(department);
        return Task.FromResult(department);
    }
    
    public Task<IEnumerable<Department>> GetAllDepartments()
    {
        return Task.FromResult(departmentList.AsEnumerable());
    }

    public Task<Department> GetDepartmentById(int id)
    {
        return Task.FromResult(departmentList.FirstOrDefault(x => x.Id == id));
    }

    public Task<Department> Remove(int id)
    {
        var department = departmentList.FirstOrDefault(x => x.Id == id);
        departmentList.Remove(department);
        return Task.FromResult(department);
    }

    public Task<Department> Update(Department department)
    {
        var departmentToUpdate = departmentList.FirstOrDefault(x => x.Id == department.Id);
        departmentToUpdate.Name = department.Name;
        return Task.FromResult(departmentToUpdate);
    }
    
}