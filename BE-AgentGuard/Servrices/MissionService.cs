using BE_AgentGuard.Interface;
using BE_AgentGuard.Models;
using BE_AgentGuard.RouteModel;
using System;
using System.Collections.Generic;

namespace BE_AgentGuard.Servrices
{
    public class MissionService
    {
        public IPerson person;
        public List<IPerson> persons;
        // dict for person and to now which func I need to choice 
        private Dictionary<Type, Func< int, Mission>> HowPersonIt = new();
        public double distance;
        public MissionService(IPerson person, List<IPerson> persons)
        {
            this.person = person;
            this.persons = persons;
            HowPersonIt.Add(typeof(Target), IdTargetToMission);
            HowPersonIt.Add(typeof(Agent), IdAgentToMission);
        }

        public List<Mission> CheckMission()
        {
            List<IPerson> personsInRange = CheckPersonsInRange();
            List<Mission> missions = new();
            Type type = person.GetType();
            foreach (var personInRange in personsInRange)
            {
                // to now how person is Target or Agent
                missions.Add(HowPersonIt[type](person.Id));
            }
            return missions;
        }

        public Mission IdTargetToMission(int id)
        {
                Mission mission = new Mission();
                mission.agentID = person.Id;
                mission.targetID = id;
            mission.status = Enums.StatusMission.PENDING;

            return mission;
        }

        public Mission IdAgentToMission(int id)
        {
            Mission mission = new Mission();
            mission.targetID = person.Id;
            mission.agentID = id;
            mission.status = Enums.StatusMission.PENDING;
            mission.distance = distance;

            return mission;
        }

        public List<IPerson> CheckPersonsInRange()
        {
            Point point1 = person.point;
            List<IPerson> list = new();
            foreach (var person1 in persons)
            {
                Point point2 = person1.point;
                Pointcalculations server = new(point1, point2);
                distance = server.difference();
                if (distance > 200)
                {
                    list.Add(person1);
                }
            }
            return list;
        }
    }
}
