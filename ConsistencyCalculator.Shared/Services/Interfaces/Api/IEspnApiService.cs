using ConsistencyCalculator.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Shared.Services.Interfaces.Api
{
    public interface IEspnApiService
    { 
        Task AddOrUpdateNbaPlayersAsync();
    }
}
