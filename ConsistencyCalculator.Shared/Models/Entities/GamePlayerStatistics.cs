using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsistencyCalculator.Models.Entities
{
    public class GamePlayerStatistics
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public int Minutes { get; set; }
        public int FieldGoalAttempts { get; set; }
        public int FieldGoalsMade { get; set; }
        public double FieldGoalPercentage { get; set; }
        public int ThreePointAttempts { get; set; }
        public int ThreePointersMade { get; set; }
        public double ThreePointPercentage { get; set; }
        public int FreeThrowAttempts { get; set; }
        public int FreeThrowsMade { get; set; }
        public double FreeThrowPercentage { get; set; }
        public int Rebounds { get; set; }
        public int Assists { get; set; }
        public int Blocks { get; set; }
        public int Steals { get; set; }
        public int PlayerFouls { get; set; }
        public int Turnovers { get; set; }
        public int Points { get; set; }
        public virtual Game Game { get; set; }
        public virtual Player Player { get; set; }
    }
}
