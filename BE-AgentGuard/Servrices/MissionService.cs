using BE_AgentGuard.Controllers;
using BE_AgentGuard.Data;
using BE_AgentGuard.FuncMove;
using BE_AgentGuard.Interface;
using BE_AgentGuard.Models;
using BE_AgentGuard.RouteModel;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BE_AgentGuard.Servrices
{
    public class MissionService
    {
        public IPerson person;
        public List<IPerson> persons;
        // dict for person and to now which func I need to choice 
        private Dictionary<Type, Func<int, Mission>> HowPersonIt = new();
        public double distance;
        public MissionService(IPerson person, List<IPerson> persons)
        {
            this.person = person;
            this.persons = persons;
            HowPersonIt.Add(typeof(Target), IdTargetToMission);
            HowPersonIt.Add(typeof(Agent), IdAgentToMission);
        }
        public MissionService( List<IPerson> persons)
        {
            this.persons = persons;
        }
        public List<Mission> CheckMission()
        {
            
            List<IPerson> personsInRange = CheckPersonsInRange();
            List<Mission> missions = new();
            Type type = person.GetType();
            foreach (var personInRange in personsInRange)
            {
                // to now how person is Target or Agent
                missions.Add(HowPersonIt[type](personInRange.Id));
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
                PointCalculations server = new(point1, point2);
                distance = server.difference();
                if (distance < 200 && person1.point.OnTheMap)
                {
                    list.Add(person1);
                }
            }
            return list;
        }
        public static List<Agent> MoveAgentAssigned(List<Agent> agents)
        {
            foreach (Agent agent in agents)
            {
                if (agent.is_active)
                {
                    PointCalculations a = new(agent.point);
                    Move b = new(agent.point, agent);
                    b.ChangeFree(a.Steps());
                }
            }                
            return agents;
        }
        public static int Kill(Agent agent,Mission mission,Target target) 
        {
                agent.is_active = false;
                target.is_active = true;
                return mission.Id;
            
        }
        public static bool ChanceToKill(Agent agent, Target target) 
        {
            if (agent.point == target.point) { return true; }
            return false;
        }
        public static List<int> CheckExpiredMission(List<Mission> missions)
        {
            List<int> intsMissionToDelete = new List<int>();
            foreach (var item in missions)
            {
                if (item.distance>200)
                {
                    intsMissionToDelete.Add(item.Id);
                }
            }
            return intsMissionToDelete;
        }

        public static TimeSpan duration(TimeOnly start)
        {
            TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);
            return now - start;
        }
    }
}
