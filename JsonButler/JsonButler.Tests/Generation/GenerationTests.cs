using System;
using Andeart.JsonButler.CodeGeneration;
using Andeart.JsonButler.Tests.Properties;
using Andeart.JsonButler.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace Andeart.JsonButler.Tests.Generation
{

    [TestClass]
    public class GenerationTests
    {
        [TestMethod]
        public void GenerateCodeFile_ComplexData_CodeGenerated ()
        {
            string input = Resources.ButlerJson0;
            string generatedCsCode = ButlerCodeGenerator.GenerateCodeFile (input);
            string expected = Resources.ButlerCs0;

            Tuple<string, string> diff = TestUtilities.PeekAtFirstDiff (expected, generatedCsCode);
            Assert.AreEqual (expected, generatedCsCode, $"\n{diff.Item1}\n{diff.Item2}");
        }
    }

}