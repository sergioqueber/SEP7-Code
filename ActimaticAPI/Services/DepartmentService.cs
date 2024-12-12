using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using Storage;

namespace Services;
public class DepartmentService(ApplicationDbContext context) : IDepartmentService{

    private readonly ApplicationDbContext _context = context;

    static DepartmentService(){

    }

    public async Task<Department> Create(Department department)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        return await Task.FromResult(department);
    }
    
    public async Task<IEnumerable<Department>> GetAllDepartments()
    {
        return await Task.FromResult(_context.Departments.AsEnumerable());
    }

    public async Task<Department?> GetDepartmentById(int id)
    {
        return await Task.FromResult(await _context.Departments.FirstOrDefaultAsync(x => x.Id == id));
    }

    public async Task<Department?> Remove(int id)
    {
        var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        return await Task.FromResult(department);
    }

    public async Task<Department?> Update(Department department)
    {
        var departmentToUpdate = await _context.Departments.FirstOrDefaultAsync(x => x.Id == department.Id);
        if (departmentToUpdate != null){
            departmentToUpdate.Id = department.Id;
            departmentToUpdate.Name = department.Name;
            departmentToUpdate.Teams = department.Teams;
            _context.Departments.Update(departmentToUpdate);
        }
        await _context.SaveChangesAsync();
        return await Task.FromResult(departmentToUpdate);
    }
    
}