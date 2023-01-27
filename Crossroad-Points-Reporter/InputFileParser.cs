namespace Crossroad_Points_Reporter
{
    public class InputFileParser
    {
        private List<VentLine> _ventLines = new();
        private List<string> _filteredInputLines = new();

        public List<VentLine> GetVentLines()
        {
            bool correctLinesFormat = false;
            List<string> inputFileLines = new();

            while (!correctLinesFormat)
            {
                string path = GetFilePath();

                inputFileLines = GetInputFileLines(path);

                correctLinesFormat = CheckLinesFormat(inputFileLines);
            }

            FilterLinesByDirection(inputFileLines);

            _ventLines = ConvertLinesToVentLines(_filteredInputLines);

            foreach (VentLine ventLine in _ventLines)
            {
                Console.WriteLine($"{ventLine.StartCoords.X},{ventLine.StartCoords.Y} -> {ventLine.EndCoords.X},{ventLine.EndCoords.Y}");
            }

            return _ventLines;
        }

        private string GetFilePath()
        {
            Console.WriteLine("Enter file path: ");
            string? path = Console.ReadLine();

            while (path == null || path == "" || CheckFileExtension(path) == false)
            {
                ClearConsole();
                Console.WriteLine("Invalid path or file extension!\nEnter file path: ");
                path = Console.ReadLine();
            }
            ClearConsole();

            return path;
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

        private bool CheckLinesFormat(List<string> lines)
        {
            foreach (string line in lines)
            {
                try
                {
                    string[] startCoords = line.Split(" -> ")[0].Split(",");
                    string[] endCoords = line.Split(" -> ")[1].Split(",");

                    if (!startCoords[0].All(char.IsDigit) ||
                        !startCoords[1].All(char.IsDigit) ||
                        !endCoords[0].All(char.IsDigit) ||
                        !endCoords[1].All(char.IsDigit))
                    {
                        Console.WriteLine("Input data is not in correct format!");
                        return false;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Input data is not in correct format!");
                    return false;
                }
            }

            return true;
        }

        private bool CheckLineDirection(string line)
        {
            int startCoordX = int.Parse(line.Split(" -> ")[0].Split(',')[0]);
            int startCoordY = int.Parse(line.Split(" -> ")[0].Split(',')[1]);

            int endCoordX = int.Parse(line.Split(" -> ")[1].Split(',')[0]);
            int endCoordY = int.Parse(line.Split(" -> ")[1].Split(',')[1]);

            if (startCoordX == endCoordX && startCoordY != endCoordY || // vertical line
                startCoordY == endCoordY && startCoordX != endCoordX || // horizontal line
                Math.Abs(startCoordX - endCoordX) == Math.Abs(startCoordY - endCoordY)) // diagonal line
            {
                return true;
            }
            return false;
        }

        private void FilterLinesByDirection(List<string> inputFileLines)
        {
            foreach (string line in inputFileLines)
            {
                if (CheckLineDirection(line)) _filteredInputLines.Add(line);
            }
        }

        private List<VentLine> ConvertLinesToVentLines(List<string> filteredInputLines)
        {
            List<VentLine> outputVentLines = new List<VentLine>();

            foreach (string line in filteredInputLines)
            {
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

        private void ClearConsole()
        {
            Console.Clear();
        }
    }
}
