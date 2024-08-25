using System.Xml.Linq;

namespace FE_AgentGuard.Models.ServerModel
{

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public bool OnTheMap
        {
            get
            {
                if (X == -1 || Y == -1)
                {
                    return false;
                }
                return true;
            }
        }




        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Point other = (Point)obj;
            return X == other.X && Y == other.Y;
        }
        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }
    }
}

