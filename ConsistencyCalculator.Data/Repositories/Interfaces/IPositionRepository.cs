using ConsistencyCalculator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Data.Repositories.Interfaces
{
    public interface IPositionRepository
    {
        IEnumerable<Position> GetAllPositions();
        Position GetPositionById(int positionId);
        Position AddPosition(Position position);
        Position UpdatePosition(Position position);
        void DeletePosition(int positionId);
    }
}
