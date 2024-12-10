using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using Storage;


namespace Services;
public class StairsService (ApplicationDbContext context) : IStairsService{

    private readonly ApplicationDbContext _context = context;

    static StairsService(){

    }

    public async Task<Stairs> CreateStairsAsync(Stairs stairs)
    {
        await _context.Stairs.AddAsync(stairs);
        await _context.SaveChangesAsync();
        return await Task.FromResult(stairs);
    }
    
    public async Task<IEnumerable<Stairs>> GetAllStairsAsync()
    {
       return await Task.FromResult(_context.Stairs.AsEnumerable());
    }

    public async Task<Stairs?> GetStairsByIdAsync(int id)
    {
        return await Task.FromResult( await _context.Stairs.FirstOrDefaultAsync(x => x.Id == id));
    }

    public async Task<Stairs?> RemoveStairsAsync(int id)
    {
        var stairs = await _context.Stairs.FirstOrDefaultAsync(x => x.Id == id);
        _context.Stairs.Remove(stairs);
        await _context.SaveChangesAsync();
        return await Task.FromResult(stairs);
    }

    public async Task<Stairs?> UpdateStairsAsync(Stairs stairs)
    {
        
        var stairsToUpdate = await _context.Stairs.FirstOrDefaultAsync(x => x.Id == stairs.Id);
        if (stairsToUpdate != null){
            stairsToUpdate.Id = stairs.Id;
            stairsToUpdate.Name = stairs.Name;
            stairsToUpdate.AwardedPoints = stairs.AwardedPoints;
            stairsToUpdate.Date = stairs.Date;
            stairsToUpdate.Floors = stairs.Floors;
        await _context.SaveChangesAsync();
        }
        return await Task.FromResult(stairsToUpdate);
        
    }
    
}