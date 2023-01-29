﻿namespace Crossroad_Points_Reporter
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

        public void Display()
        {
            int lengthY = Area.GetLength(0);
            int lengthX = Area.GetLength(1);

            string outputDiagram = "";

            for (int i = 0; i < lengthY; i++)
            {
                for (int j = 0; j < lengthX; j++)
                {
                    if (Area[i, j] == 0)
                    {
                        outputDiagram += ".";
                    }
                    else
                    {
                        outputDiagram += Area[i, j].ToString();
                    }
                }
                outputDiagram += "\n";
            }
            Console.Write(outputDiagram + "\n");
            Console.ReadKey();
        }

        public void DisplayCrossroadPoints()
        {
            Console.WriteLine($"Number of dangerous points: {CrossroadPoints.Count}\n");

            foreach (KeyValuePair<string, int> crossroadPoint in CrossroadPoints)
            {
                Console.WriteLine(
                    $"({crossroadPoint.Key.Split(",")[0]}, " +
                    $"{crossroadPoint.Key.Split(",")[1]}) -> " +
                    $"{crossroadPoint.Value}");
            }

            Console.ReadKey();
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
