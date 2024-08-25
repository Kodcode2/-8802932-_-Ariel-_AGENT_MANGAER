using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_AgentGuard.Data;
using BE_AgentGuard.Models;

namespace BE_AgentGuard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly BE_AgentGuardContext _context;
        public MissionsController(BE_AgentGuardContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mission>>> GetMission()
        {
            return await _context.Mission.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Mission>> GetMission(int id)
        {
            var mission = await _context.Mission.FindAsync(id);

            if (mission == null)
            {
                return NotFound();
            }

            return mission;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMission(int id, Mission mission)
        {
            if (id != mission.Id)
            {
                return BadRequest();
            }

            _context.Entry(mission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MissionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<Mission>> PostMission(Mission mission)
        {
            _context.Mission.Add(mission);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMission", new { id = mission.Id }, mission);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            var mission = await _context.Mission.FindAsync(id);
            if (mission == null)
            {
                return NotFound();
            }

            _context.Mission.Remove(mission);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool MissionExists(int id)
        {
            return _context.Mission.Any(e => e.Id == id);
        }
    }
}
