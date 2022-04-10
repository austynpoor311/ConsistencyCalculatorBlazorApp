using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ConsistencyCalculatorFx
{
    public class RefreshNbaPlayerInjuries
    {
        private readonly HttpClient _httpClient;

        public RefreshNbaPlayerInjuries(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [FunctionName("RefreshNbaPlayerInjuries")]
        public async Task RunAsync([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            await _httpClient.GetAsync($"api/injury/nba/refresh");
        }
    }
}
