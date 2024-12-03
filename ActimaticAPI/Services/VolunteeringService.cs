using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using Storage;


namespace Services;
public class VolunteeringService (ApplicationDbContext context) : IVolunteeringService{

    private readonly ApplicationDbContext _context = context;

    static VolunteeringService(){

    }

    public async Task<Volunteering> CreateVolunteeringAsync(Volunteering volunteering)
    {
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
        await _context.SaveChangesAsync();
        }
        return await Task.FromResult(volunteeringToUpdate);
        
    }
    
}