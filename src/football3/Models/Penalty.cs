using footballnet.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace footballnet.Models
{
    public class Penalty
    {
        public int Id { get; set; }

        public TimeSpan Time { get; set; }

        [JsonProperty("Laiks")]
        public string TimeRecord { get; set; }

        [JsonProperty("Nr")]
        public int PlayerNr { get; set; }
    }

    public class PenaltyRecord
    {
        public int Id { get; set; }

        [JsonProperty("Sods")]
        [JsonConverter(typeof(SingleOrArrayConverter<Penalty>))]
        public List<Penalty> Penalties { get; set; }
    }
}