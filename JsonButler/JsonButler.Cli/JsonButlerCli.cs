using System;
using System.Collections.Generic;
using Andeart.JsonButler.Cli.CodeGeneration;
using CommandLine;



namespace Andeart.JsonButler.Cli
{

    public class JsonButlerCli
    {
        // Uses Clipboard
        [STAThread]
        private static void Main (string[] args)
        {
            GenerationParser generationParser = new GenerationParser ();

            ParserResult<GenerationOptions> parserResult = Parser.Default.ParseArguments<GenerationOptions> (args);
            parserResult = parserResult.WithParsed (generationParser.ExecuteOptions);
            parserResult = parserResult.WithNotParsed (HandleParseError);
        }

        private static void HandleParseError (IEnumerable<Error> errors)
        {
            // TODO: Handle errors that require further guidance.
        }
    }

}