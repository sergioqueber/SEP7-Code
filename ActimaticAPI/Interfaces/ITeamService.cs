using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Interfaces
{
    public interface ITeamService
    {
        Task<Team> CreateTeam(Team team);
        Task<IEnumerable<Team>> GetAllTeams();
        Task<Team> GetTeamById(int id);
        Task<Team> RemoveTeam(int id);
        Task<Team> UpdateTeam(Team team);
    }
}
