using Interfaces;
using Model;

namespace Services;
public class StairsService : IStairsService{

    private static List<Stairs> StairsList = new List<Stairs>();

    static StairsService(){

    }

    public Task<Stairs> CreateStairs(Stairs Stairs)
    {
        StairsList.Add(Stairs);
        return Task.FromResult(Stairs);
    }
    
    public Task<IEnumerable<Stairs>> GetAllStairs()
    {
        return Task.FromResult(StairsList.AsEnumerable());
    }

    public Task<Stairs> GetStairsById(int id)
    {
        return Task.FromResult(StairsList.FirstOrDefault(x => x.Id == id));
    }

    public Task<Stairs> RemoveStairs(int id)
    {
        var Stairs = StairsList.FirstOrDefault(x => x.Id == id);
        StairsList.Remove(Stairs);
        return Task.FromResult(Stairs);
    }

    public Task<Stairs> UpdateStairs(Stairs Stairs)
    {
        var StairsToUpdate = StairsList.FirstOrDefault(x => x.Id == Stairs.Id);
        StairsToUpdate.Name = Stairs.Name;
        StairsToUpdate.AwardedPoints = Stairs.AwardedPoints;
        StairsToUpdate.Date = Stairs.Date;
        StairsToUpdate.Floors = Stairs.Floors;
        return Task.FromResult(StairsToUpdate);
    }
    
}