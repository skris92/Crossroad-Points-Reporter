using InputFileParserClassLibrary;

namespace Crossroad_Points_Reporter
{
    public class DiagramCreator
    {
        private int[][] _area;
        private List<int[]> _crossroadPoints = new();

        public int[][] CreateDiagram(List<VentLine> ventLines)
        {
            _area = new int[3][];
            return _area;
        }

        private Dictionary<string, int> CalculateAreaSize(List<VentLine> ventLines)
        {
            Dictionary<string, int> areaSize = new Dictionary<string, int>();

            int maxWidth = 0;
            int maxHeight = 0;

            foreach (VentLine line in ventLines)
            {
                if (maxWidth < line.StartCoords.X) maxWidth = line.StartCoords.X;
                if (maxWidth < line.EndCoords.X) maxWidth = line.EndCoords.X;

                if (maxHeight < line.StartCoords.Y) maxHeight = line.StartCoords.Y;
                if (maxHeight < line.EndCoords.Y) maxHeight = line.EndCoords.Y;
            }

            areaSize.Add("maxWidth", maxWidth);
            areaSize.Add("maxHeight", maxHeight);

            return areaSize;
        }
    }
}
