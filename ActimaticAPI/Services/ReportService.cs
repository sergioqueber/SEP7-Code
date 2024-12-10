using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using Storage;

namespace Services;
public class ReportService(ApplicationDbContext context) : IReportService
{
    private readonly ApplicationDbContext _context = context;
    static ReportService()
    {

    }

    public async Task<Report> Create(Report report)
    {
        await _context.Reports.AddAsync(report);
        await _context.SaveChangesAsync();
        return await Task.FromResult(report);
    }

    public async Task<IEnumerable<Report>> GetAllReports()
    {
        return await Task.FromResult(_context.Reports.AsEnumerable());
    }

    public async Task<Report?> GetReportById(int id)
    {
        return await Task.FromResult(await _context.Reports.FirstOrDefaultAsync(x => x.Id == id));
    }

    public async Task<Report> Remove(int id)
    {
        var report = await _context.Reports.FirstOrDefaultAsync(x => x.Id == id);
        if (report != null)
        {
            _context.Reports.Remove(report);
        }
        await _context.SaveChangesAsync();
        return await Task.FromResult(report);
    }

    public async Task<Report?> Update(Report report)
    {
        var reportToUpdate = await _context.Reports.FirstOrDefaultAsync(x => x.Id == report.Id);
        if (reportToUpdate != null)
        {
            reportToUpdate.StartDate = report.StartDate;
            reportToUpdate.EndDate = report.EndDate;
            reportToUpdate.ActiveParticipants = report.ActiveParticipants;
            reportToUpdate.AwardedRewards = report.AwardedRewards;
            reportToUpdate.CompletedActivities = report.CompletedActivities;
            reportToUpdate.EmissionsSaved = report.EmissionsSaved;
            await _context.SaveChangesAsync();
        }

        return await Task.FromResult(reportToUpdate);
    }

}