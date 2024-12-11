using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using SQLitePCL;
using Storage;

namespace Services;
public class RewardService(ApplicationDbContext context) : IRewardService{

    private readonly ApplicationDbContext _context = context;

    static RewardService(){
       
    }

    public async Task<Reward> CreateReward(Reward reward)
    {
        _context.Rewards.Add(reward);
        await _context.SaveChangesAsync();
        return await Task.FromResult(reward);
    }
    
    public async Task<IEnumerable<Reward>> GetAllRewards()
    {
        return await Task.FromResult(_context.Rewards.AsEnumerable());
    }

    public async Task<Reward?> GetRewardById(int id)
    {
        return await Task.FromResult( await _context.Rewards.FirstOrDefaultAsync(x => x.Id == id));
    }

    public async Task<Reward?> RemoveReward(int id)
    {
        var reward = await _context.Rewards.FirstOrDefaultAsync(x => x.Id == id);
        _context.Rewards.Remove(reward);
        await _context.SaveChangesAsync();
        return await Task.FromResult(reward);
    }


        public async Task<Reward?> UpdateReward(Reward reward)
        {
            var rewardToUpdate = await _context.Rewards.FirstOrDefaultAsync(x => x.Id == reward.Id);
            if (rewardToUpdate != null)
            {
                rewardToUpdate.Id = reward.Id;
                rewardToUpdate.Name = reward.Name;
                rewardToUpdate.Description = reward.Description;
                rewardToUpdate.PointsRequired = reward.PointsRequired;
                rewardToUpdate.Availability = reward.Availability;
                _context.Rewards.Update(rewardToUpdate);
                await _context.SaveChangesAsync();
            }
            return await Task.FromResult(rewardToUpdate);
        }
    }

