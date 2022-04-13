using ConsistencyCalculator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Data.Repositories.Interfaces
{
    public interface IGamePlayerStatisticsRepository
    {
        IEnumerable<GamePlayerStatistics> GetAllGamePlayerStatistics();
        GamePlayerStatistics GetGamePlayerStatisticsById(int playerId, int gameId);
        List<GamePlayerStatistics> GetGamePlayerStatisticsByPlayerId(int playerId);
        List<GamePlayerStatistics> GetTopGamePlayerStatisticsByPlayerId(int playerId, int takeVal);
        List<GamePlayerStatistics> GetTopRecentGames(int takeVal);
        List<GamePlayerStatistics> GetGamePlayerStatisticsByPlayerAgainstTeam(int playerId, int opposingTeamId);
        GamePlayerStatistics GetGamePlayerStatisticsByGameId(int gameId);
        GamePlayerStatistics AddGamePlayerStatistics(GamePlayerStatistics gameplayerstatistics);
        GamePlayerStatistics UpdateGamePlayerStatistics(GamePlayerStatistics gameplayerstatistics);
        void DeleteGamePlayerStatistics(int playerId, int gameId);
    }
}
