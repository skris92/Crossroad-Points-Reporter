namespace Crossroad_Points_Reporter
{
    public static class ResultExporter
    {
        public static bool UserWantsToExport()
        {
            string chosenOption = "";

            while (chosenOption != "E" && chosenOption != "e" && chosenOption != "Q" && chosenOption != "q")
            {
                Console.WriteLine("Enter \"E\" to export results or \"Q\" to quit");
                chosenOption = Console.ReadLine();
            }
            if (chosenOption == "E" || chosenOption == "e") return true;
            return false;
        }

        public static void Export()
        {
            string fileName = GetFileName();


        }

        private static string GetFileName()
        {
            string fileName = "";

            while (fileName != "" || fileName != null)
            {
                Console.WriteLine("Enter file name: ");
                fileName = Console.ReadLine();
            }
            return fileName;
        }
    }
}
