using System.Collections.Generic;
using Andeart.JsonButler.CodeGeneration.Core;
using Andeart.JsonButler.CodeGeneration.Methods;
using Andeart.JsonButler.CodeGeneration.Properties;
using Andeart.JsonButler.Utilities;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;



namespace Andeart.JsonButler.CodeGeneration.Classes
{

    internal class ButlerClass
    {
        private ButlerAccessibility _accessibility;

        public string Name { get; }

        public string Path { get; }

        public List<ButlerClass> Dependencies { get; }

        public List<ButlerProperty> Properties { get; }

        public ClassDeclarationSyntax Info { get; private set; }

        public ButlerClass (string name, string path)
        {
            Name = name;
            Path = path;
            Info = SyntaxFactory.ClassDeclaration (name);
            Dependencies = new List<ButlerClass> ();
            Properties = new List<ButlerProperty> ();
        }

        public void SetAccessibility (ButlerAccessibility accessibility)
        {
            _accessibility = accessibility;
            Info = Info.AddModifiers (SyntaxFactory.Token (RoslynUtilities.FromAccessibility (accessibility)));
        }

        public void AddProperty (ButlerProperty bProperty)
        {
            Properties.Add (bProperty);
            Info = Info.AddMembers (bProperty.Info);
            AddDependencyRange (bProperty.Dependencies);
        }

        public void AddConstructor (ButlerConstructor bConstructor)
        {
            Info = Info.AddMembers (bConstructor.Info);
        }

        private void AddDependency (ButlerClass bClass)
        {
            Dependencies.Add (bClass);
        }

        private void AddDependencyRange (IEnumerable<ButlerClass> bClasses)
        {
            Dependencies.AddRange (bClasses);
        }
    }

}