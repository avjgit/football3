using football3.Models;

namespace footballnet.Models
{
    public class Referee : Person
    {
        public int Games { get; set; }

        public int Penalties { get; set; }

        public float AvgPenaltiesPerGame { get; set; }
    }
}