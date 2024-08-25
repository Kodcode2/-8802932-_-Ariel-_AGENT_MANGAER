using FE_AgentGuard.InterFaces;
using FE_AgentGuard.Models.ServerModel;

namespace FE_AgentGuard.Models.Models
{
    public class Target : Person
    {
        public string name { get; set; }
        public string position { get; set; }
        public Target(int id, string photo_url, Point point, string name1, string position1,bool is_active) : base(id, is_active, photo_url, point)
        {
            name = name1;
            position = position1;
        }
        public Target() { }
    }
}
