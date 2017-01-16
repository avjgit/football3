using Newtonsoft.Json;

namespace footballnet.Models
{
    public class Referee
    {
        public int Id { get; set; }

        [JsonProperty("Vards")]
        public string Firstname { get; set; }

        [JsonProperty("Uzvards")]
        public string Lastname { get; set; }

        public int Games { get; set; }

        public int Penalties { get; set; }

        public float AvgPenaltiesPerGame { get; set; }
    }
}