using ConsistencyCalculator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Data.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        IEnumerable<Team> GetAllTeams();
        Team GetTeamById(int teamId);
        Team AddTeam(Team team);
        Team UpdateTeam(Team team);
        void DeleteTeam(int teamId);
    }
}
