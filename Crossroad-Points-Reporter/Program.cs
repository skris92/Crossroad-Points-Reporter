// See https://aka.ms/new-console-template for more information
using InputFileParserClassLibrary;
using Crossroad_Points_Reporter;

List<VentLine> ventLines = InputFileParser.GetVentLines();

Diagram diagram = DiagramCreator.CreateDiagram(ventLines);

diagram.Display();

diagram.DisplayCrossroadPoints();
