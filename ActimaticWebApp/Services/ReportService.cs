using System.Collections.Generic;
using System.Linq;
using Model;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AppServices;

public class ReportService : IReportService
{

    private readonly HttpClient _httpClient;

    public ReportService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<Report>> GetAllReports()
    {
        return await _httpClient.GetFromJsonAsync<List<Report>>("api/report")
                   ?? new List<Report>();
    }

    public async Task<Report?> GetReportById(int id)
    {
        return await _httpClient.GetFromJsonAsync<Report>($"api/report/{id}");
    }
    public async Task<Report> Create(Report report)
    {
        var response = await _httpClient.PostAsJsonAsync("api/report", report);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Report>();
    }
    public async Task<Report> Remove(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/report/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Report>();
    }
    public async Task<Report?> Update(Report report)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/report/{report.Id}", report);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Report>();
    }
}
