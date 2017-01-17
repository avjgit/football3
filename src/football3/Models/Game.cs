using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace footballnet.Models
{
    public class GameRecord
    {
        public int Id { get; set; }

        [JsonProperty("Spele")]
        public Game Game { get; set; }
    }

    public class Game
    {
        public int Id { get; set; }

        [JsonProperty("Laiks")]
        public DateTime Date { get; set; }

        [JsonProperty("Vieta")]
        public string Place { get; set; }

        [JsonProperty("Skatitaji")]
        public int Spectators { get; set; }

        [JsonProperty("T")]
        public List<Referee> LineReferees { get; set; }

        [JsonProperty("VT")]
        public Referee MainReferee { get; set; }

        [JsonProperty("Komanda")]
        public List<Team> Teams { get; set; }

        //public IEnumerable<Goal> GoalsOfTeam(string teamTitle)
        //    => Teams.Where(t => t.Title == teamTitle).SelectMany(t => t.GoalsRecord.Goals);
    }
}