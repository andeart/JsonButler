using Andeart.CaseConversions;
using Andeart.JsonButler.CodeGeneration.Classes;
using Andeart.JsonButler.CodeGeneration.Properties;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;


namespace Andeart.JsonButler.CodeGeneration.Methods
{

    internal class ButlerConstructor
    {
        private readonly ButlerClass _owner;

        public ConstructorDeclarationSyntax Info { get; private set; }

        public ButlerConstructor (ButlerClass owner)
        {
            _owner = owner;

            SyntaxToken classId = SyntaxFactory.Identifier (owner.Name);
            Info = SyntaxFactory.ConstructorDeclaration (classId);
        }

        public void AddPropertyParameter (ButlerProperty bProperty)
        {
            if (!_owner.Properties.Contains (bProperty))
            {
                throw new Exception ($"Property with name {bProperty.Name} cannot be set through {_owner.Name} constructor if it hasn't been defined in class.");
            }

            string propertyName = bProperty.Name;
            string parameterName = propertyName.ToCamelCase ();

            ParameterSyntax parameterInfo = SyntaxFactory.Parameter (SyntaxFactory.Identifier (parameterName)).WithType (bProperty.Info.Type);
            Info = Info.AddParameterListParameters (parameterInfo);

            ExpressionSyntax lhs = SyntaxFactory.IdentifierName (propertyName);
            ExpressionSyntax rhs = SyntaxFactory.IdentifierName (parameterName);
            AssignmentExpressionSyntax assignmentExpression = SyntaxFactory.AssignmentExpression (SyntaxKind.SimpleAssignmentExpression, lhs, rhs);
            Info = Info.AddBodyStatements (SyntaxFactory.ExpressionStatement (assignmentExpression));
        }

        public void AddPropertyParameterRange (IEnumerable<ButlerProperty> bProperties)
        {
            foreach (ButlerProperty bProperty in bProperties)
            {
                AddPropertyParameter (bProperty);
            }
        }

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
    }

}