using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam([FromBody] Team team)
        {
            var createdTeam = await _teamService.CreateTeam(team);
            return CreatedAtAction(nameof(GetTeamById), new { id = createdTeam.Id }, createdTeam);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeams();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeamById(int id)
        {
            var team = await _teamService.GetTeamById(id);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Team>> RemoveTeam(int id)
        {
            var removedTeam = await _teamService.RemoveTeam(id);
            if (removedTeam == null)
            {
                return NotFound();
            }
            return Ok(removedTeam);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Team>> UpdateTeam(int id, [FromBody] Team team)
        {
            if (id != team.Id)
            {
                return BadRequest("Team ID mismatch");
            }

            var updatedTeam = await _teamService.UpdateTeam(team);
            if (updatedTeam == null)
            {
                return NotFound();
            }
            return Ok(updatedTeam);
        }
    }
}
