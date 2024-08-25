using BE_AgentGuard.Interface;
using BE_AgentGuard.Models;
using BE_AgentGuard.RouteModel;
using System.Drawing;

namespace BE_AgentGuard.FuncMove
{
    public class Move : ImapHandling
    {
        public RouteModel.Point _point;
        public IPerson person;
        public Move(RouteModel.Point point, IPerson person)
        {
            _point = point;
            this.person = person;
        }
        public IPerson  ChangeFree(Directions directions)
        {
            if (directions.direction.Contains("n"))
            {
                _point.Y++;
            }
            if (directions.direction.Contains("s"))
            {
                _point.Y--;
            }
            if (directions.direction.Contains("e"))
            {
                _point.X++;
            }
            if (directions.direction.Contains("w"))
            {
                _point.X--;
            }
            person.point = _point;
            return person;
        }
        public bool Start()
        {
            if (person.point.OnTheMap)
            {
                return false;
            }
            person.point = _point;
            return true;
        }

        public void ChangeToTarget(Target target) 
        {
            
        }
    }
}
