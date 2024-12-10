namespace Controllers;
using Model;
using Services;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class TransportController : ControllerBase
{
    private readonly ITransportService _transportService;

    public TransportController(ITransportService transportService)
    {
        _transportService = transportService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transport>>> GetAllTransport()
    {
        var transport = await _transportService.GetAllTransport();
        return Ok(transport);
    }

    [HttpGet("{id}")]
    public async Task<Transport> GetTransportById(int id)
    {
        return await _transportService.GetTransportById(id);
    }

    [HttpPost]
    public async Task<ActionResult<Transport>> CreateTransport(Transport transport)
    {
        var newTransport = await _transportService.CreateTransport(transport);
        return CreatedAtAction(nameof(GetTransportById), new { id = newTransport.Id }, newTransport);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateTransport([FromBody] Transport transport)
    {
        await _transportService.UpdateTransport(transport);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<Transport> RemoveTransport(int id)
    {
        return await _transportService.RemoveTransport(id);
    }

    [HttpGet("dates/{startDate}/{endDate}")]
    public async Task<ActionResult<IEnumerable<Transport>>> GetStairsByDates(DateOnly startDate, DateOnly endDate){
        var transports = await _transportService.GetTransportByDatesAsync(startDate, endDate);
        return Ok(transports);
    }
}