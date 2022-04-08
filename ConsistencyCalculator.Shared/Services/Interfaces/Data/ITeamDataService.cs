using ConsistencyCalculator.Models.Entities;

namespace ConsistencyCalculator.Shared.Services.Interfaces.Data
{
    public interface ITeamDataService
    {
        Task<IEnumerable<Team>> GetAllTeams();
        Task<Team> GetTeamById(int teamId);
        Task<Team> AddTeam(Team team);
        Task UpdateTeam(Team team);
        Task DeleteTeam(int teamId);
    }
}
