// See https://aka.ms/new-console-template for more information
using Crossroad_Points_Reporter;
using InputFileParserDLL;

// Validating input format and converting data to VentLine objects
List<VentLine> ventLines = InputFileParser.GetVentLines();

// Creating Diagram object from VentLine objects
Diagram diagram = DiagramCreator.CreateDiagram(ventLines);

// Visualizing diagram
//Display.Diagram(diagram.Area); -- I misunderstood the task, it wasn't a requirement

// Visualizing result
string result = diagram.GetCrossroadPointsReport();
Display.Result(result);

// Exporting result
if (ResultExporter.UserWantsToExport())
{
    ResultExporter.Export(InputFileParser.DirectoryPath, result);
}
