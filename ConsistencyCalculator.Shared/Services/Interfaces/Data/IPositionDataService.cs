using ConsistencyCalculator.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Shared.Services.Interfaces.Data
{
    public interface IPositionDataService
    {
        Task<IEnumerable<Position>> GetAllPositions();
        Task<Position> GetPositionById(int positionId);
        Task<Position> AddPosition(Position position);
        Task UpdatePosition(Position position);
        Task DeletePosition(int positionId);
    }
}
