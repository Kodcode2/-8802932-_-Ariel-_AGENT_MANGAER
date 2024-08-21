using System.Drawing;

namespace BE_AgentGuard.Models
{
    public class Agent
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public string photo_url { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public bool is_active { get; set; }
        
    }
}
