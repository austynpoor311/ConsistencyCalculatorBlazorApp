using ConsistencyCalculator.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Shared.Services.Interfaces.Data
{
    public interface IInjuryDataService
    {
        Task<IEnumerable<Injury>> GetAllInjuries();
        Task<Injury> GetInjuryById(int injuryId);
        Task<Injury> AddInjury(Injury injury);
        Task UpdateInjury(Injury injury);
        Task DeleteInjury(int injuryId);
    }
}
