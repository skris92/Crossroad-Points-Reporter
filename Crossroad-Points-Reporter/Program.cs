// See https://aka.ms/new-console-template for more information
using Crossroad_Points_Reporter;
using InputFileParserDLL;

List<VentLine> ventLines = InputFileParser.GetVentLines();

Diagram diagram = DiagramCreator.CreateDiagram(ventLines);

Display.Diagram(diagram.Area);

string result = diagram.GetCrossroadPointsReport();

Display.Result(result);

if (ResultExporter.UserWantsToExport())
{
    ResultExporter.Export(InputFileParser.DirectoryPath, result);
}
