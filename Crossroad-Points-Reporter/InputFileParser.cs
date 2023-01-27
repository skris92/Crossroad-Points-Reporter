namespace Crossroad_Points_Reporter
{
    public class InputFileParser
    {
        private List<VentLine> _ventLines = new();
        private List<string> _lines = new();

        public List<VentLine> GetVentLines()
        {
            string? path = GetFilePath();
            while (path == null || path == "" || CheckFileExtension(path) == false)
            {
                ClearConsole();
                Console.WriteLine("Invalid path or file extension!");
                path = GetFilePath();
            }
            ClearConsole();

            string[] inputFileLines = GetInputFileLines(path);

            if (!CheckLinesFormat(inputFileLines))
            {
                Console.WriteLine("Input data is not in correct format!");
                GetVentLines();
            }

            _lines = inputFileLines.ToList();


            return _ventLines;
        }

        private string? GetFilePath()
        {
            Console.WriteLine("Enter file path: ");

            return Console.ReadLine();
        }

        private bool CheckFileExtension(string path)
        {
            if (Path.GetExtension(path) != ".txt") return false;
            return true;
        }

        private string[] GetInputFileLines(string path)
        {
            return File.ReadAllLines(path);
        }

        private bool CheckLinesFormat(string[] lines)
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
                        !endCoords[1].All(char.IsDigit)) return false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Input data is not in correct format!");
                    GetVentLines();
                }
            }

            return true;
        }

        private bool CheckLineDirection(string line)
        {
            return false;
        }

        private void ClearConsole()
        {
            Console.Clear();
        }
    }
}
