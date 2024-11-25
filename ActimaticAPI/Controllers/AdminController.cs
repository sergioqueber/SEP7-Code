namespace Controllers;
using Model;
using Services;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
//
public class AdminController : ControllerBase
{
    private readonly IAdminService _AdminService;

    public AdminController(IAdminService AdminService)
    {
        _AdminService = AdminService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Admin>>> GetAllAdmin()
    {
        var Admin = await _AdminService.GetAllAdmin();
        return Ok(Admin);
    }

    [HttpGet("{id}")]
    public async Task<Admin> GetAdminById(int id)
    {
        return await _AdminService.GetAdminById(id);
    }

    [HttpPost]
    public async Task<ActionResult<Admin>> CreateAdmin(Admin Admin)
    {
        var newAdmin = await _AdminService.CreateAdmin(Admin);
        return CreatedAtAction(nameof(GetAdminById), new { id = newAdmin.Id }, newAdmin);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAdmin([FromBody] Admin Admin)
    {
        await _AdminService.UpdateAdmin(Admin);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveAdmin(int id)
    {
        await _AdminService.RemoveAdmin(id);
        return NoContent();
    }
}