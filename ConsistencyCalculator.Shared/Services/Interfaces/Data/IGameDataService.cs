using ConsistencyCalculator.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Shared.Services.Interfaces.Data
{
    public interface IGameDataService
    {
        Task<IEnumerable<Game>> GetAllGames();
        Task<Game> GetGameById(int gameId);
        Task<Game> GetGameByRemoteId(string remoteId);
        Task<Game> AddGame(Game Game);
        Task UpdateGame(Game Game);
        Task DeleteGame(int gameId);
    }
}
