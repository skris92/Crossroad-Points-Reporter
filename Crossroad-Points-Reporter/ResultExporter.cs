﻿namespace Crossroad_Points_Reporter
{
    public static class ResultExporter
    {
        public static bool UserWantsToExport()
        {
            // Asking for user input until option export or quit selected
            string? chosenOption = "";

            while (chosenOption != "E" && chosenOption != "e")
            {
                Console.WriteLine("Enter \"E\" to export results or \"Q\" to quit");
                chosenOption = Console.ReadLine();
                if (chosenOption == "Q" || chosenOption == "q") Environment.Exit(0);
            }
            if (chosenOption == "E" || chosenOption == "e") return true;
            return false;
        }

        public static void Export(string directoryPath, string result)
        {
            string fileName = directoryPath + "\\" + GetFileName();

            File.WriteAllText(fileName, result, System.Text.Encoding.UTF8);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Result exported to:\n{fileName}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static string GetFileName()
        {
            // Getting file name with at least one character long and with extensions: "txt" and "ans"
            string? fileName = "";

            while (fileName.Length < 5 ||
                   fileName.Substring(fileName.Length - 4) != ".txt" &&
                   fileName.Substring(fileName.Length - 4) != ".ans")
            {
                Console.WriteLine("Enter file name with extensions \".txt\" or \".ans\": (Enter \"Q\" to quit)");
                fileName = Console.ReadLine();
                if (fileName == "Q" || fileName == "q") Environment.Exit(0);
            }
            return fileName;
        }
    }
}
