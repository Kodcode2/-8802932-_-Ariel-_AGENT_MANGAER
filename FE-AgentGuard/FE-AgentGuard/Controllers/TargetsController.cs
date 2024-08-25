using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FE_AgentGuard.Data;
using FE_AgentGuard.Models.Models;
using FE_AgentGuard.ServerHandling;
using System.Net.Http;
using System.Runtime.Intrinsics.Arm;

namespace FE_AgentGuard.Controllers
{
    public class TargetsController : Controller
    {
        private readonly FE_AgentGuardContext _context;
        private readonly HttpClient httpClient;
        private readonly string UrlBase;

        public TargetsController(HttpClient HttpClient)
        {
            UrlBase = "https://localhost:7030/Targets";
            httpClient = HttpClient;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Target.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            TargetServer server = new(httpClient, UrlBase);
            Target agent = await server.GetObjectAsync(id);
            return View(agent);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("name,position,Id,x,y,is_active,photoUrl,color")] Target target)
        {
            if (ModelState.IsValid)
            {
                _context.Add(target);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(target);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var target = await _context.Target.FindAsync(id);
            if (target == null)
            {
                return NotFound();
            }
            return View(target);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("name,position,Id,x,y,is_active,photoUrl,color")] Target target)
        {
            if (id != target.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(target);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TargetExists(target.Id))
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
            return View(target);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var target = await _context.Target
                .FirstOrDefaultAsync(m => m.Id == id);
            if (target == null)
            {
                return NotFound();
            }

            return View(target);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var target = await _context.Target.FindAsync(id);
            if (target != null)
            {
                _context.Target.Remove(target);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TargetExists(int id)
        {
            return _context.Target.Any(e => e.Id == id);
        }
    }
}
