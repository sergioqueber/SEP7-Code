using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using Storage;


namespace Services;
public class VolunteeringService (ApplicationDbContext context) : IVolunteeringService{

    private readonly ApplicationDbContext _context = context;
    private const int basePointsPerHour = 40;

    static VolunteeringService(){

    }
    public int CalculatePointsAsync(Volunteering volunteering)
    {
        return volunteering.Hours * basePointsPerHour;
    }
    public async Task<Volunteering> CreateVolunteeringAsync(Volunteering volunteering)
    {
        volunteering.AwardedPoints = CalculatePointsAsync(volunteering);
        await _context.Volunteerings.AddAsync(volunteering);
        await _context.SaveChangesAsync();
        return await Task.FromResult(volunteering);
    }
    
    public async Task<IEnumerable<Volunteering>> GetAllVolunteeringAsync()
    {
       return await Task.FromResult(_context.Volunteerings.AsEnumerable());
    }

    public async Task<Volunteering?> GetVolunteeringByIdAsync(int id)
    {
        return await Task.FromResult( await _context.Volunteerings.FirstOrDefaultAsync(x => x.Id == id));
    }

    public async Task<Volunteering?> RemoveVolunteeringAsync(int id)
    {
        var volunteering = await _context.Volunteerings.FirstOrDefaultAsync(x => x.Id == id);
        _context.Volunteerings.Remove(volunteering);
        await _context.SaveChangesAsync();
        return await Task.FromResult(volunteering);
    }

    public async Task<Volunteering?> UpdateVolunteeringAsync(Volunteering volunteering)
    {
        
        var volunteeringToUpdate = await _context.Volunteerings.FirstOrDefaultAsync(x => x.Id == volunteering.Id);
        if (volunteeringToUpdate != null){
            volunteeringToUpdate.Id = volunteering.Id;
            volunteeringToUpdate.Name = volunteering.Name;
            volunteeringToUpdate.AwardedPoints = volunteering.AwardedPoints;
            volunteeringToUpdate.Date = volunteering.Date;
            volunteeringToUpdate.Hours = volunteering.Hours;
            volunteeringToUpdate.Location = volunteering.Location;
        _context.Volunteerings.Update(volunteeringToUpdate);
        await _context.SaveChangesAsync();
        }
        return await Task.FromResult(volunteeringToUpdate);
        
    }
    public async Task<IEnumerable<Volunteering>> GetVolunteeringByDatesAsync(DateOnly startDate, DateOnly endDate)
    {
        return await Task.FromResult(_context.Volunteerings.Where(v => v.Date >= startDate && v.Date <= endDate).AsEnumerable());
    }
    
}