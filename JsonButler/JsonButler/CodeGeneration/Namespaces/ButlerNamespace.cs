using System.Collections.Generic;
using Andeart.JsonButler.CodeGeneration.Classes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;



namespace Andeart.JsonButler.CodeGeneration.Namespaces
{

    internal class ButlerNamespace
    {
        private readonly List<ButlerClass> _classes;

        public string Name { get; }

        public NamespaceDeclarationSyntax Info { get; private set; }

        public ButlerNamespace (string name)
        {
            Name = name;
            Info = SyntaxFactory.NamespaceDeclaration (SyntaxFactory.ParseName (name));
            _classes = new List<ButlerClass> ();
        }

        public void AddClass (ButlerClass bClass)
        {
            _classes.Add (bClass);
            Info = Info.AddMembers (bClass.Info);
        }

        public void AddClasses (IEnumerable<ButlerClass> bClasses)
        {
            _classes.AddRange (bClasses);
            foreach (ButlerClass bClass in bClasses)
            {
                Info = Info.AddMembers (bClass.Info);
            }
        }
    }

}