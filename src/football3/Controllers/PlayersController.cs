using System.Linq;
using Microsoft.AspNetCore.Mvc;
using football3.Data;
using footballnet.Models;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace football3.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        private List<Player> GetPlayers()
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

                var gamesPlayed = teamGames.Where(team =>
                    team.MainPlayersRecord.PlayersNrs.Any(n => n.Nr == player.Number) ||
                    team.ChangeRecord.Changes.Any(n => n.PlayerIn == player.Number)
                );

                player.GamesPlayed = gamesPlayed.Count();

                var teamGamesPenalties = teamGames.Select(t => t.PenaltiesRecord).Where(p => p != null);
                player.YellowCards = teamGamesPenalties.Count(p => p.Penalties.Count(x => x.PlayerNr == player.Number) == 1);
                player.RedCards = teamGamesPenalties.Count(p => p.Penalties.Count(x => x.PlayerNr == player.Number) == 2);

                player.MinutesPlayed = GetMinutesPlayed(player);
            }
            return players;
        }


        private int GetMinutesPlayed(Player player)
        {
            var gamesWithTeam = _context
            .Game
            .Select(g => g.Teams)
            .Where(g => g.Any(t => 
                t.Title == player.Team && 
                (
                    t.MainPlayersRecord.PlayersNrs.Any(p => p.Nr == player.Number) ||
                    t.ChangeRecord.Changes.Any(c => c.PlayerIn == player.Number)
                )
            ));

            var timePlayedInAllGames = new TimeSpan();
            foreach (var game in gamesWithTeam)
            {
                var teamRecord = game.Where(t => t.Title == player.Team).FirstOrDefault();
                var team = _context.Team.Where(t => t.Id == teamRecord.Id)
                    .Include(t => t.ChangeRecord.Changes)
                    .Include(t => t.MainPlayersRecord.PlayersNrs)
                    .Include(t => t.GoalsRecord.Goals)
                    .FirstOrDefault();

                var opponentTeamRecord = game.Where(t => t.Title != player.Team).FirstOrDefault();
                var opponentTeam = _context.Team.Where(t => t.Id == opponentTeamRecord.Id)
                    .Include(t => t.GoalsRecord.Goals)
                    .FirstOrDefault();

                // List of changes will be the main source of calculation - sum up changeIn-changeOut time intervals
                var changes = new List<Change>();
                if (team.ChangeRecord != null)
                    changes = team.ChangeRecord.Changes;

                var mainPlayers = team.MainPlayersRecord?.PlayersNrs;

                // Did player started from beginning? Then include it as first change - changeIn since 00:00
                if (mainPlayers.Any(p => p.Nr == player.Number))
                {
                    changes.Add(new Change
                    {
                        PlayerIn = player.Number,
                        Time = new TimeSpan()
                    });
                }

                var changesIn = changes.Where(c => c.PlayerIn == player.Number).OrderBy(c => c.Time).ToList();
                var changesOut = changes.Where(c => c.PlayerOut == player.Number).OrderBy(c => c.Time).ToList();

                // Did he played till and of the game? Then include it as last change - changeOut since endOfGame time
                if (changesIn.Count() > changesOut.Count())
                {
                    // what if game continued after 60 minutes?
                    //var lastGoal = game.SelectMany(t => t.GoalsRecord.Goals).Max(g => g.Time);
                    var lastTeamsGoalTime = team.GoalsRecord != null
                        ? team.GoalsRecord.Goals.Max(g => g.Time)
                        : new TimeSpan();
                    var lastOpponentGoalTime = opponentTeam.GoalsRecord != null
                        ? opponentTeam.GoalsRecord.Goals.Max(g => g.Time)
                        : new TimeSpan();
                    var lastGoal = lastTeamsGoalTime > lastOpponentGoalTime ? lastTeamsGoalTime : lastOpponentGoalTime;
                    var endOfGame = lastGoal.TotalMinutes > 60 ? lastGoal : new TimeSpan(0, 60, 0);

                    changesOut.Add(new Change
                    {
                        PlayerOut = player.Number,
                        Time = endOfGame
                    });
                }

                var timePlayedInGame = new TimeSpan();
                // Now just calculate changeIn-changeOut pairs
                for (int i = 0; i < changesIn.Count(); i++)
                {
                    var timePlayedBetweenChanges = changesOut[i].Time - changesIn[i].Time;
                    timePlayedInGame = timePlayedInGame.Add(timePlayedBetweenChanges);
                }

                timePlayedInAllGames = timePlayedInAllGames.Add(timePlayedInGame);
            }
            return (int) timePlayedInAllGames.TotalMinutes;
        }

        public IActionResult Index()
        {
            return View(GetPlayers().OrderBy(p => p.Team).ThenBy(p => p.Number));
        }

        private List<Player> SetPlaces(List<Player> players)
        {
            players.ForEach(r => r.PlaceInTop = players.FindIndex(x => x.Id == r.Id) + 1);
            return players;
        }

        public IActionResult TopPlayers()
        {
            var players = GetPlayers()
                .OrderByDescending(p => p.Goals)
                .ThenByDescending(p => p.Passes)
                .Take(10).ToList();

            return View(SetPlaces(players));
        }

        public IActionResult TopPenalized()
        {
            var players = GetPlayers()
                .Where(p => p.RedCards > 0 || p.YellowCards > 0)
                .OrderByDescending(p => p.RedCards)
                .ThenByDescending(p => p.YellowCards).ToList();

            return View(SetPlaces(players));
        }

        private IEnumerable<Player> GetGoalkeepers()
        {
            var goalkeepers = GetPlayers().Where(p => p.Role == PlayerRole.Goalkeeper);

            foreach (var goalkeeper in goalkeepers)
            {
                var teamsGames = _context.Game.Where(game => 
                    game.Teams.Where(t => t.Title == goalkeeper.Team)
                    .SelectMany(t => t.MainPlayersRecord.PlayersNrs)
                    .Any(n => n.Nr == goalkeeper.Number) 
                    ||
                    game.Teams.Where(t => t.Title == goalkeeper.Team)
                    .SelectMany(t => t.ChangeRecord.Changes)
                    .Any(n => n.PlayerIn == goalkeeper.Number));

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

                goalkeeper.GamesPlayed = teamsGames.Count();

                if (goalkeeper.GamesPlayed == 0)
                {
                    goalkeeper.AvgGoalsMissed = string.Empty;
                    continue;
                }

                goalkeeper.TotalGoalsMissed = teamsGames
                    .SelectMany(g => g.Teams.Where(t => t.Title != goalkeeper.Team))
                    .SelectMany(t => t.GoalsRecord.Goals)
                    .Count();

                var avg = (float) goalkeeper.TotalGoalsMissed / goalkeeper.GamesPlayed;
                goalkeeper.AvgGoalsMissed = avg.ToString("0.0");
            }
            return goalkeepers;
        }
        public IActionResult TopGoalkeepers()
        {
            var goalkeepers = GetGoalkeepers()
                .Where(g => !string.IsNullOrEmpty(g.AvgGoalsMissed))
                .OrderBy(g => g.AvgGoalsMissed)
                .Take(5).ToList();
            return View(SetPlaces(goalkeepers));
        }

        public IActionResult Goalkeepers()
        {
            return View(GetGoalkeepers());
        }
    }
}
