﻿namespace Crossroad_Points_Reporter
{
    public static class ResultExporter
    {
        public static bool UserWantsToExport()
        {
            string? chosenOption = "";

            while (chosenOption != "E" && chosenOption != "e" && chosenOption != "Q" && chosenOption != "q")
            {
                Console.WriteLine("Enter \"E\" to export results or \"Q\" to quit");
                chosenOption = Console.ReadLine();
            }
            if (chosenOption == "E" || chosenOption == "e") return true;
            return false;
        }

        public static void Export(string directoryPath, string result)
        {
            string fileName = directoryPath + GetFileName() + GetFileExtension();

            File.WriteAllText(fileName, result, System.Text.Encoding.UTF8);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Result exported to:\n{fileName}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static string GetFileName()
        {
            string? fileName = "";

            while (fileName == "" || fileName == null)
            {
                Console.WriteLine("Enter file name: ");
                fileName = Console.ReadLine();
            }
            return fileName;
        }

        private static string GetFileExtension()
        {
            string fileExtension = "";

            while (fileExtension == "")
            {
                Console.WriteLine("Enter file extension \"txt\" or \"ans\": ");
                string? chosenExtension = Console.ReadLine();
                if (chosenExtension == "txt" || chosenExtension == "ans") fileExtension += chosenExtension;
            }
            return "." + fileExtension;
        }
    }
}
