namespace Interfaces;
using Model;
public interface ISavingFoodService
{
    Task<IEnumerable<SavingFood>> GetAllSavingFood();
    Task<SavingFood> GetSavingFoodById(int id);
    Task<SavingFood> CreateSavingFood(SavingFood savingFood);
    Task<SavingFood> UpdateSavingFood(SavingFood savingFood);
    Task<SavingFood> RemoveSavingFood(int id);    

    
}