namespace Controllers;
using Model;
using Services;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class CarPoolController : ControllerBase
{
    private readonly ICarPoolService _carPoolService;

    public CarPoolController(ICarPoolService carPoolService)
    {
        _carPoolService = carPoolService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarPool>>> GetAllCarPool()
    {
        var carPool = await _carPoolService.GetAllCarPool();
        return Ok(carPool);
    }

    [HttpGet("{id}")]
    public async Task<CarPool> GetCarPoolById(int id)
    {
        return await _carPoolService.GetCarPoolById(id);
    }

    [HttpPost]
    public async Task<ActionResult<CarPool>> CreateCarPool(CarPool carPool)
    {
        var newCarPool = await _carPoolService.CreateCarPool(carPool);
        return CreatedAtAction(nameof(GetCarPoolById), new { id = newCarPool.Id }, newCarPool);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCarPool([FromBody] CarPool carPool)
    {
        await _carPoolService.UpdateCarPool(carPool);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<CarPool> RemoveCarPool(int id)
    {
        return await _carPoolService.RemoveCarPool(id);
    }

    [HttpGet("dates/{startDate}/{endDate}")]
    public async Task<ActionResult<IEnumerable<CarPool>>> GetCarPoolByDates(DateOnly startDate, DateOnly endDate){
        var carpools = await _carPoolService.GetCarPoolByDatesAsync(startDate, endDate);
        return Ok(carpools);
    }
}