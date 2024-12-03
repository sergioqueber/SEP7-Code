namespace Interfaces;
using System.Security.Principal;
using Model;

public interface IReportService{
    Task<IEnumerable<Report>> GetAllReports();
    Task<Report?> GetReportById(int id);
    Task<Report?> Remove(int id);
    Task<Report> Create(Report report);
    Task<Report?> Update(Report report);

}