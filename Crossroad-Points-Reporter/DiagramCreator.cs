using InputFileParserClassLibrary;

namespace Crossroad_Points_Reporter
{
    public class DiagramCreator
    {
        private int[,] _area;
        private Dictionary<string, int> _crossroadPoints = new();

        public int[,] CreateDiagram(List<VentLine> ventLines)
        {
            InitializeDiagram(ventLines);

            foreach (var line in ventLines)
            {
                DrawVentLines(line);
            }

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

            areaSize.Add("Width", maxWidth + 1);   // + 1 because diagram coordinates starts from 0
            areaSize.Add("Height", maxHeight + 1);

            return areaSize;
        }

        private void InitializeDiagram(List<VentLine> ventLines)
        {
            Dictionary<string, int> area = CalculateAreaSize(ventLines);

            _area = new int[area["Height"], area["Width"]];
        }

        private void DrawVentLines(VentLine ventLine)
        {
            // vertical line
            if (ventLine.StartCoords.X == ventLine.EndCoords.X)
            {
                DrawVerticalVentLines(ventLine);
            }

            // horizontal line
            if (ventLine.StartCoords.Y == ventLine.EndCoords.Y)
            {
                DrawHorizontalVentLines(ventLine);
            }
        }

        private void DrawVerticalVentLines(VentLine ventLine)
        {
            if (ventLine.StartCoords.Y < ventLine.EndCoords.Y)
            {
                for (int i = ventLine.StartCoords.Y; i <= ventLine.EndCoords.Y; i++)
                {
                    SetVentPoint(ventLine.StartCoords.X, i);
                }
            }
            else if (ventLine.StartCoords.Y > ventLine.EndCoords.Y)
            {
                for (int i = ventLine.StartCoords.Y; i >= ventLine.EndCoords.Y; i--)
                {
                    SetVentPoint(ventLine.StartCoords.X, i);
                }
            }
        }

        private void DrawHorizontalVentLines(VentLine ventLine)
        {
            if (ventLine.StartCoords.X < ventLine.EndCoords.X)
            {
                for (int i = ventLine.StartCoords.X; i <= ventLine.EndCoords.X; i++)
                {
                    SetVentPoint(i, ventLine.StartCoords.Y);
                }
            }
            else if (ventLine.StartCoords.X > ventLine.EndCoords.X)
            {
                for (int i = ventLine.StartCoords.X; i >= ventLine.EndCoords.X; i--)
                {
                    SetVentPoint(i, ventLine.StartCoords.Y);
                }
            }
        }

        private void SetVentPoint(int coordX, int coordY)
        {
            _area[coordY, coordX]++;
            if (_area[coordY, coordX] > 1)
            {
                _crossroadPoints.Add($"({coordX}, {coordY})", _area[coordY, coordX]);
            }
        }
    }
}
