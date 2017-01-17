using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using football3.Data;
using footballnet.Models;

namespace football3.Controllers
{
    public class RefereesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RefereesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Referees
        public IActionResult Index()
        {
            var referees = _context.Referee
                .GroupBy(r => new { r.Firstname, r.Lastname })
                .Select(r => r.First())
                .ToList();

            foreach (var referee in referees)
            {
                // assuming penalties issued is counted for all referees

                 var gamesAsMainOrLineReferee = _context.Game.Where(g => 
                        g.MainReferee.FullName == referee.FullName ||
                        g.LineReferees.Any(l => l.FullName == referee.FullName));

                referee.Games = gamesAsMainOrLineReferee.Count();

                if (referee.Games == 0)
                    continue;

                referee.Penalties = gamesAsMainOrLineReferee
                    .SelectMany(x => x.Teams)
                    .SelectMany(y => y.PenaltiesRecord.Penalties)
                    .Count();

                referee.AvgPenaltiesPerGame = referee.Penalties / referee.Games;
            }

            return View(referees);
        }
    }
}
