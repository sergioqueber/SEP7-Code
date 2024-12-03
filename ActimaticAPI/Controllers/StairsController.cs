namespace Controllers;
using Model;
using Services;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class StairsController : ControllerBase
{
    private readonly IStairsService _StairsService;

    public StairsController(IStairsService StairsService)
    {
        _StairsService = StairsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Stairs>>> GetAllStairs()
    {
        var Stairs = await _StairsService.GetAllStairs();
        return Ok(Stairs);
    }

    [HttpGet("{id}")]
    public async Task<Stairs> GetStairsById(int id)
    {
        return await _StairsService.GetStairsById(id);
    }

    [HttpPost]
    public async Task<ActionResult<Stairs>> CreateStairs(Stairs Stairs)
    {
        var newStairs = await _StairsService.CreateStairs(Stairs);
        return CreatedAtAction(nameof(GetStairsById), new { id = newStairs.Id }, newStairs);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateStairs([FromBody] Stairs Stairs)
    {
        await _StairsService.UpdateStairs(Stairs);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<Stairs> RemoveStairs(int id)
    {
        return await _StairsService.RemoveStairs(id);
    }
}