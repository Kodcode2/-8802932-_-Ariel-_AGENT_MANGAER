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
using BE_AgentGuard.Servrices;
using Microsoft.Ajax.Utilities;
using BE_AgentGuard.Interface;

namespace BE_AgentGuard.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly BE_AgentGuardContext _context;
        public MissionsController(BE_AgentGuardContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mission>>> GetMissions()
        {
            return await _context.Mission.ToListAsync();
        }
        //public List<Mission> GetMissionsAssigned()
        //{
        //    return _context.Mission.Where(statusMission => statusMission.status == Enums.StatusMission.ASSIGNED).ToList();
        //}
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
        public async Task<IActionResult> PutMission(int id, [FromBody] MissionAssigned missionAssigned)
        {
            Mission mission = _context.Mission.Find(id);
            TimeOnly timeNow = TimeOnly.FromDateTime(DateTime.Now);
            mission.missionStart = timeNow;
            mission.remainingTime = (int)mission.distance / 5;
            mission.status = missionAssigned.statusMission;
            return NoContent();
        }
        [HttpPut("mission/update")]
        public async Task<IActionResult> Update()
        {

            List<Agent> agents = _context.Agent.Where(status => status.is_active).ToList();
            List<Agent> Agents = MissionService.MoveAgentAssigned(agents);
            foreach (var agent in Agents)
            {
                _context.Agent.Update(agent);
            }
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPost]
        public async Task<int> PostMission(Mission mission)
        {
            _context.Mission.Add(mission);
            await _context.SaveChangesAsync();

            return mission.Id;
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

            return Ok("mission is deleted successfully"); ;
        }
        private bool MissionExists(int id)
        {
            return _context.Mission.Any(e => e.Id == id);
        }
    }
}
