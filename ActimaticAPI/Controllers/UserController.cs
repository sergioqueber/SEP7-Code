namespace Controllers;
using Model;
using Services;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
//
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
    {
        var user = await _userService.GetAllUsers();
        return Ok(user);
    }

    [HttpGet("{id}")]
    public async Task<User> GetUserById(int id)
    {
        return await _userService.GetUserById(id);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        var newUser = await _userService.CreateUser(user);
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(int id, [FromBody] User user)
    {
        await _userService.UpdateUser(user);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveUser(int id)
    {
        await _userService.RemoveUser(id);
        return NoContent();
    }
}