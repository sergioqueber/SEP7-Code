using Interfaces;
using Model;
using SQLitePCL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage;
using Microsoft.EntityFrameworkCore;

namespace Services;

    public class TeamService(ApplicationDbContext context) : ITeamService
    {
        private readonly ApplicationDbContext _context = context;

        static TeamService()
        {
        }

        public async Task<Team> CreateTeam(Team team)
        {
           _context.Teams.Add(team);
              await _context.SaveChangesAsync();
            return await Task.FromResult(team);
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await Task.FromResult( _context.Teams.AsEnumerable());
        }

        public async Task<Team?> GetTeamById(int id)
        {
            return await Task.FromResult(await _context.Teams.FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<Team> RemoveTeam(int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
           _context.Teams.Remove(team);
           await _context.SaveChangesAsync();
            return await Task.FromResult(team);
        }

        public async Task<Team?> UpdateTeam(Team team)
        {
            var teamToUpdate = await _context.Teams.FirstOrDefaultAsync(x => x.Id == team.Id);
            if (teamToUpdate != null)
            {
                teamToUpdate.Name = team.Name;
                teamToUpdate.Department = team.Department;
                teamToUpdate.TeamRewards = team.TeamRewards;
                teamToUpdate.TotalPoints = team.TotalPoints;
                _context.Teams.Update(teamToUpdate);
            }
            await _context.SaveChangesAsync();
            return await Task.FromResult(teamToUpdate);
        }
    }

