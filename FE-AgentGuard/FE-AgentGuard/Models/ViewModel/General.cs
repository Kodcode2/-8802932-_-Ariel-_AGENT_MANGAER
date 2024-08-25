using FE_AgentGuard.InterFaces;
using FE_AgentGuard.Models.Models;
using FE_AgentGuard.Models.ServerModel;

namespace FE_AgentGuard.Models.ViewModel
{
    public class General
    {
        public Dictionary<Point, Person> persons;
        public List<Target> targets;
        public List<Mission> missions;
        public List<Agent> agents;

        public General(Dictionary<Point, Person> persons, List<Target> targets, List<Mission> missions, List<Agent> agents)
        {
            this.persons = persons;
            this.targets = targets;
            this.missions = missions;
            this.agents = agents;
        }
    }
}
