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
        private TokenService tokenService;

        public GeneralController(HttpClient HttpClient)
        {
            httpClient = HttpClient;
            urlAgent = "http://localhost:5149/Agents";
            urlTarget = "http://localhost:5149/Targets";
            urlMission = "http://localhost:5149/missions";
        }

        public async Task<ActionResult> GeneralView()
        {

            AgentServer AgentServer = new(httpClient, urlAgent);
            TargetServer TargetServer = new(httpClient, urlTarget);
            MissionServer MissionServer = new(httpClient, urlTarget);
            Task<List<Agent>> taskAgents = AgentServer.GetObjectsAsync();
            Task<List<Target>> taskTargets = TargetServer.GetObjectsAsync();
            Task<List<Mission>> taskMission = MissionServer.GetObjectsAsync();

            List<Agent> agents = await taskAgents;
            List<Target> targets = await taskTargets;
            List<Mission> missions = await taskMission;

            List<Person> persons = agents.Cast<Person>()
                                        .Concat(targets.Cast<Person>())
                                        .ToList();
            Dictionary<Point, Person> dict = new Dictionary<Point, Person>();
            foreach (var person in persons)
            {
                person.color = changeColor(person,missions);
                dict[person.Point] = person;
            }
            General general = new(dict, targets, missions, agents);

            return View(general);
        }

        public async Task<ActionResult> Details(int id)
        {
            PersonServer server = new(httpClient, urlAgent);
            Person person = await server.GetObjectAsync(id);
            return View(person);
        }


        public string changeColor(Person person,List<Mission> mission
            )
        {
            if(person is Agent)
            {
                if (person.is_active) { return "yellow"; }
                return "blue";
            }
            if (TargetAssigned(person.Id,mission)) { return "green"; }
            return "red";
        }
        private bool TargetAssigned(int id, List<Mission> missions)
        {
            foreach (var mission in missions)
            {
                if (mission.targetID == id) {  return true; }
            }
            return false;
        }
    }
}
