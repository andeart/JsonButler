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
            Parser.Default.ParseArguments<GenerateOptions> (args).WithParsed (GenerationParser.ExecuteOptionsAndReturnExitCode).WithNotParsed (HandleParseError);
        }

        private static void HandleParseError (IEnumerable<Error> errors)
        {
            // TODO: Handle errors that require further guidance.
        }
    }

}