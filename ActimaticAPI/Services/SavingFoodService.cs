using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using Storage;


namespace Services;
public class SavingFoodService (ApplicationDbContext context) : ISavingFoodService{

    private readonly ApplicationDbContext _context = context;
    private const int basePointsPerPackage = 5;

    static SavingFoodService(){

    }
    public int CalculatePointsAsync(SavingFood savingFood)
    {
        return savingFood.PackagesSaved * basePointsPerPackage;
    }

    public async Task<SavingFood> CreateSavingFoodAsync(SavingFood savingFood)
    {
        savingFood.AwardedPoints = CalculatePointsAsync(savingFood);
        await _context.SavingFoods.AddAsync(savingFood);
        await _context.SaveChangesAsync();
        return await Task.FromResult(savingFood);
    }
    
    public async Task<IEnumerable<SavingFood>> GetAllSavingFoodAsync()
    {
       return await Task.FromResult(_context.SavingFoods.AsEnumerable());
    }

    public async Task<SavingFood?> GetSavingFoodByIdAsync(int id)
    {
        return await Task.FromResult( await _context.SavingFoods.FirstOrDefaultAsync(x => x.Id == id));
    }

    public async Task<SavingFood?> RemoveSavingFoodAsync(int id)
    {
        var savingFood = await _context.SavingFoods.FirstOrDefaultAsync(x => x.Id == id);
        _context.SavingFoods.Remove(savingFood);
        await _context.SaveChangesAsync();
        return await Task.FromResult(savingFood);
    }

    public async Task<SavingFood?> UpdateSavingFoodAsync(SavingFood savingFood)
    {
        
        var savingFoodToUpdate = await _context.SavingFoods.FirstOrDefaultAsync(x => x.Id == savingFood.Id);
        if (savingFoodToUpdate != null){
            savingFoodToUpdate.Id = savingFood.Id;
            savingFoodToUpdate.Name = savingFood.Name;
            savingFoodToUpdate.AwardedPoints = savingFood.AwardedPoints;
            savingFoodToUpdate.Date = savingFood.Date;
            savingFoodToUpdate.PackagesSaved = savingFood.PackagesSaved;
        await _context.SaveChangesAsync();
        }
        return await Task.FromResult(savingFoodToUpdate);
        
    }
    
}