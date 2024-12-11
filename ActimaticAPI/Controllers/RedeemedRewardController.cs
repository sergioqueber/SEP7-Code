using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RedeemedRewardController : ControllerBase
    {
        private readonly IRedeemedRewardService _redeemedRewardService;

        public RedeemedRewardController(IRedeemedRewardService redeemedRewardService)
        {
            _redeemedRewardService = redeemedRewardService;
        }

        [HttpPost]
        public async Task<ActionResult<RedeemedReward>> CreateRedeemedReward([FromBody] RedeemedReward redeemedReward)
        {
            var createdRedeemedReward = await _redeemedRewardService.CreateRedeemedReward(redeemedReward);
            return CreatedAtAction(nameof(GetRedeemedRewardById), new { id = createdRedeemedReward.Id }, createdRedeemedReward);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RedeemedReward>>> GetAllRedeemedRewards()
        {
            var redeemedRewards = await _redeemedRewardService.GetAllRedeemedRewards();
            return Ok(redeemedRewards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RedeemedReward>> GetRedeemedRewardById(int id)
        {
            var redeemedReward = await _redeemedRewardService.GetRedeemedRewardById(id);
            if (redeemedReward == null)
            {
                return NotFound();
            }
            return Ok(redeemedReward);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<RedeemedReward>>> GetRedeemedRewardsByUserId(int userId)
        {
            var redeemedRewards = await _redeemedRewardService.GetRedeemedRewardsByUserId(userId);
            return Ok(redeemedRewards);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RedeemedReward>> RemoveRedeemedReward(int id)
        {
            var removedRedeemedReward = await _redeemedRewardService.RemoveRedeemedReward(id);
            if (removedRedeemedReward == null)
            {
                return NotFound();
            }
            return Ok(removedRedeemedReward);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RedeemedReward>> UpdateRedeemedReward(int id, [FromBody] RedeemedReward redeemedReward)
        {
            if (id != redeemedReward.Id)
            {
                return BadRequest("RedeemedReward ID mismatch");
            }

            var updatedRedeemedReward = await _redeemedRewardService.UpdateRedeemedReward(redeemedReward);
            if (updatedRedeemedReward == null)
            {
                return NotFound();
            }
            return Ok(updatedRedeemedReward);
        }
    }
}
