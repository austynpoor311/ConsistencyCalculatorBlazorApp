using System.ComponentModel.DataAnnotations;

namespace ConsistencyCalculator.Models.ViewModels
{
    public class PlayerStatisticsViewModel
    {
        public List<CategoryEvent> RecentGamesStats { get; set; }
        public List<string> Averages { get; set; }

        [Display(Name = "Player")]
        public int SelectedPlayerId { get; set; }
        //public IEnumerable<SelectListItem> Players { get; set; }

    }
}
