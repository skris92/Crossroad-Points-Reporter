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
            // Incrementing area point value
            Area[coordY, coordX]++;
        }

        public void SetCrossroadPoint(int coordX, int coordY)
        {
            if (CrossroadPoints.ContainsKey($"{coordX},{coordY}")) // Update existing crossroad point
            {
                CrossroadPoints[$"{coordX},{coordY}"]++;
            }
            else
            {
                CrossroadPoints.Add($"{coordX},{coordY}", Area[coordY, coordX]);
            }
        }

        private void SortCrossroadPointsByCoordinates()
        {
            // Sorting crossroad points by coordinate X and after by Y
            CrossroadPoints = CrossroadPoints
                .OrderBy(x => int.Parse(x.Key.Split(",")[0]))
                .ThenBy(x => int.Parse(x.Key.Split(",")[1]))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public string GetCrossroadPointsReport()
        {
            List<string> result = new();
            SortCrossroadPointsByCoordinates();

            result.Add($"Number of dangerous points: {CrossroadPoints.Count}\n");

            foreach (KeyValuePair<string, int> crossroadPoint in CrossroadPoints)
            {
                result.Add( 
                    $"({crossroadPoint.Key.Split(",")[0]}, " +
                    $"{crossroadPoint.Key.Split(",")[1]}) -> " +
                    $"{crossroadPoint.Value}");
            }

            return string.Join("\n", result);
        }
    }
}
