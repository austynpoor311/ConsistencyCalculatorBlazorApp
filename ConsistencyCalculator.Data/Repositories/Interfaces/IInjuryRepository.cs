using ConsistencyCalculator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Data.Repositories.Interfaces
{
    public interface IInjuryRepository
    {
        IEnumerable<Injury> GetAllInjuries();
        Injury GetInjuryById(int injuryId);
        Injury AddInjury(Injury injury);
        Injury UpdateInjury(Injury injury);
        void DeleteInjury(int injuryId);
    }
}
