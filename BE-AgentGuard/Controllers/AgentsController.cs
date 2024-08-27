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
using BE_AgentGuard.Servrices;
using BE_AgentGuard.Interface;

namespace BE_AgentGuard.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly BE_AgentGuardContext _context;
        public AgentsController(BE_AgentGuardContext context)
        {
            _context = context;
        }


        [HttpGet]
        public List<Agent> GetAgents()
        {
            return _context.Agent.ToList();
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

        [HttpPut("/agents/{id}/pin")]
        public async Task<IActionResult> PinAgent(int id, [FromBody] RouteModel.Point point)
        {
            Agent agent = await _context.Agent.FindAsync(id);
            if (agent.point.OnTheMap)
            {
                return BadRequest("the agent is already pined");
            }
            agent.point = point;
            await _context.SaveChangesAsync();
            List<Target> targets = await _context.Target.Where(t => !t.is_active).ToListAsync();
            MissionService missionService = new(agent, targets.Cast<IPerson>().ToList());
            List<Mission> missions = _context.Mission.Where(M => M.status == Enums.StatusMission.PENDING).ToList();
            missions = missionService.CheckMission(missions);
            foreach (var item in missions)
            {
                _context.Add(item);
            }
            await _context.SaveChangesAsync();
            return Ok("the agent is pined successfully");
        }
        [HttpPut("{id}/move")]
        public async Task<IActionResult> MoveAgent([FromRoute] int id, [FromBody] Directions directions)
        {
            var agent = await _context.Agent.FindAsync(id);
            if (agent == null)
            {
                return NotFound("Agent not found");
            }

            if (agent.is_active)
            {
                return BadRequest("The agent is in mission");
            }
            if (!agent.point.OnTheMap)
            {
                return BadRequest("The agent is not on the map");
            }

            var move = new Move(agent.point, agent);
            agent = (Agent)move.ChangeFree(directions);
            if (agent is null) { return BadRequest("The map is over"); }


            _context.Update(agent);
            await _context.SaveChangesAsync();
            List<Target> targets = await _context.Target.Where<Target>(t => t.is_active).ToListAsync();
            MissionService missionService = new(agent, targets.Cast<IPerson>().ToList());
            List<Mission> missions = _context.Mission.Where(t => t.status == Enums.StatusMission.PENDING).ToList();
            missions =  missionService.CheckMission(missions);
            foreach (var mission in missions)
            {
                _context.Add(mission);
            }
            _context.SaveChanges();

            return Ok("The agent is moved successfully");
        }
        [HttpPost]
        public async Task<ActionResult<Agent>> PostAgent([FromBody] PostAgent postAgent)
        {
            Agent agent = new();
            agent.photoUrl = postAgent.photoUrl;
            agent.nickname = postAgent.nickname;
            _context.Agent.Add(agent);
            await _context.SaveChangesAsync();
            return Created("", new { id = agent.Id });
        }
        private bool AgentExists(int id)
        {
            return _context.Agent.Any(e => e.Id == id);
        }
    }
}
