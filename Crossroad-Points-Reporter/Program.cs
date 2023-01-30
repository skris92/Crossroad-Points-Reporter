using Crossroad_Points_Reporter;
using InputFileParserDLL;

// Program runs until any of the user inputs is "Q" or "q"
while (true)
{
    // Getting raw data after validating input format
    List<string> rawData = InputFileParser.GetRawData();

    // Converting raw data to relevant VentLine objects
    List<VentLine> ventLines = RawDataConverter.ConvertRawDataToVentLines(rawData);

    // Creating Diagram object from VentLine objects
    Diagram diagram = DiagramCreator.CreateDiagram(ventLines);

    // Visualizing diagram
    //Display.Diagram(diagram.Area); // Maybe I misunderstood the task, I assume it wasn't a requirement

    // Visualizing result
    string result = diagram.GetCrossroadPointsReport();
    Display.Result(result);

    // Exporting result to the same directory as the input file
    if (ResultExporter.UserWantsToExport() && InputFileParser.DirectoryPath != null)
    {
        ResultExporter.Export(InputFileParser.DirectoryPath, result);
    }
}
