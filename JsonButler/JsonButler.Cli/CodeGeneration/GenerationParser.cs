using System;
using Andeart.JsonButler.IO;



namespace Andeart.JsonButler.Cli.CodeGeneration
{

    internal class GenerationParser
    {
        public static void ExecuteOptionsAndReturnExitCode (GenerateOptions options)
        {
            string inputFile = options.InputFile;
            string inputJson = options.InputJson;
            string outputFile = options.OutputFile;

            // If option is used but empty string is passed, commandlineparser does not handle it as an error.
            // So we need to check explicitly...
            if (inputFile == string.Empty)
            {
                Console.WriteLine ("\nERROR: Invalid value passed for option 'f, inputfile'.");
                return;
            }

            if (inputJson == string.Empty)
            {
                Console.WriteLine ("\nERROR: Invalid value passed for option 'j, inputjson'.");
                return;
            }

            if (outputFile == string.Empty)
            {
                Console.WriteLine ("\nERROR: Invalid value passed for option 'o, outputfile'.");
                return;
            }

            // Handle from file if inputFile argument was set.
            if (!string.IsNullOrEmpty (inputFile))
            {
                HandleFromFile (inputFile, outputFile);
                return;
            }

            // inputJson should be available at this point.
            HandleFromJson (inputJson, outputFile);
        }

        private static void HandleFromFile (string inputFile, string outputFile)
        {
            string fileContents = ButlerReader.ReadAllText (inputFile);
            HandleFromJson (fileContents, outputFile);
        }

        private static void HandleFromJson (string inputJson, string outputFile)
        {
            if (string.IsNullOrEmpty (outputFile))
            {
                Generator.Generate (inputJson);
                Console.WriteLine ("\nCode generation complete. Contents copied to clipboard.");
                return;
            }

            if (ConfirmFileWrite (outputFile))
            {
                Generator.Generate (inputJson, outputFile);
                Console.WriteLine($"\nCode generation complete. Contents written to {outputFile}");
            }
        }

        private static bool ConfirmFileWrite (string outputFile)
        {
            if (ButlerReader.Exists (outputFile))
            {
                Console.WriteLine ($"\nFile already exists at {outputFile}. Overwriting.");
                return true;
            }

            Console.WriteLine ($"\nFile at path {outputFile} does not exist.\nDo you want to create a new file (y/n)?");
            ConsoleKeyInfo input = Console.ReadKey ();
            switch (input.Key)
            {
                case ConsoleKey.Y:
                    Console.WriteLine ($"\nCreating new file at {outputFile}.");
                    return true;
                case ConsoleKey.N:
                    Console.WriteLine ("\nCode generation aborted.");
                    return false;
                default:
                    Console.WriteLine ("\nERROR: Invalid option. Generation aborted.");
                    return false;
            }
        }
    }

}