using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Models.Entities
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string RemoteId { get; set; }

        public DateTime GameDate { get; set; }

        public string Score { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public string HomeTeamScore { get; set; }

        public string AwayTeamScore { get; set; }

        public string GameResult { get; set; }

        public string? LeagueName { get; set; }

        public string? LeagueAbbreviation { get; set; }

        public string? LeagueShortName { get; set; }

        public virtual Team AwayTeam { get; set; }

        public virtual Team HomeTeam { get; set; }

        public virtual ICollection<GamePlayerStatistics> GamePlayerStatistics { get; set; } = new List<GamePlayerStatistics>();


    }
}
