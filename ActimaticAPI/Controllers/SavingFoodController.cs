namespace Controllers;
using Model;
using Services;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class SavingFoodController : ControllerBase
{
    private readonly ISavingFoodService _savingFoodService;

    public SavingFoodController(ISavingFoodService savingFoodService)
    {
        _savingFoodService = savingFoodService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SavingFood>>> GetAllSavingFood()
    {
        var savingFood = await _savingFoodService.GetAllSavingFoodAsync();
        return Ok(savingFood);
    }

    [HttpGet("{id}")]
    public async Task<SavingFood> GetSavingFoodById(int id)
    {
        return await _savingFoodService.GetSavingFoodByIdAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<SavingFood>> CreateSavingFood(SavingFood savingFood)
    {
        var newSavingFood = await _savingFoodService.CreateSavingFoodAsync(savingFood);
        return CreatedAtAction(nameof(GetSavingFoodById), new { id = newSavingFood.Id }, newSavingFood);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateSavingFood([FromBody] SavingFood savingFood)
    {
        await _savingFoodService.UpdateSavingFoodAsync(savingFood);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<SavingFood> RemoveSavingFood(int id)
    {
        return await _savingFoodService.RemoveSavingFoodAsync(id);
    }

    [HttpGet("dates/{startDate}/{endDate}")]
    public async Task<ActionResult<IEnumerable<SavingFood>>> GetAllSavingFoodByDate(DateOnly startDate, DateOnly endDate)
    {
        var savings = await _savingFoodService.GetSavingFoodByDatesAsync(startDate, endDate);
        return Ok(savings);
    }
}