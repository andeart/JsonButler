using System;
using Andeart.JsonButler.CodeGeneration.Classes;
using Andeart.JsonButler.CodeGeneration.Namespaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;



namespace Andeart.JsonButler.CodeGeneration.Core
{

    public class ButlerCode
    {
        public string Namespace { get; set; }

        public string ClassName { get; set; }

        public string SourceJson { get; set; }

        internal ButlerCode ()
        {
            Namespace = "JsonButler.Payloads";
            ClassName = "MyPayload";
            SourceJson = string.Empty;
        }

        public string Generate ()
        {
            if (string.IsNullOrEmpty (SourceJson))
            {
                throw new InvalidOperationException ("Source JSON for code generation cannot be empty.");
            }

            CompilationUnitSyntax compileUnit = SyntaxFactory.CompilationUnit ();
            compileUnit = compileUnit.AddUsings (SyntaxFactory.UsingDirective (SyntaxFactory.ParseName ("System")));
            compileUnit = compileUnit.AddUsings (SyntaxFactory.UsingDirective (SyntaxFactory.ParseName ("Newtonsoft.Json")));

            ButlerNamespace bNamespace = ButlerNamespaceFactory.Create (Namespace);

            JToken jToken = JToken.Parse (SourceJson);
            if (jToken == null)
            {
                throw new InvalidOperationException ("Source JSON is not valid JSON.");
            }

            ButlerClass bClass = ButlerClassFactory.Create (ClassName, ClassName, jToken);

            // Add the class to the namespace.
            bNamespace.AddClass (bClass);

            // Add created classes (dependencies) to the namespace.
            bNamespace.AddClasses (bClass.Dependencies);

            compileUnit = compileUnit.AddMembers (bNamespace.Info);
            compileUnit = compileUnit.NormalizeWhitespace ();

            string code = compileUnit.ToFullString ();
            return code;
        }
    }

}