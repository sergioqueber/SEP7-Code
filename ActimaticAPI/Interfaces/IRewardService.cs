using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Interfaces
{
    public interface IRewardService
    {
        Task<Reward> CreateReward(Reward reward);
        Task<IEnumerable<Reward>> GetAllRewards();
        Task<Reward?> GetRewardById(int id);
        Task<Reward?> RemoveReward(int id);
        Task<Reward?> UpdateReward(Reward reward);
    }
}
