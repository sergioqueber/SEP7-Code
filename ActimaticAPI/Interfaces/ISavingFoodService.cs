namespace Interfaces;
using Model;
public interface ISavingFoodService
{
    Task<IEnumerable<SavingFood>> GetAllSavingFoodAsync();
    Task<SavingFood> GetSavingFoodByIdAsync(int id);
    Task<SavingFood> CreateSavingFoodAsync(SavingFood savingFood);
    Task<SavingFood> UpdateSavingFoodAsync(SavingFood savingFood);
    Task<SavingFood> RemoveSavingFoodAsync(int id);  
    Task<IEnumerable<SavingFood>> GetSavingFoodByDatesAsync(DateOnly startDate, DateOnly endDate);  

    
}