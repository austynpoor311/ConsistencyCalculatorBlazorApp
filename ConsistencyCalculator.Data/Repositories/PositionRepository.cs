using ConsistencyCalculator.Data.DbContexts;
using ConsistencyCalculator.Data.Repositories.Interfaces;
using ConsistencyCalculator.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ConsistencyCalculator.Data.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly AppDbContext _appDbContext;

        public PositionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Position> GetAllPositions()
        {
            return _appDbContext.Positions;
        }

        public Position GetPositionById(int positionId)
        {
            return _appDbContext.Positions.FirstOrDefault(p => p.Id == positionId);
        }

        public Position AddPosition(Position position)
        {
            var addedEntity = _appDbContext.Positions.Add(position);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public Position UpdatePosition(Position position)
        {
            var foundPosition = _appDbContext.Positions.FirstOrDefault(p => p.Id == position.Id);

            if (foundPosition != null)
            {
                foundPosition.Id = position.Id;
                foundPosition.RemoteId = position.RemoteId;
                foundPosition.Name = position.Name;
                foundPosition.Abbreviation = position.Abbreviation;

                _appDbContext.SaveChanges();

                return foundPosition;
            }

            return null;
        }

        public void DeletePosition(int positionId)
        {
            var foundPosition = _appDbContext.Positions.FirstOrDefault(p => p.Id == positionId);
            if (foundPosition == null) return;

            _appDbContext.Positions.Remove(foundPosition);
            _appDbContext.SaveChanges();
        }
    }
}
