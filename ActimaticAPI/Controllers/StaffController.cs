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
    public async Task<ActionResult<IEnumerable<Staff>>> GetAllStaff()
    {
        var staff = await _staffService.GetAllStaff();
        return Ok(staff);
    }

    [HttpGet("{id}")]
    public async Task<Staff> GetStaffById(int id)
    {
        return await _staffService.GetStaffById(id);
    }

    [HttpPost]
    public async Task<ActionResult<Staff>> CreateStaff(Staff staff)
    {
        var newStaff = await _staffService.CreateStaff(staff);
        return CreatedAtAction(nameof(GetStaffById), new { id = newStaff.Id }, newStaff);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateStaff([FromBody] Staff staff)
    {
        await _staffService.UpdateStaff(staff);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveStaff(int id)
    {
        await _staffService.RemoveStaff(id);
        return NoContent();
    }
}