using Newtonsoft.Json;

namespace football3.Models
{
    public class Person
    {
        public int Id { get; set; }

        [JsonProperty("Vards")]
        public string Firstname { get; set; }

        [JsonProperty("Uzvards")]
        public string Lastname { get; set; }

        public string FullName => $"{Firstname} {Lastname}";
    }
}
