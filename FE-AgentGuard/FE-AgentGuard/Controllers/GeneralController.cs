using FE_AgentGuard.InterFaces;
using FE_AgentGuard.Models.Models;
using FE_AgentGuard.Models.ServerModel;
using FE_AgentGuard.Models.ViewModel;
using FE_AgentGuard.ServerHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace FE_AgentGuard.Controllers
{
    public class GeneralController : Controller
    {
        // GET: GeneralController1
        private readonly HttpClient httpClient;
        private readonly string urlAgent;
        private readonly string urlTarget;
        private readonly string urlMission;

        public GeneralController(HttpClient HttpClient)
        {
            httpClient = HttpClient;
            urlAgent = "https://localhost:7030/Agents";
            urlTarget = "https://localhost:7030/Targets";
            urlMission = "https://localhost:7030/missions";
        }

        public async Task<ActionResult> GeneralView()
        {

            AgentServer AgentServer = new(httpClient, urlAgent);
            TargetServer TargetServer = new(httpClient, urlTarget);
            MissionServer MissionServer = new(httpClient, urlTarget);
            Task<List<Agent>> taskAgents = AgentServer.GetObjectsAsync();
            Task<List<Target>> taskTargets = TargetServer.GetObjectsAsync();
            Task<List<Mission>> taskMission = MissionServer.GetObjectsAsync();

            await Task.WhenAll(taskAgents, taskTargets, taskMission);
            List<Agent> agents = await taskAgents;
            List<Target> targets = await taskTargets;
            List<Mission> missions = await taskMission;

            List<Person> persons = agents.Cast<Person>()
                                        .Concat(targets.Cast<Person>())
                                        .ToList();
            Dictionary<Point, Person> dict = new Dictionary<Point, Person>();
            foreach (var person in persons)
            {
                person.color = changeColor(person);
                dict[person.Point] = person;
            }
            General general = new(dict, targets, missions, agents);

            return View(general);
        }

        // GET: GeneralController1/Details/5
        public async Task<ActionResult> Details(int id)
        {
            PersonServer server = new(httpClient, urlAgent);
            Person person = await server.GetObjectAsync(id);
            return View(person);
        }

        // GET: GeneralController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GeneralController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GeneralController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GeneralController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GeneralController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GeneralController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public string changeColor(Person person)
        {
            if(person is Agent)
            {
                if (person.is_active) { return "yellow"; }
                return "blue";
            }
            if (person.is_active) { return "green"; }
            return "red";
        }
    }
}
