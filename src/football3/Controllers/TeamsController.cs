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

        public IActionResult Index()
        {
            var teams = _context.Team.GroupBy(t => t.Title).Select(t => t.First()).ToList();
            foreach (var team in teams)
            {
                var gamesParticipated = _context.Game.Where(g => g.Teams.Any(t => t.Title == team.Title));
                var teamGames = gamesParticipated.SelectMany(g => g.Teams.Where(t => t.Title == team.Title));
                var opposingTeamGames = gamesParticipated.SelectMany(g => g.Teams.Where(t => t.Title != team.Title));
                var goalsWon = teamGames.SelectMany(t => t.GoalsRecord.Goals);
                team.GoalsWon = goalsWon.Count();
                team.PenaltyGoals = goalsWon.Where(w => w.GoalType == GoalType.Penalty).Count();
                team.GoalsLost = opposingTeamGames.SelectMany(t => t.GoalsRecord.Goals).Count();

                var teamWins = gamesParticipated.Where(g =>
                    g.Teams.Where(t => t.Title == team.Title).SelectMany(t => t.GoalsRecord.Goals).Count() >
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

                var teamLosses = gamesParticipated.Where(g =>
                    g.Teams.Where(t => t.Title == team.Title).SelectMany(t => t.GoalsRecord.Goals).Count() <
                    g.Teams.Where(t => t.Title != team.Title).SelectMany(t => t.GoalsRecord.Goals).Count()
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

                //var players = _context.Player.Where(u => u.Team == team.Title);
                //team.Defendors = players.Count(d => d.Role == PlayerRole.Defender);
                //team.Forwards = players.Count(d => d.Role == PlayerRole.Forward);
                //team.Goalkeepers = players.Count(d => d.Role == PlayerRole.Goalkeeper);

                ////public TimeSpan TotalTimePlayed { get; set; }
                ////public TimeSpan AverageTimePlayed { get; set; }
            }

            foreach (var team in teams)
                team.Place = teams.Count(p => p.Points > team.Points) + 1;

            return View(teams.OrderBy(t => t.Place));
        }
    }
}
