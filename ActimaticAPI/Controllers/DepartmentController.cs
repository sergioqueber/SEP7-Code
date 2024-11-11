namespace Controller;
using Model;
using Services;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<IEnumerable<Department>> GetAllDepartment()
    {
        return await _departmentService.GetAllDepartments();
    }

    [HttpGet("{id}")]
    public async Task<Department> GetDepartmentById(int id)
    {
        return await _departmentService.GetDepartmentById(id);
    }

    [HttpPost]
    public async Task<Department> CreateDepartment([FromBody] Department Department)
    {
        return await _departmentService.Create(Department);
    }

    [HttpPut]
    public async Task<Department> UpdateDepartment([FromBody] Department department)
    {
        return await _departmentService.Update(department);
    }

    [HttpDelete("{id}")]
    public async Task<Department> RemoveDepartment(int id)
    {
        return await _departmentService.Remove(id);
    }
}