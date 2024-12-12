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
    public async Task<ActionResult<CarPool>> GetCarPoolById(int id)
    {
        var carPool = await _carPoolService.GetCarPoolById(id);
        if (carPool == null)
        {
            return NotFound();
        }
        return Ok(carPool);
    }

    [HttpPost]
    public async Task<ActionResult<CarPool>> CreateCarPool(CarPool carPool)
    {
        var newCarPool = await _carPoolService.CreateCarPool(carPool);
        return CreatedAtAction(nameof(GetCarPoolById), new { id = newCarPool.Id }, newCarPool);
    }

    [HttpPut]
    public async Task<ActionResult<CarPool>> UpdateCarPool([FromBody] CarPool carPool)
    {
        var updatedCarPool = await _carPoolService.UpdateCarPool(carPool);
        if (updatedCarPool == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CarPool>> RemoveCarPool(int id)
    {
        var removedCarPool = await _carPoolService.RemoveCarPool(id);
        if (removedCarPool == null)
        {
            return NotFound();
        }
        return Ok(removedCarPool);
    }

    [HttpGet("dates/{startDate}/{endDate}")]
    public async Task<ActionResult<IEnumerable<CarPool>>> GetAllCarPoolByDate(DateOnly startDate, DateOnly endDate)
    {
        var carPools = await _carPoolService.GetCarPoolByDatesAsync(startDate, endDate);
        return Ok(carPools);
    }
}