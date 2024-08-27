

using BE_AgentGuard.Interface;
using BE_AgentGuard.RouteModel;

namespace BE_AgentGuard.Servrices
{
    public class PointCalculations
    {
        private Point point1;
        private Point point2;

        public PointCalculations(Point point1, Point point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }
        public PointCalculations(Point point1)
        {
            this.point1 = point1;
        }
        public double difference()
        {

            double point = Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
            return point;
        }
        public Directions Steps()
        {
            Directions directions = new Directions();
            Point distancePoint = new Point(point2.X-point1.X,  point2.Y- point1.Y );

            if (distancePoint.X > 0)
            {
                directions.direction += "e";
            }
            if (distancePoint.X < 0)
            {
                directions.direction += "w";
            }
            if (distancePoint.Y > 0)
            {
                directions.direction += "n";
            }
            if (distancePoint.Y < 0)
            {
                directions.direction += "s";
            }

            return directions;
        }

        public int subtractMinFromMax(int num1 , int num2)
        {
            if (num1 > num2) { return num1 - num2; }
            return num2-num1;
        }
    }
}
