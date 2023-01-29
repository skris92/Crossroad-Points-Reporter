namespace Crossroad_Points_Reporter
{
    public static class Display
    {
        public static void Area(int[,] area)
        {
            int lengthY = area.GetLength(0);
            int lengthX = area.GetLength(1);

            string outputDiagram = "";

            for (int i = 0; i < lengthY; i++)
            {
                for (int j = 0; j < lengthX; j++)
                {
                    if (area[i, j] == 0)
                    {
                        outputDiagram += ".";
                    }
                    else
                    {
                        outputDiagram += area[i, j].ToString();
                    }
                }
                outputDiagram += "\n";
            }
            Console.Write(outputDiagram + "\n");
            Console.ReadKey();
        }

        public static void CrossroadPoints(Dictionary<string, int> crossroadPoints)
        {
            Console.WriteLine($"Number of dangerous points: {crossroadPoints.Count}\n");

            foreach (KeyValuePair<string, int> crossroadPoint in crossroadPoints)
            {
                Console.WriteLine(
                    $"({crossroadPoint.Key.Split(",")[0]}, " +
                    $"{crossroadPoint.Key.Split(",")[1]}) -> " +
                    $"{crossroadPoint.Value}");
            }

            Console.ReadKey();
        }
    }
}
