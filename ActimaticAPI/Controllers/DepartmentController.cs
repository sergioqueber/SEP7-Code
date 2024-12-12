namespace Controllers;
using Model;
using Services;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]

public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartment()
    {
        var department = await _departmentService.GetAllDepartments();
        return Ok(department);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartmentById(int id)
    {
        var department = await _departmentService.GetDepartmentById(id);
        if (department == null)
        {
            return NotFound();
        }
        return Ok(department);
    }

    [HttpPost]
    public async Task<ActionResult<Department>> CreateDepartment([FromBody] Department department)
    {
        var newDepartment = await _departmentService.Create(department);
        return CreatedAtAction(nameof(GetDepartmentById), new { id = newDepartment.Id }, newDepartment);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Department department)
    {
        if (id != department.Id)
        {
            return BadRequest("Department ID mismatch");
        }

        var updatedDepartment = await _departmentService.Update(department);
        if (updatedDepartment == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Department>> RemoveDepartment(int id)
    {
        var removedDepartment = await _departmentService.Remove(id);
        if (removedDepartment == null)
        {
            return NotFound();
        }
        return Ok(removedDepartment);
    }
}