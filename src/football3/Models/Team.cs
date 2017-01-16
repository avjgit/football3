using Newtonsoft.Json;
using System;

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

        public int GoalsWon { get; set; }
        public int GoalsLost { get; set; }
        public int WinsDuringMainTime { get; set; }
        public int LossesDuringMainTime { get; set; }
        public int WinsDuringAddedTime { get; set; }
        public int LossesDuringAddedTime { get; set; }
        public int Points { get; set; }
        public int Place { get; set; }
        public int PenaltyGoals { get; set; }
        public int Defendors { get; set; }
        public int Forwards { get; set; }
        public int Goalkeepers { get; set; }
        public TimeSpan TotalTimePlayed { get; set; }
        public TimeSpan AverageTimePlayed { get; set; }
    }
}