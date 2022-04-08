using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Models.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string RemoteId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Age { get; set; }
        // TODO: Add date of birth

        public virtual ICollection<Injury> Injuries { get; set; } = new List<Injury>();
        public virtual Team Team { get; set; }
        public virtual Position Position { get; set; }
    }
}
