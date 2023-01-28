using InputFileParserClassLibrary;

namespace Crossroad_Points_Reporter
{
    public class DiagramCreator
    {
        private int[,] _area;
        private Dictionary<int[,], int> _crossroadPoints = new();

        public int[,] CreateDiagram(List<VentLine> ventLines)
        {
            InitializeDiagram(ventLines);

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

            areaSize.Add("Width", maxWidth);
            areaSize.Add("Height", maxHeight);

            return areaSize;
        }

        private void InitializeDiagram(List<VentLine> ventLines)
        {
            Dictionary<string, int> area = CalculateAreaSize(ventLines);

            _area = new int[area["Height"], area["Width"]];
        }
    }
}
