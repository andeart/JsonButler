﻿using Andeart.CaseConversions;
using Andeart.JsonButler.CodeGeneration.Core;
using Andeart.JsonButler.IO;


namespace Andeart.JsonButler.Cli.CodeGeneration
{

    internal class GenerationUtility
    {
        public static void Generate (string sourceJson)
        {
            ButlerCode bCode = ButlerCodeFactory.Create ();
            bCode.Namespace = "JsonButler.Cli.Payloads";
            bCode.ClassName = "MyPayload";
            bCode.SourceJson = sourceJson;
            string generatedCode = bCode.Generate ();
            ButlerWriterService.SetClipboardText (generatedCode);
        }

        public static void Generate (string sourceJson, string outputFile)
        {
            ButlerCode bCode = ButlerCodeFactory.Create ();

            string[] filePathSegments = outputFile.Split ('/');
            string fullFileName = filePathSegments[filePathSegments.Length - 1];
            string[] fileNameSegments = fullFileName.Split ('.');
            string shortFileName = fileNameSegments[0];

            bCode.Namespace = "JsonButler.Cli.Payloads";
            bCode.ClassName = shortFileName.ToPascalCase ();
            bCode.SourceJson = sourceJson;
            string generatedCode = bCode.Generate ();
            ButlerWriterService.WriteAllText (outputFile, generatedCode);
        }
    }

}