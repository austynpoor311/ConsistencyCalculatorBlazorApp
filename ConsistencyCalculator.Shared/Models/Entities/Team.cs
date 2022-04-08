using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Models.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string RemoteId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
