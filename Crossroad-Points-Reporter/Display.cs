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
    }
}
