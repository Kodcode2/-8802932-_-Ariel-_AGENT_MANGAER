using BE_AgentGuard.RouteModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Security.Cryptography;

namespace BE_AgentGuard.Interface
{
    public abstract class IPerson
    {

        private RouteModel.Point _point;
        public int Id { get; set; }
        public int x { get; set; } = -1;
        public int y { get; set; } = -1;


        // is active (target = dead; agent = free) 
        public bool is_active {  get; set; }
        public string photoUrl { get; set; }
        [NotMapped]
        public RouteModel.Point point
        {
            get
            {
                return _point ?? (_point = new RouteModel.Point(x, y));
            }
            set
            {
                _point = value;
                x = value.X;
                y = value.Y;
            }
        }
    }
}
