using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ConsistencyCalculatorFx
{
    public class RefreshNbaGamePlayerStatistics
    {
        private readonly HttpClient _httpClient;

        public RefreshNbaGamePlayerStatistics(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [FunctionName("RefreshNbaGamePlayerStatistics")]
        public async Task RunAsync([TimerTrigger("0 0 11 * * *")] TimerInfo myTimer, ILogger log)
        {
            await _httpClient.GetAsync($"api/gameplayerstatistics/nba/addorupdate");
        }
    }
}
