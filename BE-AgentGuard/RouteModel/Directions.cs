using System.ComponentModel.DataAnnotations.Schema;

namespace BE_AgentGuard.RouteModel
{
    public class Directions
    {
        [NotMapped]
        public string token {  get; set; }
        public string direction {  get; set; }
    }
}
