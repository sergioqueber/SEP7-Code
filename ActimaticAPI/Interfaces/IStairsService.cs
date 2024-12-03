namespace Interfaces;
using Model;
public interface IStairsService
{
    Task<IEnumerable<Stairs>> GetAllStairs();
    Task<Stairs> GetStairsById(int id);
    Task<Stairs> CreateStairs(Stairs Stairs);
    Task<Stairs> UpdateStairs(Stairs Stairs);
    Task<Stairs> RemoveStairs(int id);    

    
}