using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Models.Entities
{
    public class Injury
    {
        public int Id { get; set; }
        public string RemoteId { get; set; }
        public string? LongComment { get; set; }
        public string? ShortComment { get; set; }
        public string? Status { get; set; }
        public DateTime? DateString { get; set; }
        public string? Type { get; set; }
        public string? Location { get; set; }
        public string? Detail { get; set; }
        public string? Side { get; set; }
        public virtual Player Player { get; set; }
    }
}
