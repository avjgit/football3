using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using football3.Data;
using footballnet.Models;

namespace football3.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        const int mainTime = 60;
        const int winDuringMainTimePoints = 5;
        const int winDuringAddedTimePoints = 3;
        const int lossDuringAddedTimePoints = 2;
        const int lossDuringMainTimePoints = 1;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Game> GamesTeamParticipated(string teamTitle)
          => _context.Game.Where(g => g.Teams.Any(t => t.Title == teamTitle));
        
        public IQueryable<Team> TeamGames(IQueryable<Game> games, string teamTitle, bool isOfGivenTeam = true)
            => games.SelectMany(g => g.Teams.Where(t => (t.Title == teamTitle) == isOfGivenTeam));

        public IEnumerable<Goal> TeamGoals(Game game, string teamTitle)
            => game.Teams.Where(t => t.Title == teamTitle).SelectMany(t => t.GoalsRecord.Goals);

        public IEnumerable<Goal> OpponentGoals(Game game, string teamTitle)
            => game.Teams.Where(t => t.Title != teamTitle).SelectMany(t => t.GoalsRecord.Goals);

        public IActionResult Index()
        {
            var teams = _context.Team.GroupBy(t => t.Title).Select(t => t.First()).ToList();

            foreach (var team in teams)
            {
                var gamesParticipated = GamesTeamParticipated(team.Title);
                var teamGames = TeamGames(gamesParticipated, team.Title);
                var opponentTeamGames = TeamGames(gamesParticipated, team.Title, false);
                var goalsWon = teamGames.SelectMany(t => t.GoalsRecord.Goals);
                var goalsLost = opponentTeamGames.SelectMany(t => t.GoalsRecord.Goals);

                team.GoalsWon = goalsWon.Count();
                team.PenaltyGoals = goalsWon.Where(w => w.GoalType == GoalType.Penalty).Count();
                team.GoalsLost = goalsLost.Count();

            //var teamWins = gamesParticipated.Where(g =>
            //    TeamGoals(g, team.Title).Count() > OpponentGoals(g, team.Title).Count()
            //);

            //var teamLosses = gamesParticipated.Where(g =>
            //    TeamGoals(g, team.Title).Count() < OpponentGoals(g, team.Title).Count()
            //);

            //ArgumentException: Parameter 'expression.Type' is a 'football3.Controllers.TeamsController', which cannot be assigned to type 'System.Collections.IEnumerable'.
            //Parameter name: expression.Type

            //NotSupportedException: Cannot parse expression 'value(football3.Controllers.TeamsController)' as it has an unsupported type. Only query sources(that is, expressions that implement IEnumerable) and query operators can be parsed.

                var teamWins = gamesParticipated.Where(g =>
                   g.Teams.Where(t => t.Title == team.Title).SelectMany(t => t.GoalsRecord.Goals).Count() >
                   g.Teams.Where(t => t.Title != team.Title).SelectMany(t => t.GoalsRecord.Goals).Count()
                );

                var teamLosses = gamesParticipated.Where(g =>
                    g.Teams.Where(t => t.Title == team.Title).SelectMany(t => t.GoalsRecord.Goals).Count() <
                    g.Teams.Where(t => t.Title != team.Title).SelectMany(t => t.GoalsRecord.Goals).Count()
                );

                team.WinsDuringMainTime = teamWins.Count(g =>
                    g.Teams.Where(t => t.Title == team.Title).SelectMany(t => t.GoalsRecord.Goals)
                    .Max(x => x.Time).TotalMinutes <= mainTime
                );

                team.WinsDuringAddedTime = teamWins.Count(g =>
                    g.Teams.Where(t => t.Title == team.Title).SelectMany(t => t.GoalsRecord.Goals)
                    .Max(x => x.Time).TotalMinutes > mainTime
                );

                team.LossesDuringMainTime = teamLosses.Count(g =>
                    g.Teams.Where(t => t.Title != team.Title).SelectMany(t => t.GoalsRecord.Goals)
                    .Max(x => x.Time).TotalMinutes <= mainTime
                );

                team.LossesDuringAddedTime = teamLosses.Count(g =>
                    g.Teams.Where(t => t.Title != team.Title).SelectMany(t => t.GoalsRecord.Goals)
                    .Max(x => x.Time).TotalMinutes > mainTime
                );

                team.Points = team.WinsDuringMainTime * winDuringMainTimePoints
                            + team.WinsDuringAddedTime * winDuringAddedTimePoints
                            + team.LossesDuringAddedTime * lossDuringAddedTimePoints
                            + team.LossesDuringMainTime * lossDuringMainTimePoints;

                var players = _context
                    .Player
                    .Where(p => p.Team == team.Title)
                    .GroupBy(p => new { p.Firstname, p.Lastname, p.Team })
                    .Select(p => p.First())
                    .ToList();

                team.Defendors = players.Count(d => d.Role == PlayerRole.Defender);
                team.Forwards = players.Count(d => d.Role == PlayerRole.Forward);
                team.Goalkeepers = players.Count(d => d.Role == PlayerRole.Goalkeeper);

                ////public TimeSpan TotalTimePlayed { get; set; }
                ////public TimeSpan AverageTimePlayed { get; set; }
            }

            foreach (var team in teams)
                team.Place = teams.Count(p => p.Points > team.Points) + 1;

            return View(teams.OrderBy(t => t.Place));
        }
    }
}
