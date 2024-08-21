using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_AgentGuard.Data;
using BE_AgentGuard.Models;
using System.Drawing;
using BE_AgentGuard.RouteModel;
using BE_AgentGuard.FuncMove;

namespace BE_AgentGuard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly BE_AgentGuardContext _context;
        public Move move;

        public AgentsController(BE_AgentGuardContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAgents()
        {
            return await _context.Agent.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agent>> GetAgent(int id)
        {
            var agent = await _context.Agent.FindAsync(id);

            if (agent == null)
            {
                return NotFound();
            }

            return agent;
        }

        [HttpPut("{id}/pin")]
        public async Task<IActionResult> PinAgent(int id, RouteModel.Point point)
        {            
            Agent agent = await _context.Agent.FirstAsync(user => user.id == id);
            agent.Point = point;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentExists(id))
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
        [HttpPut("{id}/move")]
        public async Task<IActionResult> MoveAgent(int id,Directions directions)
        {
            Agent agent = _context.Agent.Find(id);
            move = new Move(agent.Point);
            agent.Point = move.Change(directions);
            return NoContent();
        }
       


        [HttpPost]
        public async Task<ActionResult<Agent>> PostAgent(Agent agent)
        {
            _context.Agent.Add(agent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgent", new { id = agent.id }, agent);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgent(int id)
        {
            var agent = await _context.Agent.FindAsync(id);
            if (agent == null)
            {
                return NotFound();
            }

            _context.Agent.Remove(agent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgentExists(int id)
        {
            return _context.Agent.Any(e => e.id == id);
        }
    }
}
