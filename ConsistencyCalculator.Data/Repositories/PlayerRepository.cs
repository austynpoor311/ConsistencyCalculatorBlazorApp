using ConsistencyCalculator.Data.DbContexts;
using ConsistencyCalculator.Data.Repositories.Interfaces;
using ConsistencyCalculator.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsistencyCalculator.Data.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _appDbContext;

        public PlayerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _appDbContext.Players;
        }

        public Player GetPlayerById(int playerId)
        {
            return _appDbContext.Players
                .Include(p => p.Injuries)
                .Include(p => p.Team)
                .Include(p => p.Position)
                .FirstOrDefault(p => p.Id == playerId);
        }

        public Player GetPlayerByRemoteId(string remoteId)
        {
            return _appDbContext.Players
                .Include(p => p.Injuries)
                .Include(p => p.Team)
                .Include(p => p.Position)
                .FirstOrDefault(p => p.RemoteId == remoteId);
        }

        public Player AddPlayer(Player player)
        {
            var addedEntity = _appDbContext.Players.Add(player);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public Player UpdatePlayer(Player player)
        {
            var foundPlayer = _appDbContext.Players
                .Include(p => p.Injuries)
                .Include(p => p.Team)
                .Include(p => p.Position)
                .FirstOrDefault(p => p.Id == player.Id);

            if (foundPlayer != null)
            {
                foundPlayer.Id = player.Id;      
                foundPlayer.RemoteId = player.RemoteId;
                foundPlayer.FirstName = player.FirstName;
                foundPlayer.LastName = player.LastName;
                foundPlayer.Age = player.Age;
                foundPlayer.Team = player.Team;
                foundPlayer.Position = player.Position;
                foundPlayer.Injuries = player.Injuries;
                foundPlayer.Height = player.Height;
                foundPlayer.Weight = player.Weight;

                _appDbContext.SaveChanges();

                return foundPlayer;
            }

            return null;
        }

        public void DeletePlayer(int playerId)
        {
            var foundPlayer = _appDbContext.Players
                .Include(p => p.Injuries)
                .Include(p => p.Team)
                .Include(p => p.Position)
                .FirstOrDefault(p => p.Id == playerId);
            if (foundPlayer == null) return;

            _appDbContext.Players.Remove(foundPlayer);
            _appDbContext.SaveChanges();
        }
    }
}
