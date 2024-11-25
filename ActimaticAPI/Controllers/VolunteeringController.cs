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
        var volunteeringActivities = _volunteeringService.GetAllVolunteering();
        return Ok(volunteeringActivities);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var volunteeringActivity = _volunteeringService.GetVolunteeringById(id);
        if (volunteeringActivity == null)
        {
            return NotFound();
        }
        return Ok(volunteeringActivity);
    }

    [HttpPost]
    public IActionResult Create(Volunteering volunteering)
    {
        _volunteeringService.CreateVolunteering(volunteering);
        return CreatedAtAction(nameof(GetById), new { id = volunteering.Id }, volunteering);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Volunteering volunteering)
    {
        if (id != volunteering.Id)
        {
            return BadRequest();
        }

        var existingVolunteering = _volunteeringService.GetVolunteeringById(id);
        if (existingVolunteering == null)
        {
            return NotFound();
        }

        _volunteeringService.UpdateVolunteering(volunteering);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var volunteering = _volunteeringService.GetVolunteeringById(id);
        if (volunteering == null)
        {
            return NotFound();
        }

        _volunteeringService.RemoveVolunteering(id);
        return NoContent();
    }
}