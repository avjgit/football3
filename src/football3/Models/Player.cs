﻿using football3.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace footballnet.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PlayerRole
    {
        [EnumMember(Value = "V")]
        Goalkeeper = 'V',

        [EnumMember(Value = "A")]
        Defender = 'A',

        [EnumMember(Value = "U")]
        Forward = 'U'
    }

    public class PlayerNr
    {
        public int Id { get; set; }
        public int Nr { get; set; }
    }

    public class Player : Person
    {
        [JsonProperty("Nr")]
        public int Number { get; set; }

        [JsonProperty("Loma")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlayerRole Role { get; set; }

        public string Team { get; set; }

        public int Goals { get; set; }
        public int Passes { get; set; }

        [Display(Name = "Games \nplayed")]
        public int GamesPlayed { get; set; }
        
        [Display(Name = "incl.\nin main team")]
        public int GamesPlayedInMainTeam { get; set; }

        [Display(Name = "Minutes played")]
        public int MinutesPlayed { get; set; }

        [Display(Name = "Yellow\ncards")]
        public int YellowCards { get; set; }

        [Display(Name = "Red\ncards")]
        public int RedCards { get; set; }

        [Display(Name = "Total goals missed")]
        public int TotalGoalsMissed { get; set; }

        [Display(Name = "Average goals missed")]
        [DisplayFormat(DataFormatString = "{0:0.0}")]
        public string AvgGoalsMissed { get; set; }

        public string NameAndTeam => $"{FullName}, {Team}";
    }

    public class PlayerRecord
    {
        public int Id { get; set; }

        [JsonProperty("Speletajs")]
        public List<Player> Players { get; set; }
    }

    public class PlayerNrRecord
    {
        public int Id { get; set; }

        [JsonProperty("Speletajs")]
        public List<PlayerNr> PlayersNrs { get; set; }
    }
}