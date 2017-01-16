using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.SingleOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Place,Spectators")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.SingleOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Place,Spectators")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.SingleOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Game.SingleOrDefaultAsync(m => m.Id == id);
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
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
            //_context.PlayerRecord.RemoveRange(_context.PlayersList);
            //_context.PlayersNr.RemoveRange(_context.PlayersNr);
            //_context.PlayersNrList.RemoveRange(_context.PlayersNrList);
            //_context.Penalty.RemoveRange(_context.Penalty);
            //_context.PenaltiesList.RemoveRange(_context.PenaltiesList);
            //_context.Goal.RemoveRange(_context.Goal);
            //_context.GoalsList.RemoveRange(_context.GoalsList);
            //_context.Change.RemoveRange(_context.Change);
            //_context.ChangeRecord.RemoveRange(_context.ChangeRecord);
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
