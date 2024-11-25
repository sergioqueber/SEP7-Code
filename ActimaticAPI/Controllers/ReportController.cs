namespace Controllers;
using Model;
using Services;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]

public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Report>>> GetAllReport()
    {
        var report = await _reportService.GetAllReports();
        return Ok(report);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Report>> GetReportById(int id)
    {
        return await _reportService.GetReportById(id);
    }

    [HttpPost]
    public async Task<ActionResult<Report>> CreateReport([FromBody] Report report)
    {
        var newReport = await _reportService.Create(report);
        return CreatedAtAction(nameof(GetReportById), new { id = newReport.Id }, newReport);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateReport([FromBody] Report report)
    {
        await _reportService.Update(report);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<Report> RemoveReport(int id)
    {
        return await _reportService.Remove(id);
    }
}