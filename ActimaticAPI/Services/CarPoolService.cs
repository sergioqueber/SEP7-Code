using Interfaces;
using Model;

namespace Services;
public class CarPoolService : ICarPoolService{

    private static List<CarPool> carPoolList = new List<CarPool>();

    static CarPoolService(){

    }

    public Task<CarPool> CreateCarPool(CarPool carPool)
    {
        carPoolList.Add(carPool);
        return Task.FromResult(carPool);
    }
    
    public Task<IEnumerable<CarPool>> GetAllCarPool()
    {
        return Task.FromResult(carPoolList.AsEnumerable());
    }

    public Task<CarPool> GetCarPoolById(int id)
    {
        return Task.FromResult(carPoolList.FirstOrDefault(x => x.Id == id));
    }

    public Task<CarPool> RemoveCarPool(int id)
    {
        var carPool = carPoolList.FirstOrDefault(x => x.Id == id);
        carPoolList.Remove(carPool);
        return Task.FromResult(carPool);
    }

    public Task<CarPool> UpdateCarPool(CarPool carPool)
    {
        var carPoolToUpdate = carPoolList.FirstOrDefault(x => x.Id == carPool.Id);
        carPoolToUpdate.Name = carPool.Name;
        carPoolToUpdate.AwardedPoints = carPool.AwardedPoints;
        carPoolToUpdate.Date = carPool.Date;
        carPoolToUpdate.Distance = carPool.Distance;
        carPoolToUpdate.EmptySeats = carPool.EmptySeats;
        carPoolToUpdate.CarType = carPool.CarType;
        return Task.FromResult(carPoolToUpdate);
    }
    
}