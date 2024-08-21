using BE_AgentGuard.RouteModel;
using System.Drawing;

namespace BE_AgentGuard.FuncMove
{
    public class Move
    {
        public RouteModel.Point _point;
        public Move(RouteModel.Point point)
        {
             point = point;
        }
    public  RouteModel.Point Change(Directions directions)
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
            return _point;
        }
    }
}
