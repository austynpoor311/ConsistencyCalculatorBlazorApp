using ConsistencyCalculator.Data.DbContexts;
using ConsistencyCalculator.Data.Repositories.Interfaces;
using ConsistencyCalculator.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsistencyCalculator.Data.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _appDbContext;

        public TeamRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _appDbContext.Teams.Include(t => t.Players);
        }

        public Team GetTeamById(int teamId)
        {
            return _appDbContext.Teams.Include(t => t.Players).FirstOrDefault(t => t.Id == teamId);
        }

        public Team AddTeam(Team team)
        {
            var addedEntity = _appDbContext.Teams.Add(team);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public Team UpdateTeam(Team team)
        {
            var foundTeam = _appDbContext.Teams.Include(t => t.Players)
                .FirstOrDefault(t => t.Id == team.Id);

            if (foundTeam != null)
            {
                foundTeam.Id = team.Id;
                foundTeam.RemoteId = team.RemoteId;
                foundTeam.Name = team.Name;
                foundTeam.Abbreviation = team.Abbreviation;
                foundTeam.Players = team.Players;

                _appDbContext.SaveChanges();

                return foundTeam;
            }

            return null;
        }

        public void DeleteTeam(int teamId)
        {
            var foundTeam = _appDbContext.Teams
                .Include(t => t.Players)
                .FirstOrDefault(t => t.Id == teamId);
            if (foundTeam == null) return;

            _appDbContext.Teams.Remove(foundTeam);
            _appDbContext.SaveChanges();
        }
    }
}
