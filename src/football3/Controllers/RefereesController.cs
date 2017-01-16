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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Referee.ToListAsync());
        }
    }
}
