using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_AgentGuard.Data;
using BE_AgentGuard.Models;
using BE_AgentGuard.RouteModel;
using BE_AgentGuard.FuncMove;

namespace BE_AgentGuard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetsController : ControllerBase
    {
        private readonly BE_AgentGuardContext _context;

        public TargetsController(BE_AgentGuardContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Target>>> GetTarget()
        {
            return await _context.Target.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Target>> GetTarget(int id)
        {
            var target = await _context.Target.FindAsync(id);

            if (target == null)
            {
                return NotFound();
            }

            return target;
        }

        [HttpPut("{id}/pin")]
        public async Task<IActionResult> PinTarget([FromRoute] int id, [FromBody] Point point)
        {
            Target target = await _context.Target.FindAsync(id);
            Move move = new(point,target);
            bool start = move.Start();
            if (!start) { return BadRequest("the target is already pined"); }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok("the agent is pined successfully");
        }
        [HttpPut("{id}/move")]
        public async Task<IActionResult> MoveTarget([FromRoute]int id,[FromBody]Directions direction)
        {
            
            Target target = _context.Target.Find(id);
            if (!target.is_active)
            {
                BadRequest("Sorry, but the target has been eliminated");
            }            
            Move move = new Move(target.point,target);
            move.ChangeFree(direction);
            await _context.SaveChangesAsync();
            return Ok("the agent is moved successfully");
        }
        [HttpPost]
        public async Task<ActionResult<Target>> PostTarget(Target target)
        {
            _context.Target.Add(target);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTarget", new { id = target.Id }, target);
        }
        private bool TargetExists(int id)
        {
            return _context.Target.Any(e => e.Id == id);
        }
    }
}
