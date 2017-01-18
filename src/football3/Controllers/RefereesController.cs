using System.Linq;
using Microsoft.AspNetCore.Mvc;
using football3.Data;

namespace football3.Controllers
{
    public class RefereesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RefereesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        public IActionResult Index()
        {
            var referees = _context.Referee
                .GroupBy(r => new { r.Firstname, r.Lastname })
                .Select(r => r.First())
                .ToList();

            foreach (var referee in referees)
            {
                 var gamesAsMainOrLineReferee = _context.Game.Where(g => 
                        g.MainReferee.FullName == referee.FullName ||
                        g.LineReferees.Any(l => l.FullName == referee.FullName));

                referee.Games = gamesAsMainOrLineReferee.Count();

                if (referee.Games == 0)
                {
                    referee.AvgPenaltiesPerGame = string.Empty;
                    continue;
                }

                // assuming penalties issued is counted for all referees
                referee.Penalties = gamesAsMainOrLineReferee
                    .SelectMany(x => x.Teams)
                    .SelectMany(y => y.PenaltiesRecord.Penalties)
                    .Count();

                var avg = (float)referee.Penalties / referee.Games;
                referee.AvgPenaltiesPerGame = avg.ToString("0.0");
            }

            referees = referees.OrderByDescending(r => r.AvgPenaltiesPerGame).ToList();
            referees.ForEach(r => r.PlaceInTop = referees.FindIndex(x => x.Id == r.Id) + 1);
            return View(referees);
        }
    }
}