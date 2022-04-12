using ConsistencyCalculator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Data.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetAllPlayers();
        Player GetPlayerById(int playerId);
        List<Player> GetPlayersByTeamId(int teamId);
        Player GetPlayerByRemoteId(string remoteId);
        Player AddPlayer(Player player);
        Player UpdatePlayer(Player player);
        void DeletePlayer(int playerId);
    }
}
