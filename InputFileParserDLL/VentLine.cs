namespace InputFileParserDLL
{
    public class VentLine
    {
        public Coords StartCoords { get; }

        public Coords EndCoords { get; }

        public VentLine(Coords startCoords, Coords endCoords)
        {
            StartCoords = startCoords;
            EndCoords = endCoords;
        }
    }
}
