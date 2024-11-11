namespace Interfaces;
using Model;
public interface ICarPoolService
{
    Task<IEnumerable<CarPool>> GetAllCarPool();
    Task<CarPool> GetCarPoolById(int id);
    Task<CarPool> CreateCarPool(CarPool carPool);
    Task<CarPool> UpdateCarPool(CarPool carPool);
    Task<CarPool> RemoveCarPool(int id);    

    
}