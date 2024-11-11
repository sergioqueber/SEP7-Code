using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RewardController : ControllerBase
    {
        private readonly IRewardService _rewardService;

        public RewardController(IRewardService rewardService)
        {
            _rewardService = rewardService;
        }

        [HttpPost]
        public async Task<ActionResult<Reward>> CreateReward([FromBody] Reward reward)
        {
            var createdReward = await _rewardService.CreateReward(reward);
            return CreatedAtAction(nameof(GetRewardById), new { id = createdReward.Id }, createdReward);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reward>>> GetAllRewards()
        {
            var rewards = await _rewardService.GetAllRewards();
            return Ok(rewards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reward>> GetRewardById(int id)
        {
            var reward = await _rewardService.GetRewardById(id);
            if (reward == null)
            {
                return NotFound();
            }
            return Ok(reward);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Reward>> RemoveReward(int id)
        {
            var removedReward = await _rewardService.RemoveReward(id);
            if (removedReward == null)
            {
                return NotFound();
            }
            return Ok(removedReward);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Reward>> UpdateReward(int id, [FromBody] Reward reward)
        {
            if (id != reward.Id)
            {
                return BadRequest("Reward ID mismatch");
            }

            var updatedReward = await _rewardService.UpdateReward(reward);
            if (updatedReward == null)
            {
                return NotFound();
            }
            return Ok(updatedReward);
        }
    }
}
