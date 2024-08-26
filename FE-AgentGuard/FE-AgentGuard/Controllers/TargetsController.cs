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
        private readonly HttpClient httpClient;
        private readonly string UrlBase;
        private readonly TargetServer server;

        public TargetsController(HttpClient HttpClient)
        {
            UrlBase = "http://localhost:5149/Targets";
            httpClient = HttpClient;
            server = new(httpClient, UrlBase);
        }

        public async Task<IActionResult> Index()
        {
            return View(await server.GetObjectsAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            Target agent = await server.GetObjectAsync(id);
            return View(agent);
        }


   
    }
}
