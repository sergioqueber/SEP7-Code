using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using SQLitePCL;
using Storage;

namespace Services;
public class RedeemedRewardService(ApplicationDbContext context) : IRedeemedRewardService{

    private readonly ApplicationDbContext _context = context;

    static RedeemedRewardService(){
       
    }

    public async Task<RedeemedReward> CreateRedeemedReward(RedeemedReward redeemedReward)
    {
        _context.RedeemedRewards.Add(redeemedReward);
        await _context.SaveChangesAsync();
        return await Task.FromResult(redeemedReward);
    }
    
    public async Task<IEnumerable<RedeemedReward>> GetAllRedeemedRewards()
    {
        return await Task.FromResult(_context.RedeemedRewards.AsEnumerable());
    }

    public async Task<RedeemedReward?> GetRedeemedRewardById(int id)
    {
        return await Task.FromResult( await _context.RedeemedRewards.FirstOrDefaultAsync(x => x.Id == id));
    }

    public async Task<RedeemedReward?> RemoveRedeemedReward(int id)
    {
        var redeemedReward = await _context.RedeemedRewards.FirstOrDefaultAsync(x => x.Id == id);
        _context.RedeemedRewards.Remove(redeemedReward);
        await _context.SaveChangesAsync();
        return await Task.FromResult(redeemedReward);
    }


        public async Task<RedeemedReward?> UpdateRedeemedReward(RedeemedReward redeemedReward)
        {
            var redeemedRewardToUpdate = await _context.RedeemedRewards.FirstOrDefaultAsync(x => x.Id == redeemedReward.Id);
            if (redeemedRewardToUpdate != null)
            {
                redeemedRewardToUpdate.Id = redeemedReward.Id;
                await _context.SaveChangesAsync();
            }
            return await Task.FromResult(redeemedRewardToUpdate);
        }

        public async Task<IEnumerable<RedeemedReward?>> GetRedeemedRewardsByUserId(int userId)
        {
            return await Task.FromResult(_context.RedeemedRewards.Where(x => x.UserId == userId).AsEnumerable());
        }
    }

