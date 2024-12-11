namespace Interfaces;
using Model;

public interface IRedeemedRewardService
{
    Task<RedeemedReward> CreateRedeemedReward(RedeemedReward redeemedReward);
    Task<IEnumerable<RedeemedReward>> GetAllRedeemedRewards();
    Task<RedeemedReward?> GetRedeemedRewardById(int id);
    Task<RedeemedReward?> RemoveRedeemedReward(int id);
    Task<RedeemedReward?> UpdateRedeemedReward(RedeemedReward redeemedReward);
    Task<IEnumerable<RedeemedReward?>> GetRedeemedRewardsByUserId(int userId);
}