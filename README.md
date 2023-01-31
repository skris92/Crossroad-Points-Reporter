
# Crossroad-Points-Reporter

## Description

A console application reporting dangerous hydrothermal vent crossroad points based on input coordinates for submarines to avoid.

## How to build and run the application

- Open Crossroad-Points-Reporter\Crossroad-Points-Reporter.sln solution file with Microsoft Visual Studio.
- On the top toolbar select: Build -> Configuration Manager...
- Set Active solution configuration to "Release" in the Active solution configuration dropdown menu and close the Configuration Manager window.
- On the top toolbar select: Build -> Build solution
- Run file  Crossroad-Points-Reporter\bin\Release\net6.0\Crossroad-Points-Reporter.exe

## How to use the application

- Enter the input file path.
- Wait for the parsing process and the user should see the result on the console.
- Enter "E" for exporting.
- Enter an existing export directory.
- Enter at least one character long output file name with extensions ".txt" or ".ans".

## Bug

When not aborting the parsing process and the program asking the user if he/she wants to export the result the aborting task is still waiting for a key to be pressed.
Just hit any key and the program proceeds as intended.

## Note

If the progress bar for parsing is not visible due fast computation uncomment line 100 in InputFileParserDLL\InputFileParser.cs and rebuild the application for slowing down the process.