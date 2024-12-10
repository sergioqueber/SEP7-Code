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
        var Stairs = await _StairsService.GetAllStairsAsync();
        return Ok(Stairs);
    }

    [HttpGet("{id}")]
    public async Task<Stairs> GetStairsById(int id)
    {
        return await _StairsService.GetStairsByIdAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<Stairs>> CreateStairs(Stairs Stairs)
    {
        var newStairs = await _StairsService.CreateStairsAsync(Stairs);
        return CreatedAtAction(nameof(GetStairsById), new { id = newStairs.Id }, newStairs);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateStairs([FromBody] Stairs Stairs)
    {
        await _StairsService.UpdateStairsAsync(Stairs);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<Stairs> RemoveStairs(int id)
    {
        return await _StairsService.RemoveStairsAsync(id);
    }
    [HttpGet("dates/{startDate}/{endDate}")]
    public async Task<ActionResult<IEnumerable<Stairs>>> GetStairsByDates(DateOnly startDate, DateOnly endDate){
        var stairs = await _StairsService.GetStairsByDatesAsync(startDate, endDate);
        return Ok(stairs);
    }
}