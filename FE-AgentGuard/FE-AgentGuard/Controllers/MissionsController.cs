using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FE_AgentGuard.Data;
using FE_AgentGuard.Models.Models;

namespace FE_AgentGuard.Controllers
{
    public class MissionsController : Controller
    {
        private readonly FE_AgentGuardContext _context;

        public MissionsController(FE_AgentGuardContext context)
        {
            _context = context;
        }

        // GET: Missions
        public async Task<IActionResult> Index()
        {
            var fE_AgentGuardContext = _context.Mission.Include(m => m.Agent).Include(m => m.Target);
            return View(await fE_AgentGuardContext.ToListAsync());
        }

        // GET: Missions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mission = await _context.Mission
                .Include(m => m.Agent)
                .Include(m => m.Target)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mission == null)
            {
                return NotFound();
            }

            return View(mission);
        }

        // GET: Missions/Create
        public IActionResult Create()
        {
            ViewData["agentID"] = new SelectList(_context.Set<Agent>(), "Id", "Id");
            ViewData["targetID"] = new SelectList(_context.Set<Target>(), "Id", "Id");
            return View();
        }

        // POST: Missions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,agentID,targetID,distance,missionStart,duration,remainingTime,status")] Mission mission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["agentID"] = new SelectList(_context.Set<Agent>(), "Id", "Id", mission.agentID);
            ViewData["targetID"] = new SelectList(_context.Set<Target>(), "Id", "Id", mission.targetID);
            return View(mission);
        }

        // GET: Missions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mission = await _context.Mission.FindAsync(id);
            if (mission == null)
            {
                return NotFound();
            }
            ViewData["agentID"] = new SelectList(_context.Set<Agent>(), "Id", "Id", mission.agentID);
            ViewData["targetID"] = new SelectList(_context.Set<Target>(), "Id", "Id", mission.targetID);
            return View(mission);
        }

        // POST: Missions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,agentID,targetID,distance,missionStart,duration,remainingTime,status")] Mission mission)
        {
            if (id != mission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MissionExists(mission.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["agentID"] = new SelectList(_context.Set<Agent>(), "Id", "Id", mission.agentID);
            ViewData["targetID"] = new SelectList(_context.Set<Target>(), "Id", "Id", mission.targetID);
            return View(mission);
        }

        // GET: Missions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mission = await _context.Mission
                .Include(m => m.Agent)
                .Include(m => m.Target)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mission == null)
            {
                return NotFound();
            }

            return View(mission);
        }

        // POST: Missions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mission = await _context.Mission.FindAsync(id);
            if (mission != null)
            {
                _context.Mission.Remove(mission);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MissionExists(int id)
        {
            return _context.Mission.Any(e => e.Id == id);
        }
    }
}
