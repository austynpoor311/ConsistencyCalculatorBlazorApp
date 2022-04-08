
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConsistencyCalculator.Models
{
    public class Item
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }
    }

    public class PlayersByTeamResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("pageIndex")]
        public int PageIndex { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }
    }
}
