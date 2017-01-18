using football3.Models;
using System.ComponentModel.DataAnnotations;

namespace footballnet.Models
{
    public class Referee : Person
    {
        public int Games { get; set; }

        public int Penalties { get; set; }

        [Display(Name = "Average penalties per game")]
        [DisplayFormat(DataFormatString = "{0:0.0}")]
        public string AvgPenaltiesPerGame { get; set; }
    }
}