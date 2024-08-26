using BE_AgentGuard.Enums;

namespace FE_AgentGuard.Models.ServerModel
{
    public class MissionAssigned
    {
        public MissionAssigned(string token, StatusMission statusMission)
        {
            this.token = token;
            this.statusMission = statusMission;
        }

        public string token { get; set; }
        public StatusMission statusMission { get; set; }
    }
}
