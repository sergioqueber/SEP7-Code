using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using SQLitePCL;
using Storage;

namespace Services;
public class CarPoolService(ApplicationDbContext context) : ICarPoolService{

    private readonly ApplicationDbContext _context = context;
    private const int BasePointsPerKm = 1;

    static CarPoolService(){
       
    }

    private int CalculatePoints(CarPool carPool)
    {
        int basepoints =  carPool.Distance * BasePointsPerKm;
        int typeBonus = carPool.CarType switch
        {
            "Small" => 20,
            "Medium" => 10,
            "Large" => 5,
            _ => 0
        };
        double occupancyRatio = (double)carPool.FilledSeats/carPool.EmptySeats;
        int occupancyBonus = occupancyRatio switch
        {
            >= 0.75 => 10, // 75-100% occupancy
            >= 0.5 => 5,   // 50-74% occupancy
            >= 0.25 => 2,  // 25-49% occupancy
            _ => 0
        };
        return basepoints + typeBonus + occupancyBonus;
    }

    public async Task<CarPool> CreateCarPool(CarPool carPool)
    {   
        carPool.AwardedPoints = CalculatePoints(carPool);
        _context.CarPools.Add(carPool);
        await _context.SaveChangesAsync();
        return await Task.FromResult(carPool);
    }
    
    public async Task<IEnumerable<CarPool>> GetAllCarPool()
    {
        return await Task.FromResult(_context.CarPools.AsEnumerable());
    }

    public async Task<CarPool?> GetCarPoolById(int id)
    {
        return await Task.FromResult( await _context.CarPools.FirstOrDefaultAsync(x => x.Id == id));
    }

    public async Task<CarPool?> RemoveCarPool(int id)
    {
        var carPool = await _context.CarPools.FirstOrDefaultAsync(x => x.Id == id);
        _context.CarPools.Remove(carPool);
        await _context.SaveChangesAsync();
        return await Task.FromResult(carPool);
    }

    public async Task<CarPool?> UpdateCarPool(CarPool carPool)
    {
        var carPoolToUpdate = await _context.CarPools.FirstOrDefaultAsync(x => x.Id == carPool.Id);
        if (carPoolToUpdate != null){
            carPoolToUpdate.Id = carPool.Id;
            carPoolToUpdate.Name = carPool.Name;
            carPoolToUpdate.AwardedPoints = carPool.AwardedPoints;
            carPoolToUpdate.Date = carPool.Date;
            carPoolToUpdate.Distance = carPool.Distance;
            carPoolToUpdate.EmptySeats = carPool.EmptySeats;
            carPoolToUpdate.CarType = carPool.CarType;
            await _context.SaveChangesAsync();
        }
        return await Task.FromResult(carPoolToUpdate);
    }
    public async Task<IEnumerable<CarPool>> GetCarPoolByDatesAsync(DateOnly startDate, DateOnly endDate)
    {
        return await Task.FromResult(_context.CarPools.Where(cp => cp.Date >= startDate && cp.Date <= endDate).AsEnumerable());
    }
    
}