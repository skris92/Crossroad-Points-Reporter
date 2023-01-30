namespace Crossroad_Points_Reporter
{
    public static class RawDataConverter
    {
        // Converting data to VentLine objects
        public static List<VentLine> ConvertRawDataToVentLines(List<string> rawData)
        {
            List<VentLine> outputVentLines = new List<VentLine>();

            foreach (string line in rawData)
            {
                // Converting string coordinates to integers
                int startCoordX = int.Parse(line.Split(" -> ")[0].Split(',')[0]);
                int startCoordY = int.Parse(line.Split(" -> ")[0].Split(',')[1]);

                int endCoordX = int.Parse(line.Split(" -> ")[1].Split(',')[0]);
                int endCoordY = int.Parse(line.Split(" -> ")[1].Split(',')[1]);

                Coords startCoords = new(startCoordX, startCoordY);
                Coords endCoords = new(endCoordX, endCoordY);

                VentLine ventLine = new(startCoords, endCoords);

                outputVentLines.Add(ventLine);
            }

            outputVentLines = FilterVentLinesByDirection(outputVentLines);

            return outputVentLines;
        }

        private static List<VentLine> FilterVentLinesByDirection(List<VentLine> ventLines)
        {
            // Filtering VentLines by direction,
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
    }
}
