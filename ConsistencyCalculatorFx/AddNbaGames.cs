using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ConsistencyCalculatorFx
{
    public class AddNbaGames
    {
        private readonly HttpClient _httpClient;

        public AddNbaGames(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [FunctionName("AddNbaGames")]
        public async Task RunAsync([TimerTrigger("0 0 10 * * *")] TimerInfo myTimer, ILogger log)
        {
            await _httpClient.GetAsync($"api/game/nba/add");
        }
    }
}
