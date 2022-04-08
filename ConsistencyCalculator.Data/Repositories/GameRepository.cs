using ConsistencyCalculator.Data.DbContexts;
using ConsistencyCalculator.Data.Repositories.Interfaces;
using ConsistencyCalculator.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsistencyCalculator.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _appDbContext;

        public GameRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _appDbContext.Games;
        }

        public Game GetGameById(int gameId)
        {
            return _appDbContext.Games
                .FirstOrDefault(g => g.Id == gameId);
        }

        public Game GetGameByRemoteId(string remoteId)
        {
            return _appDbContext.Games
                .FirstOrDefault(g => g.RemoteId == remoteId);
        }

        public Game AddGame(Game game)
        {
            var addedEntity = _appDbContext.Games.Add(game);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public Game UpdateGame(Game game)
        {
            var foundGame = _appDbContext.Games
                .FirstOrDefault(g => g.Id == game.Id);

            if (foundGame != null)
            {
                foundGame.Id = game.Id;
                //TODO

                _appDbContext.SaveChanges();

                return foundGame;
            }

            return null;
        }

        public void DeleteGame(int gameId)
        {
            var foundGame = _appDbContext.Games
                .FirstOrDefault(g => g.Id == gameId);
            if (foundGame == null) return;

            _appDbContext.Games.Remove(foundGame);
            _appDbContext.SaveChanges();
        }
    }
}
