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
            CheckMissionExpire();
            UpdateMissions();
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
            Mission mission = new();
            List<Mission> missions = _context.Mission
    .Include(m => m.Agent)
    .Include(m => m.Target)
    .ToList();
            foreach (var item in missions)
            {
                if (item.Id == id)
                {
                    mission = item;
                }
                if(item.Id == id && item.Agent.is_active)
                {
                    return BadRequest("thw agent is active already");
                }
            }
            foreach (var item in missions)
            {
                if (item.Id != mission.Id && (item.agentID == mission.agentID || item.targetID == mission.targetID))
                {
                    _context.Mission.Remove(item);
                }             
            }

            TimeOnly timeNow = TimeOnly.FromDateTime(DateTime.Now);
            mission.missionStart = timeNow;
            mission.remainingTime = (int)mission.distance / 5;
            mission.duration = new TimeSpan();
            mission.status = missionAssigned.statusMission;
            mission.Agent.is_active = true;
            _context.Update(mission);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("mission/update")]
        public async Task<IActionResult> UpdateAgents()
        {

            List<Agent> agents = _context.Agent.Where(status => status.is_active).ToList();
            List<Agent> Agents = MissionService.MoveAgentAssigned(agents);
            foreach (var agent in Agents)
            {
                _context.Agent.Update(agent);

            }
            _context.SaveChanges();
            CheckKills();


            return NoContent();
        }
        private void UpdateMissions()
        {
            var missions = _context.Mission
    .Include(m => m.Agent)
    .Include(m => m.Target)
    .Where(m => m.status == Enums.StatusMission.ASSIGNED);
            foreach (var item in missions)
            {
                PointCalculations point = new(item.Agent.point, item.Target.point);
                item.duration = MissionService.duration(item.missionStart);
                item.distance = point.difference();
                item.remainingTime = (int)point.difference() / 5;
                _context.Update(item);

            }
            _context.SaveChanges();
        }
        //[HttpPost]
        //public async Task<int> PostMission(Mission mission)
        //{
        //    _context.Mission.Add(mission);
        //    await _context.SaveChangesAsync();

        //    return mission.Id;
        //}
        [HttpDelete("{id}")]
        public async void DeleteMission(int id)
        {
            var mission = await _context.Mission.FindAsync(id);
            _context.Mission.Remove(mission);
            await _context.SaveChangesAsync();

            return;
        }
        private bool MissionExists(int id)
        {
            return _context.Mission.Any(e => e.Id == id);
        }
        private void CheckKills()
        {
            var mission = _context.Mission
                .Include(m => m.Agent)
                .Include(m => m.Target)
                .Where(m => m.status == Enums.StatusMission.ASSIGNED);
            foreach (var item in mission)
            {
                if (MissionService.ChanceToKill(item.Agent, item.Target))
                {
                    item.Agent.kills += 1;
                    item.Agent.is_active = false;
                    item.Target.is_active = false;
                    item.status = Enums.StatusMission.COMPLETED;                   
                }
            }
            _context.SaveChanges();
        }
        private void CheckMissionExpire()
        {
            List<Mission> missionToCheck = _context.Mission.Include(m => m.Agent)
                .Include(m => m.Target)
                .Where(m => m.status == Enums.StatusMission.ASSIGNED).ToList();
            List<int> intsMissionsToDelete = MissionService.CheckExpiredMission(missionToCheck);
            for (int i = 0; i < intsMissionsToDelete.Count; i++)
            {
                DeleteMission(intsMissionsToDelete[i]);
            }
        }

    }
}
