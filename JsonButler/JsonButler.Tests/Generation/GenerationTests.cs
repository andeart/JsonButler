using Andeart.JsonButler.CodeGeneration.Core;
using Andeart.JsonButler.Tests.Properties;
using Andeart.JsonButler.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace Andeart.JsonButler.Tests.Generation
{

    [TestClass]
    public class GenerationTests
    {
        // TODO: The following isn't really a unit test. Will update with better tests soon. -AD
        [TestMethod]
        public void GenerateCodeFile_ComplexData_CodeGenerated ()
        {
            // Valid JSON text. This can be from a file, or from a database/server response.
            string input = Resources.ButlerJson0;

            // Generates C# code (i.e. contents of potential C# file).
            ButlerCode bCode = ButlerCodeFactory.Create ();
            bCode.Namespace = "JsonButler.Tests.Results";
            bCode.ClassName = "ComplexDataPayload";
            bCode.SourceJson = input;
            string generatedCsCode = bCode.Generate (); // Generate
            Console.WriteLine (generatedCsCode);
            string expected = Resources.ButlerCs0;

            Tuple<string, string> diff = TestUtilities.PeekAtFirstDiff (expected, generatedCsCode);
            Assert.AreEqual (expected, generatedCsCode, $"\n{diff.Item1}\n{diff.Item2}");
        }
    }

}