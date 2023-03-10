namespace InputFileParserDLL
{
    public static class InputFileParser
    {
        private static CancellationTokenSource cTokenSource = new CancellationTokenSource();

        public async static Task<List<string>> GetRawData()
        {
            bool correctLinesFormat = false;
            List<string> inputFileLines = new();

            // Validating user input
            while (!correctLinesFormat)
            {
                string filePath = GetFilePath();

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

                // Aborting task for CheckInputFileLinesFormatTask
                Task abortTask = Task.Run(() =>
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.A)
                    {
                        Console.WriteLine("\nParsing aborted!\n");
                        cTokenSource.Cancel();
                    }
                });

                Task CheckInputFileLinesFormatTask = Task.Run(() =>
                {
                    correctLinesFormat = CheckInputFileLinesFormat(inputFileLines);
                });

                await Task.WhenAny(new[] { abortTask, CheckInputFileLinesFormatTask });
            }

            return inputFileLines;
        }

        private static string GetFilePath()
        {
            // Asking for user input something with correct extension entered
            while (true)
            {
                Console.WriteLine("Enter input file path: (Enter \"Q\" to quit)");
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
            for (int i = 0; i < lines.Count; i++)
            {
                if (cTokenSource.IsCancellationRequested)
                {
                    // Resetting CancellationTokenSource for letting the user to add a new input file
                    cTokenSource = new CancellationTokenSource();
                    return false;
                }
                ShowProgressBar("Parsing input data (Press \"A\" to abort)", i, lines.Count);
                //Thread.Sleep(10); // Slowing down the process for progress bar visibility
                // Checking input file format before any conversions
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
            int progressBarLength = 50; // Adjust this value to manipulate bar length
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
