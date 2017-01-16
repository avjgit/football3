using footballnet.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace footballnet.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GoalType
    {
        [EnumMember(Value = "N")]
        Game = 'N',

        [EnumMember(Value = "J")]
        Penalty = 'J'
    }

    public class Goal
    {
        public int Id { get; set; }

        public TimeSpan Time { get; set; }

        [JsonProperty("Sitiens")]
        public GoalType GoalType { get; set; }

        [JsonProperty("Laiks")]
        public string TimeRecord { get; set; }

        [JsonProperty("Nr")]
        public int PlayerNr { get; set; }

        [JsonProperty("P")]
        [JsonConverter(typeof(SingleOrArrayConverter<PlayerNr>))]
        public List<PlayerNr> Passers { get; set; }
    }

    public class GoalRecord
    {
        public int Id { get; set; }

        [JsonProperty("VG")]
        [JsonConverter(typeof(SingleOrArrayConverter<Goal>))]
        public List<Goal> Goals { get; set; }
    }
}