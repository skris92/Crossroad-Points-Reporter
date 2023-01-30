namespace InputFileParserDLL
{
    public static class InputFileParser
    {
        public static string? DirectoryPath { get; private set; }
        public static List<string> GetRawData()
        {
            bool correctLinesFormat = false;
            List<string> inputFileLines = new();

            // Validating user input
            while (!correctLinesFormat)
            {
                string filePath = GetFilePath();
                DirectoryPath = Path.GetDirectoryName(filePath);

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

            return inputFileLines;
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
            for (int i = 0; i < lines.Count; i++)
            {
                ShowProgressBar("Parsing input data", i, lines.Count);
                //Thread.Sleep(10); // for progress bar visibility
                try
                {
                    string[] startCoords = lines[i].Split(" -> ")[0].Split(",");
                    string[] endCoords = lines[i].Split(" -> ")[1].Split(",");

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

        private static void ShowProgressBar(string message, int iteration, int length)
        {
            int progressBarLength = 50;
            float progressPercent = (iteration + 1) / (float)length * progressBarLength;

            Console.SetCursorPosition(0, 0);
            Console.Write($"{message}" +
                          $"[{new string('#', (int)progressPercent)}" +
                          $"{new string('-', progressBarLength - (int)progressPercent)}]\n");
        }

        private static void ClearConsole()
        {
            Console.Clear();
        }
    }
}
