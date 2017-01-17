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
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        public IActionResult Index()
        {
            var players = _context
                   .Player
                   .GroupBy(p => new { p.Firstname, p.Lastname, p.Team })
                   .Select(p => p.First())
                   .ToList();

            foreach (var player in players)
            {
                player.Goals = _context
                    .Game
                    .SelectMany(g => g.Teams.Where(t => t.Title == player.Team))
                    .SelectMany(a => a.GoalsRecord.Goals)
                    .Count(b => b.PlayerNr == player.Number);

                player.Passes = _context
                    .Game
                    .SelectMany(c => c.Teams.Where(d => d.Title == player.Team))
                    .SelectMany(e => e.GoalsRecord.Goals)
                    .SelectMany(f => f.Passers)
                    .Count(p => p.Nr == player.Number);



                player.GamesPlayedInMainTeam = _context.Game
                    .Count(game => game.Teams.Where(t => t.Title == player.Team)
                                   .SelectMany(t => t.MainPlayersRecord.PlayersNrs)
                                   .Any(n => n.Nr == player.Number));

                player.GamesPlayed = _context.Game
                    .Count(game => game.Teams.Where(t => t.Title == player.Team)
                                   .SelectMany(t => t.MainPlayersRecord.PlayersNrs)
                                   .Any(n => n.Nr == player.Number) ||
                                   game.Teams.Where(t => t.Title == player.Team)
                                   .SelectMany(t => t.ChangeRecord.Changes)
                                   .Any(n => n.PlayerIn == player.Number));

                var teamGames = _context.Team.Where(t => t.Title == player.Team);
                var teamGamesPenalties = teamGames.Select(t => t.PenaltiesRecord).Where(p => p != null);
                player.YellowCards = teamGamesPenalties.Count(p => p.Penalties.Count(x => x.PlayerNr == player.Number) == 1);
                player.RedCards = teamGamesPenalties.Count(p => p.Penalties.Count(x => x.PlayerNr == player.Number) == 2);

                //public int MinutesPlayed { get; set; }
            }

            return View(players
                    .OrderByDescending(p => p.Goals)
                    .ThenByDescending(z => z.Passes)
            );
        }
    }
}
