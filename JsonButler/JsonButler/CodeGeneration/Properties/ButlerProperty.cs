using System;
using System.Collections.Generic;
using Andeart.JsonButler.CodeGeneration.Classes;
using Andeart.JsonButler.CodeGeneration.Core;
using Andeart.JsonButler.Utilities;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;



namespace Andeart.JsonButler.CodeGeneration.Properties
{

    internal class ButlerProperty
    {
        private readonly HashSet<ButlerAccessorType> _definedAccessors;
        private ButlerAccessibility _accessibility;

        public string Name { get; }

        public string Id { get; }

        public List<ButlerClass> Dependencies { get; }

        public PropertyDeclarationSyntax Info { get; private set; }

        public ButlerProperty (string name, string id, string type)
        {
            TypeSyntax propertyType = SyntaxFactory.ParseTypeName (type);
            Info = SyntaxFactory.PropertyDeclaration (propertyType, name);

            _accessibility = ButlerAccessibility.Private;
            _definedAccessors = new HashSet<ButlerAccessorType> ();
            Name = name;
            Id = id;
            Dependencies = new List<ButlerClass> ();
        }

        public void SetAccessibility (ButlerAccessibility accessibility)
        {
            _accessibility = accessibility;
            Info = Info.AddModifiers (SyntaxFactory.Token (RoslynUtilities.FromAccessibility (accessibility)));
        }

        public void AddAccessor (ButlerAccessorType accessorType, ButlerAccessibility accessibility)
        {
            if (accessorType == ButlerAccessorType.Set && !_definedAccessors.Contains (ButlerAccessorType.Get))
            {
                throw new Exception ("Cannot define a set accessor without a get accessor.");
            }

            AccessorDeclarationSyntax accessor = SyntaxFactory.AccessorDeclaration (RoslynUtilities.FromAccessorType (accessorType));
            accessor = accessor.WithSemicolonToken (SyntaxFactory.Token (SyntaxKind.SemicolonToken));

            if (accessibility > _accessibility)
            {
                throw new Exception ($"The accessibility modifier of the {accessorType.ToString ()} accessor must be more restrictive than the property");
            }

            if (accessibility != _accessibility)
            {
                accessor = accessor.AddModifiers (SyntaxFactory.Token (RoslynUtilities.FromAccessibility (accessibility)));
            }

            Info = Info.AddAccessorListAccessors (accessor);
            _definedAccessors.Add (accessorType);
        }

        // verbatim arguments should include double-quotes if of type string, etc.
        public void AddAttribute (string attributeName, params string[] verbatimArguments)
        {
            NameSyntax name = SyntaxFactory.ParseName (attributeName);
            AttributeSyntax attribute;
            if (verbatimArguments.Length > 0)
            {
                string argsCsv = string.Join (", ", verbatimArguments);
                AttributeArgumentListSyntax arguments = SyntaxFactory.ParseAttributeArgumentList ($"({argsCsv})");
                attribute = SyntaxFactory.Attribute (name, arguments);
            } else
            {
                attribute = SyntaxFactory.Attribute (name);
            }

            AttributeListSyntax attributeList = SyntaxFactory.AttributeList (SyntaxFactory.SingletonSeparatedList (attribute));
            Info = Info.AddAttributeLists (attributeList);
        }

        public void AddDependency (ButlerClass bClass)
        {
            Dependencies.Add (bClass);
        }

        public void AddDependencyRange (IEnumerable<ButlerClass> bClasses)
        {
            Dependencies.AddRange (bClasses);
        }
    }

}