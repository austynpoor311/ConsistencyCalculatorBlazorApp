using ConsistencyCalculator.Data.DbContexts;
using ConsistencyCalculator.Data.Repositories.Interfaces;
using ConsistencyCalculator.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsistencyCalculator.Data.Repositories
{
    public class GamePlayerStatisticsRepository : IGamePlayerStatisticsRepository
    {
        private readonly AppDbContext _appDbContext;

        public GamePlayerStatisticsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<GamePlayerStatistics> GetAllGamePlayerStatistics()
        {
            return _appDbContext.GamePlayerStatistics
                .Include(gps => gps.Player)
                .Include(gps => gps.Game);
        }

        public GamePlayerStatistics GetGamePlayerStatisticsById(int playerId, int gameId)
        {
            return _appDbContext.GamePlayerStatistics
                .Include(gps => gps.Player)
                .Include(gps => gps.Game)
                .FirstOrDefault(gps => gps.Player.Id == playerId && gps.Game.Id == gameId);
        }
        public GamePlayerStatistics GetGamePlayerStatisticsByPlayerId(int playerId)
        {
            return _appDbContext.GamePlayerStatistics
                .Include(gps => gps.Player)
                .Include(gps => gps.Game)
                .FirstOrDefault(gps => gps.Player.Id == playerId);
        }
        public GamePlayerStatistics GetGamePlayerStatisticsByGameId(int gameId)
        {
            return _appDbContext.GamePlayerStatistics
                .Include(gps => gps.Player)
                .Include(gps => gps.Game)
                .FirstOrDefault(gps => gps.Game.Id == gameId);
        }

        public GamePlayerStatistics AddGamePlayerStatistics(GamePlayerStatistics gamePlayerStatistics)
        {
            var addedEntity = _appDbContext.GamePlayerStatistics.Add(gamePlayerStatistics);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public GamePlayerStatistics UpdateGamePlayerStatistics(GamePlayerStatistics gamePlayerStatistics)
        {
            var foundGamePlayerStatistics = _appDbContext.GamePlayerStatistics
                .Include(gps => gps.Player)
                .Include(gps => gps.Game)
                .FirstOrDefault(gps => gps.Player.Id == gamePlayerStatistics.Player.Id && gps.Game.Id == gamePlayerStatistics.Game.Id);

            if (foundGamePlayerStatistics != null)
            {
                foundGamePlayerStatistics.Game = gamePlayerStatistics.Game;
                foundGamePlayerStatistics.Player = gamePlayerStatistics.Player;
                foundGamePlayerStatistics.Assists = gamePlayerStatistics.Assists;
                foundGamePlayerStatistics.Minutes = gamePlayerStatistics.Minutes;
                foundGamePlayerStatistics.Rebounds = gamePlayerStatistics.Rebounds;
                foundGamePlayerStatistics.Blocks = gamePlayerStatistics.Blocks;
                foundGamePlayerStatistics.Steals = gamePlayerStatistics.Steals;
                foundGamePlayerStatistics.Points = gamePlayerStatistics.Points;
                foundGamePlayerStatistics.ThreePointersMade = gamePlayerStatistics.ThreePointersMade;
                foundGamePlayerStatistics.ThreePointAttempts = gamePlayerStatistics.ThreePointAttempts;
                foundGamePlayerStatistics.ThreePointPercentage = gamePlayerStatistics.ThreePointPercentage;
                foundGamePlayerStatistics.FreeThrowAttempts = gamePlayerStatistics.FreeThrowAttempts;
                foundGamePlayerStatistics.FreeThrowsMade = gamePlayerStatistics.FreeThrowsMade;
                foundGamePlayerStatistics.FreeThrowPercentage = gamePlayerStatistics.FreeThrowPercentage;
                foundGamePlayerStatistics.FieldGoalAttempts = gamePlayerStatistics.FieldGoalAttempts;
                foundGamePlayerStatistics.FieldGoalPercentage = gamePlayerStatistics.FieldGoalPercentage;
                foundGamePlayerStatistics.FieldGoalsMade = gamePlayerStatistics.FieldGoalsMade;
                foundGamePlayerStatistics.PlayerFouls = gamePlayerStatistics.PlayerFouls;
                foundGamePlayerStatistics.Turnovers = gamePlayerStatistics.Turnovers;

                _appDbContext.SaveChanges();

                return foundGamePlayerStatistics;
            }

            return null;
        }

        public void DeleteGamePlayerStatistics(int playerId, int gameId)
        {
            var foundGamePlayerStatistics = _appDbContext.GamePlayerStatistics
                .Include(gps => gps.Player)
                .Include(gps => gps.Game)
                .FirstOrDefault(gps => gps.Player.Id == playerId && gps.Game.Id == gameId);
            if (foundGamePlayerStatistics == null) return;

            _appDbContext.GamePlayerStatistics.Remove(foundGamePlayerStatistics);
            _appDbContext.SaveChanges();
        }
    }
}
