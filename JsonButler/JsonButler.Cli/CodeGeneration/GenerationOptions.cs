using System;
using Andeart.JsonButler.Cli.Core;
using CommandLine;



namespace Andeart.JsonButler.Cli.CodeGeneration
{

    [Verb ("generate", HelpText = "Generate C# type/file from JSON data.")]
    internal class GenerationOptions : IParserOptions
    {
        [Option ('f',
            "inputfile",
            Required = true,
            HelpText = "(Optional if a -j/--inputjson argument is added). File containing the JSON to be converted to C# type.",
            SetName = "inputfile")]
        public string InputFile { get; }

        [Option ('j',
            "inputjson",
            Required = true,
            HelpText = "(Optional if an -f/--inputfile argument is added). The JSON text to be converted to C# type.",
            SetName = "jsontext")]
        public string InputJson { get; }

        [Option ('o', "output", HelpText = "Output file to write the generated type into.")]
        public string OutputFile { get; }

        public GenerationOptions (string inputFile, string inputJson, string outputFile)
        {
            InputFile = inputFile;
            InputJson = inputJson;
            OutputFile = outputFile;
            string x = DateTime.UtcNow.Year.ToString ("YY");

            string Year = DateTime.UtcNow.ToString ("YY");
            int Month = DateTime.UtcNow.Month;
            int Day = DateTime.UtcNow.Day;
            int Minute = unchecked((int) DateTime.UtcNow.TimeOfDay.TotalMinutes);
        }
    }

}