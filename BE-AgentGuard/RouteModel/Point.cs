namespace BE_AgentGuard.RouteModel
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
                if (X==-1 || Y==-1)
                {
                    return false;
                } 
                return true;
            } 
        }

    }
}
