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
    public IActionResult GetAll()
    {
        var volunteeringActivities = _volunteeringService.GetAllVolunteeringAsync();
        return Ok(volunteeringActivities);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var volunteeringActivity = _volunteeringService.GetVolunteeringByIdAsync(id);
        if (volunteeringActivity == null)
        {
            return NotFound();
        }
        return Ok(volunteeringActivity);
    }

    [HttpPost]
    public IActionResult Create(Volunteering volunteering)
    {
        _volunteeringService.CreateVolunteeringAsync(volunteering);
        return CreatedAtAction(nameof(GetById), new { id = volunteering.Id }, volunteering);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Volunteering volunteering)
    {
        if (id != volunteering.Id)
        {
            return BadRequest();
        }

        var existingVolunteering = _volunteeringService.GetVolunteeringByIdAsync(id);
        if (existingVolunteering == null)
        {
            return NotFound();
        }

        _volunteeringService.UpdateVolunteeringAsync(volunteering);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var volunteering = _volunteeringService.GetVolunteeringByIdAsync(id);
        if (volunteering == null)
        {
            return NotFound();
        }

        _volunteeringService.RemoveVolunteeringAsync(id);
        return NoContent();
    }

    [HttpGet("dates/{startDate}/{endDate}")]
    public async Task<ActionResult<IEnumerable<Volunteering>>> GetAllVolunteeringByDate(DateOnly startDate, DateOnly endDate)
    {
        var volunteerings = await _volunteeringService.GetVolunteeringByDatesAsync(startDate, endDate);
        return Ok(volunteerings);
    }
}