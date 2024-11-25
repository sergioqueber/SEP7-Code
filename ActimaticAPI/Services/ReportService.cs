using Interfaces;
using Model;

namespace Services;
public class ReportService : IReportService{

    private static List<Report> ReportList = new List<Report>();

    static ReportService(){

    }

    public Task<Report> Create(Report report)
    {
        ReportList.Add(report);
        return Task.FromResult(report);
    }
    
    public Task<IEnumerable<Report>> GetAllReports()
    {
        return Task.FromResult(ReportList.AsEnumerable());
    }

    public Task<Report> GetReportById(int id)
    {
        return Task.FromResult(ReportList.FirstOrDefault(x => x.Id == id));
    }

    public Task<Report> Remove(int id)
    {
        var report = ReportList.FirstOrDefault(x => x.Id == id);
        ReportList.Remove(report);
        return Task.FromResult(report);
    }

    public Task<Report> Update(Report report)
    {
        var reportToUpdate = ReportList.FirstOrDefault(x => x.Id == report.Id);
        reportToUpdate.StartDate = report.StartDate;
        reportToUpdate.EndDate = report.EndDate;
        reportToUpdate.ActiveParticipants = report.ActiveParticipants;
        reportToUpdate.AwardedRewards = report.AwardedRewards;
        reportToUpdate.CompletedActivities = report.CompletedActivities;
        reportToUpdate.EmissionsSaved = report.EmissionsSaved;
        return Task.FromResult(reportToUpdate);
    }
    
}