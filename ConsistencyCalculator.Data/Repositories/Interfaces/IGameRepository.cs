using ConsistencyCalculator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Data.Repositories.Interfaces
{
    public interface IGameRepository
    {
        IEnumerable<Game> GetAllGames();
        Game GetGameById(int gameId);
        Game GetGameByRemoteId(string remoteId);
        Game AddGame(Game game);
        Game UpdateGame(Game game);
        void DeleteGame(int gameId);
    }
}
