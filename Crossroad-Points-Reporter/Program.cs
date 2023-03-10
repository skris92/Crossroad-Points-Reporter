using Crossroad_Points_Reporter;
using InputFileParserDLL;

// Getting raw data after validating input format
List<string> rawData = await InputFileParser.GetRawData();

// Converting raw data to relevant VentLine objects
List<VentLine> ventLines = RawDataConverter.ConvertRawDataToVentLines(rawData);

// Creating Diagram object from VentLine objects
Diagram diagram = DiagramCreator.CreateDiagram(ventLines);

// Visualizing diagram
//Display.Diagram(diagram.Area); // Maybe I misunderstood the task, I assume it wasn't a requirement

// Visualizing result
string result = diagram.GetCrossroadPointsReport();
Display.Result(result);

// Exporting result
if (ResultExporter.UserWantsToExport())
{
    ResultExporter.Export(result);
}
