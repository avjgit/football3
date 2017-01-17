using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using football3.Data;
using footballnet.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using football3.Utils;

namespace football3.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return View(await _context.Game.ToListAsync());
        }

        public bool GameExists(Game json) =>
            _context.Game.Any(g =>
                json.Date == g.Date &&
                json.Place == g.Place &&
                json.Teams.All(jsonTeam => g.Teams.Any(
                    dbteam => dbteam.Title == jsonTeam.Title)));

        public async Task<IActionResult> DeleteAll()
        {
            _context.Player.RemoveRange(_context.Player);
            _context.Referee.RemoveRange(_context.Referee);
            _context.Team.RemoveRange(_context.Team);
            _context.Game.RemoveRange(_context.Game);

            await _context.SaveChangesAsync();
            return View("Index", await _context.Game.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            var parser = new Parser();
            foreach (var file in files.Where(f => f.Length > 0))
            {
                if (ModelState.IsValid)
                {
                    using (var fileStream = new StreamReader(file.OpenReadStream()))
                    {
                        var game = JsonConvert.DeserializeObject<GameRecord>(fileStream.ReadToEnd()).Game;

                        if (GameExists(game))
                            continue;

                        foreach (var team in game.Teams)
                        {
                            foreach (var player in team.AllPLayersRecord.Players)
                                player.Team = team.Title;

                            if (team.GoalsRecord != null)
                                foreach (var goal in team.GoalsRecord.Goals)
                                    goal.Time = parser.GetTime(goal.TimeRecord);

                            if (team.PenaltiesRecord != null)
                                foreach (var penalty in team.PenaltiesRecord.Penalties)
                                    penalty.Time = parser.GetTime(penalty.TimeRecord);

                            if (team.ChangeRecord != null)
                                foreach (var change in team.ChangeRecord.Changes)
                                    change.Time = parser.GetTime(change.TimeRecord);
                        }

                        _context.Add(game);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return View("Index", await _context.Game.ToListAsync());
        }
    }
}
