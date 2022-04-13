using ConsistencyCalculator.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Shared.Services.Interfaces.Data
{
    public interface IGamePlayerStatisticsDataService
    {
        Task<IEnumerable<GamePlayerStatistics>> GetAllGamePlayerStatistics();
        Task<List<GamePlayerStatistics>> GetGamePlayerStatisticsByPlayerId(int playerId);
        Task<List<GamePlayerStatistics>> GetTopGamePlayerStatisticsByPlayerId(int playerId, int takeVal);
        Task<List<GamePlayerStatistics>> GetGamePlayerStatisticsByPlayerAgainstTeam(int playerId, int opposingTeamId);
        Task<GamePlayerStatistics> GetGamePlayerStatisticsByGameId(int gameId);
        Task<GamePlayerStatistics> GetGamePlayerStatisticsById(int playerId, int gameId);
        Task<GamePlayerStatistics> AddGamePlayerStatistics(GamePlayerStatistics gamePlayerStatistics);
        Task UpdateGamePlayerStatistics(GamePlayerStatistics gamePlayerStatistics);
        Task DeleteGamePlayerStatistics(int playerId, int gameId);
    }
}
