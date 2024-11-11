using Interfaces;
using Model;

namespace Services;
public class SavingFoodService : ISavingFoodService{

    private static List<SavingFood> savingFoodList = new List<SavingFood>();

    static SavingFoodService(){

    }

    public Task<SavingFood> CreateSavingFood(SavingFood savingFood)
    {
        savingFoodList.Add(savingFood);
        return Task.FromResult(savingFood);
    }
    
    public Task<IEnumerable<SavingFood>> GetAllSavingFood()
    {
        return Task.FromResult(savingFoodList.AsEnumerable());
    }

    public Task<SavingFood> GetSavingFoodById(int id)
    {
        return Task.FromResult(savingFoodList.FirstOrDefault(x => x.Id == id));
    }

    public Task<SavingFood> RemoveSavingFood(int id)
    {
        var savingFood = savingFoodList.FirstOrDefault(x => x.Id == id);
        savingFoodList.Remove(savingFood);
        return Task.FromResult(savingFood);
    }

    public Task<SavingFood> UpdateSavingFood(SavingFood savingFood)
    {
        var savingFoodToUpdate = savingFoodList.FirstOrDefault(x => x.Id == savingFood.Id);
        savingFoodToUpdate.Name = savingFood.Name;
        savingFoodToUpdate.AwardedPoints = savingFood.AwardedPoints;
        savingFoodToUpdate.Date = savingFood.Date;
        savingFoodToUpdate.PackagesSaved = savingFood.PackagesSaved;
        return Task.FromResult(savingFoodToUpdate);
    }
    
}