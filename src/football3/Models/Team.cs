using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace footballnet.Models
{
    public class Team
    {
        public int Id { get; set; }

        [JsonProperty("Nosaukums")]
        public string Title { get; set; }

        [JsonProperty("Speletaji")]
        public PlayerRecord AllPLayersRecord { get; set; }

        [JsonProperty("Pamatsastavs")]
        public PlayerNrRecord MainPlayersRecord { get; set; }

        [JsonProperty("Sodi")]
        public PenaltyRecord PenaltiesRecord { get; set; }

        [JsonProperty("Varti")]
        public GoalRecord GoalsRecord { get; set; }

        [JsonProperty("Mainas")]
        public ChangeRecord ChangeRecord { get; set; }

        [Display(Name = "Goals \nwon")]
        public int GoalsWon { get; set; }

        [Display(Name = "Goals \nlost")]
        public int GoalsLost { get; set; }

        [Display(Name="Won \nat main time")]
        public int WinsDuringMainTime { get; set; }

        [Display(Name="Lost \nat main time")]
        public int LossesDuringMainTime { get; set; }

        [Display(Name = "Won \nat added time")]
        public int WinsDuringAddedTime { get; set; }

        [Display(Name = "Lost \nat added time")]
        public int LossesDuringAddedTime { get; set; }

        public int Points { get; set; }

        public int Place { get; set; }

        [Display(Name = "incl.\nby penalty")]
        public int PenaltyGoals { get; set; }

        public int Defendors { get; set; }

        public int Forwards { get; set; }

        public int Goalkeepers { get; set; }
    }
}