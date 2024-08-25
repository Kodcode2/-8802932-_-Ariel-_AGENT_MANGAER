using FE_AgentGuard.InterFaces;
using FE_AgentGuard.Models.ServerModel;

namespace FE_AgentGuard.Models.Models
{
    public class Agent : Person
    {
        public Agent()
        {
            // אתה יכול להשאיר אותו ריק או לאתחל ערכים ברירת מחדל אם צריך
        }
        public string nickname { get; set; }
        public Agent(int id, string photoUrl, string color, Point point, bool is_active) : base(id, is_active, photoUrl, point) { }
    }
}
