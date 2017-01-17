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

        // GET: Players
        public async Task<IActionResult> Index()
        {

            //foreach (var player in _context.Player)
            //{
            //    player.Goals = _context
            //        .Game
            //        .SelectMany(g => g.Teams.Where(t => t.Title == player.Team))
            //        .SelectMany(a => a.GoalsRecord.Goals)
            //        .Where(b => b.PlayerNr == player.Number)
            //        .Count();

            //    player.Passes = _context
            //        .Game
            //        .SelectMany(c => c.Teams.Where(d => d.Title == player.Team))
            //        .SelectMany(e => e.GoalsRecord.Goals)
            //        .SelectMany(f => f.Passers)
            //        .Where(h => h.Nr == player.Number)
            //        .Count();

            //    var playersTeamGames = _context.Game
            //        .Where(g => g.Teams.Any(t => t.Title == player.Team));


        //public int GamesPlayedInMainTeam { get; set; }
        //public int MinutesPlayed { get; set; }


            //    player.GamesPlayedInMainTeam = _context.Game
            //        .Where(game => game.Teams.Where(t => t.Title == player.Team)
            //                       .SelectMany(t => t.MainPlayersRecord.PlayersNrs)
            //                       .Any(n => n.Nr == player.Number) ||
            //                       game.Teams.Where(t => t.Title == player.Team)
            //                       .SelectMany(t => t.ChangeRecord.Changes)
            //                       .Any(n => n.PlayerIn == player.Number)).Count()
            //        ;

            //    player.GamesPlayed = _context.Game
            //        .Where(game => game.Teams.Where(t => t.Title == player.Team)
            //                       .SelectMany(t => t.MainPlayersRecord.PlayersNrs)
            //                       .Any(n => n.Nr == player.Number) ||
            //                       game.Teams.Where(t => t.Title == player.Team)
            //                       .SelectMany(t => t.ChangeRecord.Changes)
            //                       .Any(n => n.PlayerIn == player.Number)).Count()
            //        ;

            //    var teamGames = playersTeamGames.SelectMany(g => g.Teams.Where(t => t.Title == player.Team));
            //    var penalties = teamGames.Select(t => t.PenaltiesRecord);
            //    player.YellowCards = penalties.Count(p => p.Penalties.Count(x => x.PlayerNr == player.Number) == 1);

            //    //.Where(g => g.PenaltiesRecord.Penalties.Count(p => p.PlayerNr == player.Number) == 1)
            //    //.Count();

            //    //player.RedCards = playersTeamGames
            //    //    .Count(g => g.PenaltiesRecord.Penalties.Where(p => p.PlayerNr == player.Number).Count() == 2);
            //}
            //return View(await _context
            //    .Player
            //    .OrderByDescending(p => p.Goals)
            //    .ThenByDescending(z => z.Passes).ToListAsync());
            return View(await _context.Player.ToListAsync());
        }
    }
}
