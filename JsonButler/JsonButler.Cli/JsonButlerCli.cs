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
            foreach (Error error in errors)
            {
                HandleParseError (error);
            }
        }

        private static void HandleParseError (Error error)
        {
            if (error is VersionRequestedError || error is HelpRequestedError)
            {
                return;
            }

            Console.WriteLine ($"\nERROR: {error}\n");
        }
    }

}