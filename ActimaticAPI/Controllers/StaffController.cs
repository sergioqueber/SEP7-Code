namespace Controllers;
using Model;
using Services;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class StaffController : ControllerBase
{
    private readonly IStaffService _staffService;

    public StaffController(IStaffService staffService)
    {
        _staffService = staffService;
    }

    [HttpGet]
    public async Task<IEnumerable<Staff>> GetAllStaff()
    {
        return await _staffService.GetAllStaff();
    }

    [HttpGet("{id}")]
    public async Task<Staff> GetStaffById(int id)
    {
        return await _staffService.GetStaffById(id);
    }

    [HttpPost]
    public async Task<Staff> CreateStaff([FromBody] Staff staff)
    {
        return await _staffService.CreateStaff(staff);
    }

    [HttpPut]
    public async Task<Staff> UpdateStaff([FromBody] Staff staff)
    {
        return await _staffService.UpdateStaff(staff);
    }

    [HttpDelete("{id}")]
    public async Task<Staff> RemoveStaff(int id)
    {
        return await _staffService.RemoveStaff(id);
    }
}