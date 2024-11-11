using Interfaces;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class TeamService : ITeamService
    {
        private static List<Team> teamList = new List<Team>();

        static TeamService()
        {
        }

        public Task<Team> CreateTeam(Team team)
        {
            teamList.Add(team);
            return Task.FromResult(team);
        }

        public Task<IEnumerable<Team>> GetAllTeams()
        {
            return Task.FromResult(teamList.AsEnumerable());
        }

        public Task<Team> GetTeamById(int id)
        {
            return Task.FromResult(teamList.FirstOrDefault(x => x.Id == id));
        }

        public Task<Team> RemoveTeam(int id)
        {
            var team = teamList.FirstOrDefault(x => x.Id == id);
            if (team != null)
            {
                teamList.Remove(team);
            }
            return Task.FromResult(team);
        }

        public Task<Team> UpdateTeam(Team team)
        {
            var teamToUpdate = teamList.FirstOrDefault(x => x.Id == team.Id);
            if (teamToUpdate != null)
            {
                teamToUpdate.Name = team.Name;
                teamToUpdate.Department = team.Department;
                teamToUpdate.TeamRewards = team.TeamRewards;
            }
            return Task.FromResult(teamToUpdate);
        }
    }
}
