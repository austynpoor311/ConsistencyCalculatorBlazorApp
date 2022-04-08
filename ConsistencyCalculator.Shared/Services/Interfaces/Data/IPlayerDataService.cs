using ConsistencyCalculator.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Shared.Services.Interfaces.Data
{
    public interface IPlayerDataService
    {
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<Player> GetPlayerById(int playerId);
        Task<Player> AddPlayer(Player player);
        Task UpdatePlayer(Player player);
        Task DeletePlayer(int playerId);
    }
}
