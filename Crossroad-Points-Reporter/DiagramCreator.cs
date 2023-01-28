using InputFileParserClassLibrary;

namespace Crossroad_Points_Reporter
{
    public static class DiagramCreator
    {
        public static Diagram CreateDiagram()
        {
            List<VentLine> ventLines = InputFileParser.GetVentLines();

            Diagram diagram = InitializeDiagram(ventLines);

            DrawVentLines(diagram, ventLines);

            return diagram;
        }

        private static Dictionary<string, int> CalculateAreaSize(List<VentLine> ventLines)
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

        private static Diagram InitializeDiagram(List<VentLine> ventLines)
        {
            Dictionary<string, int> area = CalculateAreaSize(ventLines);

            // initializing _area size
            return new Diagram(new int[area["Height"], area["Width"]]);
        }

        private static void DrawVentLines(Diagram diagram, List<VentLine> ventLines)
        {
            foreach (VentLine ventLine in ventLines)
            {
                // vertical line
                if (ventLine.StartCoords.X == ventLine.EndCoords.X)
                {
                    DrawVerticalVentLine(ventLine, diagram);
                }

                // horizontal line
                else if (ventLine.StartCoords.Y == ventLine.EndCoords.Y)
                {
                    DrawHorizontalVentLine(ventLine, diagram);
                }

                // diagonal line
                else
                {
                    DrawDiagonalVentLine(ventLine, diagram);
                }
            }
        }

        private static void DrawVerticalVentLine(VentLine ventLine, Diagram diagram)
        {
            if (ventLine.StartCoords.Y < ventLine.EndCoords.Y)
            {
                for (int i = ventLine.StartCoords.Y; i <= ventLine.EndCoords.Y; i++)
                {
                    SetVentPoint(ventLine.StartCoords.X, i, diagram);
                }
            }
            else if (ventLine.StartCoords.Y > ventLine.EndCoords.Y)
            {
                for (int i = ventLine.StartCoords.Y; i >= ventLine.EndCoords.Y; i--)
                {
                    SetVentPoint(ventLine.StartCoords.X, i, diagram);
                }
            }
        }

        private static void DrawHorizontalVentLine(VentLine ventLine, Diagram diagram)
        {
            if (ventLine.StartCoords.X < ventLine.EndCoords.X)
            {
                for (int i = ventLine.StartCoords.X; i <= ventLine.EndCoords.X; i++)
                {
                    SetVentPoint(i, ventLine.StartCoords.Y, diagram);
                }
            }
            else if (ventLine.StartCoords.X > ventLine.EndCoords.X)
            {
                for (int i = ventLine.StartCoords.X; i >= ventLine.EndCoords.X; i--)
                {
                    SetVentPoint(i, ventLine.StartCoords.Y, diagram);
                }
            }
        }

        private static void DrawDiagonalVentLine(VentLine ventLine, Diagram diagram)
        {
            int ventLineLength = Math.Abs(ventLine.StartCoords.X - ventLine.EndCoords.X) + 1;
            int currentX = ventLine.StartCoords.X;
            int currentY = ventLine.StartCoords.Y;

            for (int i = 0; i < ventLineLength; i++)
            {
                if (ventLine.StartCoords.X > ventLine.EndCoords.X &&
                    ventLine.StartCoords.Y < ventLine.EndCoords.Y)
                {
                    SetVentPoint(currentX--, currentY++, diagram);
                }
                else if (ventLine.StartCoords.X < ventLine.EndCoords.X &&
                         ventLine.StartCoords.Y > ventLine.EndCoords.Y)
                {
                    SetVentPoint(currentX++, currentY--, diagram);
                }
                else if (ventLine.StartCoords.X > ventLine.EndCoords.X &&
                         ventLine.StartCoords.Y > ventLine.EndCoords.Y)
                {
                    SetVentPoint(currentX--, currentY--, diagram);
                }
                else if (ventLine.StartCoords.X < ventLine.EndCoords.X &&
                         ventLine.StartCoords.Y < ventLine.EndCoords.Y)
                {
                    SetVentPoint(currentX++, currentY++, diagram);
                }
            }
        }

        private static void SetVentPoint(int coordX, int coordY, Diagram diagram)
        {
            diagram.SetAreaPoint(coordX, coordY);

            // Registering crossroad points
            if (diagram.Area[coordY, coordX] > 1)
            {
                RegisterCrossroadPoint(coordX, coordY, diagram);
            }
        }

        private static void RegisterCrossroadPoint(int coordX, int coordY, Diagram diagram)
        {
            if (diagram.CrossroadPoints.ContainsKey($"({coordX}, {coordY})"))
            {
                diagram.UpdateCrossroadPoint(coordX, coordY);
                return;
            }
            diagram.SetCrossroadPoint(coordX, coordY);
        }
    }
}
