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

namespace FE_AgentGuard.Controllers
{
    public class AgentsController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly string UrlBase;
        AgentServer server ;

        public AgentsController(HttpClient HttpClient)
        {
            UrlBase = "http://localhost:5149/Agents";
            httpClient = HttpClient;
            server = new(httpClient, UrlBase);
        }

        // GET: Agents
        public async Task<IActionResult> Index()
        {
            
            return View(await server.GetObjectsAsync());
        }

        // GET: Agents/Details/5
        public async Task<IActionResult> Details(int id)
        {
            AgentServer server = new(httpClient, UrlBase);
            Agent agent = await server.GetObjectAsync(id);
            return View(agent);
        }
    }
}
