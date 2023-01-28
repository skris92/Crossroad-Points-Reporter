using InputFileParserClassLibrary;

namespace Crossroad_Points_Reporter
{
    public class DiagramCreator
    {
        private int[,] _area;
        private SortedDictionary<string, int> _crossroadPoints = new();

        public int[,] CreateDiagram(List<VentLine> ventLines)
        {
            InitializeDiagram(ventLines);

            foreach (var line in ventLines)
            {
                DrawVentLines(line);
            }

            return _area;
        }

        public SortedDictionary<string, int> GetCrossroadPoints()
        {
            return _crossroadPoints;
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

            // initializing _area size
            _area = new int[area["Height"], area["Width"]];
        }

        private void DrawVentLines(VentLine ventLine)
        {
            // vertical line
            if (ventLine.StartCoords.X == ventLine.EndCoords.X)
            {
                DrawVerticalVentLine(ventLine);
            }

            // horizontal line
            else if (ventLine.StartCoords.Y == ventLine.EndCoords.Y)
            {
                DrawHorizontalVentLine(ventLine);
            }

            // diagonal line
            else
            {
                DrawDiagonalVentLine(ventLine);
            }
        }

        private void DrawVerticalVentLine(VentLine ventLine)
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

        private void DrawHorizontalVentLine(VentLine ventLine)
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

        private void DrawDiagonalVentLine(VentLine ventLine)
        {
            int ventLineLength = Math.Abs(ventLine.StartCoords.X - ventLine.EndCoords.X) + 1;
            int currentX = ventLine.StartCoords.X;
            int currentY = ventLine.StartCoords.Y;

            for (int i = 0; i < ventLineLength; i++)
            {
                if (ventLine.StartCoords.X > ventLine.EndCoords.X &&
                    ventLine.StartCoords.Y < ventLine.EndCoords.Y)
                {
                    SetVentPoint(currentX--, currentY++);
                }
                else if (ventLine.StartCoords.X < ventLine.EndCoords.X &&
                         ventLine.StartCoords.Y > ventLine.EndCoords.Y)
                {
                    SetVentPoint(currentX++, currentY--);
                }
                else if (ventLine.StartCoords.X > ventLine.EndCoords.X &&
                         ventLine.StartCoords.Y > ventLine.EndCoords.Y)
                {
                    SetVentPoint(currentX--, currentY--);
                }
                else if (ventLine.StartCoords.X < ventLine.EndCoords.X &&
                         ventLine.StartCoords.Y < ventLine.EndCoords.Y)
                {
                    SetVentPoint(currentX++, currentY++);
                }
            }
        }

        private void SetVentPoint(int coordX, int coordY)
        {
            _area[coordY, coordX]++;

            // Registering crossroad points
            if (_area[coordY, coordX] > 1)
            {
                if (_crossroadPoints.ContainsKey($"({coordX}, {coordY})")) 
                {
                    _crossroadPoints[$"({coordX}, {coordY})"]++;
                    return;
                }
                _crossroadPoints.Add($"({coordX}, {coordY})", _area[coordY, coordX]);
            }
        }
    }
}
