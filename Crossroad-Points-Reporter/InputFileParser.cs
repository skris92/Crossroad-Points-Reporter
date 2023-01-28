namespace Crossroad_Points_Reporter
{
    public class InputFileParser
    {
        public List<VentLine> GetVentLines()
        {
            bool correctLinesFormat = false;
            List<string> inputFileLines = new();

            while (!correctLinesFormat)
            {
                string path = GetFilePath();

                try
                {
                    inputFileLines = GetInputFileLines(path);
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Invalid file path!");
                    continue;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File not found!");
                    continue;
                }

                correctLinesFormat = CheckInputFileLinesFormat(inputFileLines);
            }

            List<VentLine> ventLines = ConvertLinesToVentLines(inputFileLines);

            List<VentLine> filteredVentLines = FilterVentLinesByDirection(ventLines);

            return filteredVentLines;
        }

        private string GetFilePath()
        {
            while (true)
            {
                Console.WriteLine("Enter file path: ");
                string? path = Console.ReadLine();

                if (path == null || path == "")
                {
                    ClearConsole();
                    continue;
                }
                else if (!CheckFileExtension(path))
                {
                    ClearConsole();
                    Console.WriteLine("Invalid file extension!");
                    continue;
                }
                ClearConsole();
                return path;
            }
        }

        private bool CheckFileExtension(string path)
        {
            if (Path.GetExtension(path) != ".txt") return false;
            return true;
        }

        private List<string> GetInputFileLines(string path)
        {
            return File.ReadAllLines(path).ToList();
        }

        private bool CheckInputFileLinesFormat(List<string> lines)
        {
            // Checking input format before any conversions
            foreach (string line in lines)
            {
                try
                {
                    string[] startCoords = line.Split(" -> ")[0].Split(",");
                    string[] endCoords = line.Split(" -> ")[1].Split(",");

                    if (!startCoords[0].All(char.IsDigit) ||
                        !startCoords[1].All(char.IsDigit) ||
                        !endCoords[0].All(char.IsDigit) ||
                        !endCoords[1].All(char.IsDigit) ||
                        startCoords.Length > 2 || endCoords.Length > 2) // More than one "," between coordinates
                    {
                        Console.WriteLine("Input data is not in correct format!");
                        return false;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Input data is not in correct format!");
                    return false;
                }
            }

            return true;
        }

        private List<VentLine> ConvertLinesToVentLines(List<string> filteredInputLines)
        {
            List<VentLine> outputVentLines = new List<VentLine>();

            foreach (string line in filteredInputLines)
            {
                // Converting string numbers to integers
                int startCoordX = int.Parse(line.Split(" -> ")[0].Split(',')[0]);
                int startCoordY = int.Parse(line.Split(" -> ")[0].Split(',')[1]);

                int endCoordX = int.Parse(line.Split(" -> ")[1].Split(',')[0]);
                int endCoordY = int.Parse(line.Split(" -> ")[1].Split(',')[1]);

                Coords startCoords = new(startCoordX, startCoordY);
                Coords endCoords = new(endCoordX, endCoordY);

                VentLine ventLine = new(startCoords, endCoords);

                outputVentLines.Add(ventLine);
            }

            return outputVentLines;
        }

        private List<VentLine> FilterVentLinesByDirection(List<VentLine> ventLines)
        {
            List<VentLine> filteredVentLines = new List<VentLine>();

            foreach (VentLine vl in ventLines)
            {
                if (vl.StartCoords.X == vl.EndCoords.X && vl.StartCoords.Y != vl.EndCoords.Y || // vertical
                    vl.StartCoords.Y == vl.EndCoords.Y && vl.StartCoords.X != vl.EndCoords.X || // horizontal
                    Math.Abs(vl.StartCoords.X - vl.EndCoords.X) == Math.Abs(vl.StartCoords.Y - vl.EndCoords.Y)) // diagonal
                {
                    filteredVentLines.Add(vl);
                }
            }

            return filteredVentLines;
        }

        private void ClearConsole()
        {
            Console.Clear();
        }
    }
}
