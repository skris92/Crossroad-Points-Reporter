namespace Crossroad_Points_Reporter
{
    public static class Display
    {
        public static void Diagram(int[,] area)
        {
            int lengthY = area.GetLength(0);
            int lengthX = area.GetLength(1);

            string outputDiagram = "";

            for (int i = 0; i < lengthY; i++)
            {
                ShowProgressBar("Creating diagram.. ", i, lengthY);

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
            Console.Write(outputDiagram + "\nPress any key to view results");
            Console.ReadKey();
        }

        public static void Result(string result)
        {
                Console.WriteLine(result + "\n");
        }

        private static void ShowProgressBar(string message, int iteration, int areaLength)
        {
            float progressPercent = (iteration + 1) / (float)areaLength * 50;

            Console.SetCursorPosition(0, 0);
            Console.Write($"{message}" +
                          $"[{new string('#', (int)progressPercent)}" +
                          $"{new string('-', 50 - (int)progressPercent)}]\n");
        }
    }
}
