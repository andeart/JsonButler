using Andeart.JsonButler.CodeGeneration;
using Andeart.JsonButler.Tests.Properties;
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

            Assert.AreEqual (expected, generatedCsCode, "Generated code was not correct.");
        }
    }

}