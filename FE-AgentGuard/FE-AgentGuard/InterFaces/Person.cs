using FE_AgentGuard.Models.ServerModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace FE_AgentGuard.InterFaces
{
    public abstract class Person
    {
        private Point _point;

        protected Person(int id,bool is_active, string photoUrl, Point point)
        {
            Id = id;
            this.is_active = is_active;
            this.photoUrl = photoUrl;
            this.Point = point;
        }
        public Person() { }

        public int Id { get; set; }
        public int x { get; set; }
        public int y { get; set; }


        // is active (target = dead; agent = free) 
        public bool is_active { get; set; }
        public string photoUrl { get; set; }
        public string color {  get; set; }
        [NotMapped]
        public Point Point
        {
            get
            {
                return _point ?? (_point = new Point(x, y));
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

