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

        private void ClearConsole()
        {
            Console.Clear();
        }
    }
}
