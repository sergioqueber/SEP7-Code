using Interfaces;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class RewardService : IRewardService
    {
        private static List<Reward> rewardList = new List<Reward>();

        static RewardService()
        {
        }

        public Task<Reward> CreateReward(Reward reward)
        {
            rewardList.Add(reward);
            return Task.FromResult(reward);
        }

        public Task<IEnumerable<Reward>> GetAllRewards()
        {
            return Task.FromResult(rewardList.AsEnumerable());
        }

        public Task<Reward> GetRewardById(int id)
        {
            return Task.FromResult(rewardList.FirstOrDefault(x => x.Id == id));
        }

        public Task<Reward> RemoveReward(int id)
        {
            var reward = rewardList.FirstOrDefault(x => x.Id == id);
            if (reward != null)
            {
                rewardList.Remove(reward);
            }
            return Task.FromResult(reward);
        }

        public Task<Reward> UpdateReward(Reward reward)
        {
            var rewardToUpdate = rewardList.FirstOrDefault(x => x.Id == reward.Id);
            if (rewardToUpdate != null)
            {
                rewardToUpdate.Name = reward.Name;
                rewardToUpdate.Description = reward.Description;
                rewardToUpdate.PointsRequired = reward.PointsRequired;
                rewardToUpdate.Availability = reward.Availability;
            }
            return Task.FromResult(rewardToUpdate);
        }
    }
}
