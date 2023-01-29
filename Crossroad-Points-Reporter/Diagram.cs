namespace Crossroad_Points_Reporter
{
    public class Diagram
    {
        public int[,] Area { get; private set; }
        public Dictionary<string, int> CrossroadPoints { get; private set; }

        public Diagram(int[,] area)
        {
            Area = area;
            CrossroadPoints = new();
        }

        public void SetAreaPoint(int coordX, int coordY)
        {
            Area[coordY, coordX]++;
        }

        public void UpdateCrossroadPoint(int coordX, int coordY)
        {
            CrossroadPoints[$"{coordX},{coordY}"]++;
        }

        public void SetCrossroadPoint(int coordX, int coordY)
        {
            CrossroadPoints.Add($"{coordX},{coordY}", Area[coordY, coordX]);
        }

        public void SortCrossroadPointsByCoordinates()
        {
            CrossroadPoints = CrossroadPoints
                .OrderBy(x => int.Parse(x.Key.Split(",")[0]))
                .ThenBy(x => int.Parse(x.Key.Split(",")[1]))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
