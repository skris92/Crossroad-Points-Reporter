namespace Crossroad_Points_Reporter
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

        public static void Export(string result)
        {
            bool askingFileName = true;

            while (askingFileName)
            {
                string fileName = GetDirectoryPath() + GetFileName();

                if (File.Exists(fileName))
                {
                    if (UserWantsToOverride())
                    {
                        WriteTextToFile(fileName, result);
                        askingFileName = false;
                    }
                }
                else
                {
                    WriteTextToFile(fileName, result);
                    askingFileName = false;
                }
            }
        }

        private static bool UserWantsToOverride()
        {
            // Asking for user input until option yes, no or quit selected
            string? chosenOption = "";

            while (chosenOption != "Y" && chosenOption != "y" && chosenOption != "N" && chosenOption != "n")
            {
                Console.WriteLine("File already exists!\nDo you want to override? " +
                                  "(\"Y\"\\\"N\") (Enter \"Q\" to quit)");
                chosenOption = Console.ReadLine();
                if (chosenOption == "Q" || chosenOption == "q") Environment.Exit(0);
            }
            if (chosenOption == "Y" || chosenOption == "y") return true;

            return false;
        }

        private static void WriteTextToFile(string fileName, string result)
        {
            File.WriteAllText(fileName, result, System.Text.Encoding.UTF8);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Result exported to:\n{fileName}\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }

        private static string GetDirectoryPath()
        {
            // Getting existing directory path
            string? filePath = "";
            bool existingDirectory = false;

            while (filePath == null || !existingDirectory)
            {
                Console.WriteLine("Enter export directory path: (Enter \"Q\" to quit)");
                filePath = Console.ReadLine();
                if (filePath == "Q" || filePath == "q") Environment.Exit(0);
                else if (filePath == "" || filePath == null) continue;
                else if (!Directory.Exists(filePath))
                {
                    Console.WriteLine("Directory not found!");
                }
                else existingDirectory = true;
            }

            // Adding a "\" at the end of the path if it is not already there
            filePath += filePath[filePath.Length - 1] == '\\' ? "" : "\\";

            return filePath;
        }

        private static string GetFileName()
        {
            // Getting file name with at least one character long and with extensions: "txt" and "ans"
            string fileName = "";

            while (fileName == null || fileName.Length < 5 ||
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
