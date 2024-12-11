namespace Controllers;
using Model;
using Services;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class VolunteeringController : ControllerBase
{
    private readonly IVolunteeringService _volunteeringService;

    public VolunteeringController(IVolunteeringService volunteeringService)
    {
        _volunteeringService = volunteeringService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Volunteering>>> GetAll()
    {
        var volunteeringActivities = await _volunteeringService.GetAllVolunteeringAsync();
        return Ok(volunteeringActivities);
    }


    
    [HttpGet("{id}")]
    public async Task<Volunteering> GetById(int id)
    {
        return await _volunteeringService.GetVolunteeringByIdAsync(id);
    }

   
    [HttpPost]
    public async Task<ActionResult<Volunteering>> Create(Volunteering volunteering)
    {
        var newVolunteering = await _volunteeringService.CreateVolunteeringAsync(volunteering);
        return CreatedAtAction(nameof(GetById), new { id = newVolunteering.Id }, newVolunteering);
    }

   
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] Volunteering volunteering)
    {
        await _volunteeringService.UpdateVolunteeringAsync(volunteering);
        return NoContent();
    }
    

   
    [HttpDelete("{id}")]
    public async Task<Volunteering> Delete(int id){
        return await _volunteeringService.RemoveVolunteeringAsync(id);
    }

    [HttpGet("dates/{startDate}/{endDate}")]
    public async Task<ActionResult<IEnumerable<Volunteering>>> GetAllVolunteeringByDate(DateOnly startDate, DateOnly endDate)
    {
        var volunteerings = await _volunteeringService.GetVolunteeringByDatesAsync(startDate, endDate);
        return Ok(volunteerings);
    }
}