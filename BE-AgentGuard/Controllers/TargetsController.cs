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
using BE_AgentGuard.Interface;
using BE_AgentGuard.Servrices;

namespace BE_AgentGuard.Controllers
{
    [Route("[controller]")]
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
            Move move = new(point, target);
            bool start = move.Start();
            if (!start) { return BadRequest("the target is already pined"); }
            _context.SaveChanges();
            CheckMission(target);
            return Ok("the agent is pined successfully");
        }
        [HttpPut("{id}/move")]
        public async Task<IActionResult> MoveTarget([FromRoute] int id, [FromBody] Directions direction)
        {

            Target target = _context.Target.Find(id);
            if (!target.is_active)
            {
                BadRequest("Sorry, but the target has been eliminated");
            }
            Move move = new Move(target.point, target);
            target = (Target)move.ChangeFree(direction);
            _context.Update(target);
            await _context.SaveChangesAsync();


            return Ok("the agent is moved successfully");
        }
        [HttpPost]
        public async Task<ActionResult<Target>> PostTarget(PostTarget postTarget)
        {
            Target target = new();
            target.name = postTarget.name;
            target.position = postTarget.position;
            target.photoUrl = postTarget.photoUrl;
            _context.Target.Add(target);
            await _context.SaveChangesAsync();
            return Created("", new { id = target.Id });
        }
        private bool TargetExists(int id)
        {
            return _context.Target.Any(e => e.Id == id);
        }
       async Task CheckMission(Target target)
        {
            List<Agent> agents = await _context.Agent.Where(t => t.is_active).ToListAsync();
            MissionService missionService = new(target, agents.Cast<IPerson>().ToList());
            List<Mission> missions = missionService.CheckMission();
            foreach (var item in missions)
            {
                _context.Add(item);
            }
            await _context.SaveChangesAsync();
        }
    }
}
