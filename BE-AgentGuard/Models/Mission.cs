using BE_AgentGuard.Enums;

namespace BE_AgentGuard.Models
{
    public class Mission
    {
        public int Id { get; set; }
        public int agentID { get; set; }
        public Agent Agent { get; set; }
        public int targetID { get; set; }
        public Target Target { get; set; }
        public double distance { get; set; }
        public TimeOnly missionStart { get; set; }
        public TimeOnly duration { get; set; }
        public int remainingTime { get; set; }
        public StatusMission status { get; set; }
    }
}
