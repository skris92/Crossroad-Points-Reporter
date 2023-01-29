// See https://aka.ms/new-console-template for more information
using Crossroad_Points_Reporter;

Diagram diagram = DiagramCreator.CreateDiagram();

Display.Area(diagram.Area);

Display.CrossroadPoints(diagram.CrossroadPoints);
