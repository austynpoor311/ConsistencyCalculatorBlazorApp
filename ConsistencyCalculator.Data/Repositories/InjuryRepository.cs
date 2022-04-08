using ConsistencyCalculator.Data.DbContexts;
using ConsistencyCalculator.Data.Repositories.Interfaces;
using ConsistencyCalculator.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsistencyCalculator.Data.Repositories
{
    public class InjuryRepository : IInjuryRepository
    {
        private readonly AppDbContext _appDbContext;

        public InjuryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Injury> GetAllInjuries()
        {
            return _appDbContext.Injuries.Include(i => i.Player);
        }

        public Injury GetInjuryById(int injuryId)
        {
            return _appDbContext.Injuries
                .Include(i => i.Player)
                .FirstOrDefault(i => i.Id == injuryId);
        }

        public Injury AddInjury(Injury injury)
        {
            var addedEntity = _appDbContext.Injuries.Add(injury);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public Injury UpdateInjury(Injury injury)
        {
            var foundInjury = _appDbContext.Injuries
                .Include(i => i.Player)
                .FirstOrDefault(i => i.Id == injury.Id);

            if (foundInjury != null)
            {
                foundInjury.Id = injury.Id;
                foundInjury.RemoteId = injury.RemoteId;
                foundInjury.Status = injury.Status;
                foundInjury.ShortComment = injury.ShortComment;
                foundInjury.LongComment = injury.LongComment;
                foundInjury.Location = injury.Location;
                foundInjury.Side = injury.Side;
                foundInjury.DateString = injury.DateString;
                foundInjury.Detail = injury.Detail;
                foundInjury.Type = injury.Type;
                foundInjury.Player = injury.Player;

                _appDbContext.SaveChanges();

                return foundInjury;
            }

            return null;
        }

        public void DeleteInjury(int injuryId)
        {
            var foundInjury = _appDbContext.Injuries
                .Include(i => i.Player)
                .FirstOrDefault(i => i.Id == injuryId);
            if (foundInjury == null) return;

            _appDbContext.Injuries.Remove(foundInjury);
            _appDbContext.SaveChanges();
        }
    }
}
