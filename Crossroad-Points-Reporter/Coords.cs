namespace Crossroad_Points_Reporter
{
    public class Coords
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public void SetX(int coordinateX)
        {
            X = coordinateX;
        }

        public void SetY(int coordinateY)
        {
            Y = coordinateY;
        }
    }
}
