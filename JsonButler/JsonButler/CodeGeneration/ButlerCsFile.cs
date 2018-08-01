using System.Windows.Forms;
using Andeart.JsonButler.CodeGeneration.Classes;
using Andeart.JsonButler.CodeGeneration.Namespaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;



namespace Andeart.JsonButler.CodeGeneration
{

    public class ButlerCsFile
    {
        public void CreateCode (string jsonText)
        {
            CompilationUnitSyntax compileUnit = SyntaxFactory.CompilationUnit ();
            compileUnit = compileUnit.AddUsings (SyntaxFactory.UsingDirective (SyntaxFactory.ParseName ("System")));
            compileUnit = compileUnit.AddUsings (SyntaxFactory.UsingDirective (SyntaxFactory.ParseName ("Newtonsoft.Json")));

            //NamespaceDeclarationSyntax codeNamespace = SyntaxFactory.NamespaceDeclaration (SyntaxFactory.ParseName ("JsonButler.Creations"));
            ButlerNamespace bNamespace = ButlerNamespaceFactory.Create ("JsonButler.Creations");

            JToken jToken = JToken.Parse (jsonText);
            ButlerClass bClass = ButlerClassFactory.Create ("ButlerFoo", "ButlerFoo", jToken);

            // Add the class to the namespace.
            bNamespace.AddClass (bClass);

            // Add created classes (dependencies) to the namespace.
            bNamespace.AddClasses (bClass.Dependencies);

            compileUnit = compileUnit.AddMembers (bNamespace.Info);
            compileUnit = compileUnit.NormalizeWhitespace ();

            string code = compileUnit.ToFullString ();

            Clipboard.SetText (code);
        }
    }

}