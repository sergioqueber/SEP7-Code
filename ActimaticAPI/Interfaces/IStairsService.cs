namespace Interfaces;
using Model;
public interface IStairsService
{
    Task<IEnumerable<Stairs>> GetAllStairsAsync();
    Task<Stairs> GetStairsByIdAsync(int id);
    Task<Stairs> CreateStairsAsync(Stairs Stairs);
    Task<Stairs> UpdateStairsAsync(Stairs Stairs);
    Task<Stairs> RemoveStairsAsync(int id); 
    Task<IEnumerable<Stairs>> GetStairsByDatesAsync(DateOnly startDate, DateOnly endDate);   

    
}