namespace InputFileParserDLL
{
    public static class InputFileParser
    {
        public static string DirectoryPath { get; private set; } // Used for future exporting
        public static List<VentLine> GetVentLines()
        {
            bool correctLinesFormat = false;
            List<string> inputFileLines = new();

            // Validating user input
            while (!correctLinesFormat)
            {
                string filePath = GetFilePath();
                DirectoryPath = Path.GetDirectoryName(filePath) + "\\";

                try
                {
                    inputFileLines = GetInputFileLines(filePath);
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Directory not found!");
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

        private static string GetFilePath()
        {
            // Validating user input and file extension
            while (true)
            {
                Console.WriteLine("Enter file path: (Enter \"Q\" to quit)");
                string? input = Console.ReadLine();

                if (input == null || input == "")
                {
                    ClearConsole();
                    continue;
                }
                else if (input == "q" || input == "Q") Environment.Exit(0);
                else if (!CheckFileExtension(input))
                {
                    ClearConsole();
                    Console.WriteLine("Invalid file extension!");
                    continue;
                }
                ClearConsole();
                return input;
            }
        }

        private static bool CheckFileExtension(string path)
        {
            if (Path.GetExtension(path) != ".txt" && Path.GetExtension(path) != ".ans") return false;
            return true;
        }

        private static List<string> GetInputFileLines(string path)
        {
            return File.ReadAllLines(path).ToList();
        }

        private static bool CheckInputFileLinesFormat(List<string> lines)
        {
            // Checking input file format before any conversions
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

        private static List<VentLine> ConvertLinesToVentLines(List<string> filteredInputLines)
        {
            // Converting data to VentLine objects
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

        private static List<VentLine> FilterVentLinesByDirection(List<VentLine> ventLines)
        {
            // Filtering vent lines by direction,
            // skipping non vertical, horizontal and diagonal lines
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

        private static void ClearConsole()
        {
            Console.Clear();
        }
    }
}
