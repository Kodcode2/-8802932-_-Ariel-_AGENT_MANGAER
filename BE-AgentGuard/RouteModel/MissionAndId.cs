using BE_AgentGuard.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_AgentGuard.RouteModel
{
    [NotMapped]
    public class MissionAndId
    {
        public MissionAndId(Mission mission, int id)
        {
            Mission = mission;
            this.id = id;
        }

        public Mission Mission { get; set; }
        public int id { get; set; }
    }
}
