using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace BE_AgentGuard.Models
{
    public class Agent
    {
        
        private RouteModel.Point _point;
        public int id { get; set; }
        public string nickname { get; set; }
        public string photo_url { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        [NotMapped]
        public RouteModel.Point Point
        {
            get
            {
                return _point ?? (_point = new RouteModel.Point(x, y));
            }
            set
            {
                _point = value;
            }
        }

        public bool is_active { get; set; }

    }
}
