using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using SQLitePCL;
using Storage;

namespace Services;
public class CarPoolService(ApplicationDbContext context) : ICarPoolService{

    private readonly ApplicationDbContext _context = context;

    static CarPoolService(){
       
    }

    public async Task<CarPool> CreateCarPool(CarPool carPool)
    {
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
    
}