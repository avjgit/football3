using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
                var teamGames = _context.Team.Where(t => t.Title == player.Team);
                var teamGoals = teamGames.SelectMany(a => a.GoalsRecord.Goals);

                player.Goals = teamGoals.Count(b => b.PlayerNr == player.Number);
                player.Passes = teamGoals.SelectMany(f => f.Passers).Count(p => p.Nr == player.Number);

                player.GamesPlayedInMainTeam = teamGames
                    .SelectMany(t => t.MainPlayersRecord.PlayersNrs)
                    .Count(n => n.Nr == player.Number);

                player.GamesPlayed = teamGames.Count(team => 
                    team.MainPlayersRecord.PlayersNrs.Any(n => n.Nr == player.Number) ||
                    team.ChangeRecord.Changes.Any(n => n.PlayerIn == player.Number)
                );

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

        public IActionResult Goalkeepers()
        {
            var goalkeepers = _context
                   .Player
                   .Where(p => p.Role == PlayerRole.Goalkeeper)
                   .GroupBy(p => new { p.Firstname, p.Lastname, p.Team })
                   .Select(p => p.First())
                   .ToList();

            foreach (var player in goalkeepers)
            {
                var teamsGames = _context.Game
                    .Where(game => game.Teams.Where(t => t.Title == player.Team)
                                   .SelectMany(t => t.MainPlayersRecord.PlayersNrs)
                                   .Any(n => n.Nr == player.Number) ||
                                   game.Teams.Where(t => t.Title == player.Team)
                                   .SelectMany(t => t.ChangeRecord.Changes)
                                   .Any(n => n.PlayerIn == player.Number))
                    ;

                // or this?
                //var teamsGames = _context.Game
                //    .Where(game => 
                //        game.Teams.Any(t => 
                //            (t.Title == player.Team) &&
                //            (
                //                t.MainPlayersRecord.PlayersNrs.Any(n => n.Nr == player.Number) ||
                //                t.ChangeRecord.Changes.Any(c => c.PlayerIn == player.Number)
                //            )
                //        )
                //    );

                player.GamesPlayed = teamsGames.Count();

                player.TotalGoalsMissed = teamsGames
                    .SelectMany(g => g.Teams.Where(t => t.Title != player.Team))
                    .SelectMany(t => t.GoalsRecord.Goals)
                    .Count();

                player.AvgGoalsMissed = (float)player.TotalGoalsMissed / player.GamesPlayed;
            }
            return View(goalkeepers);
        }
    }
}
