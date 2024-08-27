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
using BE_AgentGuard.Enums;
using FE_AgentGuard.Models.ServerModel;

namespace FE_AgentGuard.Controllers
{
    public class MissionsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly MissionServer server;
        private TokenService tokenService;

        public MissionsController(HttpClient httpClient, TokenService tokenService)
        {
            _httpClient = httpClient;
            _url = "http://localhost:5149/missions";
            server = new(_httpClient, _url);
            this.tokenService = tokenService;
        }
        public async Task<IActionResult> Index()
        {
            //var fE_AgentGuardContext = _context.Mission.Include(m => m.Agent).Include(m => m.Target);
            MissionServer server = new(_httpClient, _url);
            return View(await server.GetObjectsAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var mission = await _context.Mission
            //    .Include(m => m.Agent)
            //    .Include(m => m.Target)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            MissionServer server = new(_httpClient, _url);
            Mission mission = await server.GetObjectAsync(id);
            if (mission == null)
            {
                return NotFound();
            }

            return View(mission);
        }
        //https://localhost:7254/Missions/assigned/1
        [HttpPost("missions/assigned/{id}")]
        public async Task<IActionResult> assigned(int id)
        {
            string token = tokenService.Token.token;
            StatusMission status = StatusMission.ASSIGNED;
            MissionAssigned mission = new(token, status);
            await server.UpdateObjectAsync(mission, id);
            return RedirectToAction("Index");
        }
    }
}