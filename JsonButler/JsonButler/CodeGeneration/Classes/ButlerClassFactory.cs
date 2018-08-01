using Andeart.JsonButler.CodeGeneration.Core;
using Andeart.JsonButler.CodeGeneration.Methods;
using Andeart.JsonButler.CodeGeneration.Properties;
using Newtonsoft.Json.Linq;



namespace Andeart.JsonButler.CodeGeneration.Classes
{

    internal class ButlerClassFactory
    {
        public static ButlerClass Create (string className, string path, JToken jToken)
        {
            // ClassDeclarationSyntax codeClass = SyntaxFactory.ClassDeclaration (className);
            // codeClass = codeClass.AddModifiers (SyntaxFactory.Token (SyntaxKind.PublicKeyword));

            // SyntaxToken classId = SyntaxFactory.Identifier (className);
            // ConstructorDeclarationSyntax constructorDeclaration = SyntaxFactory.ConstructorDeclaration (classId);

            // List<ButlerClass> dependencies = new List<ButlerClass> ();

            ButlerClass bClass = new ButlerClass (className, path);

            foreach (JToken childToken in jToken.Children ())
            {
                // Create property.
                ButlerProperty bProperty = ButlerPropertyFactory.Create (childToken);
                bProperty.SetAccessibility (ButlerAccessibility.Public);
                bProperty.AddAccessor (ButlerAccessorType.Get, ButlerAccessibility.Public);
                bProperty.AddAccessor (ButlerAccessorType.Set, ButlerAccessibility.Private);
                bProperty.AddAttribute ("JsonProperty", $"\"{bProperty.Id}\"");

                // codeClass = codeClass.AddMembers (property.Info);
                // dependencies.AddRange (property.Dependencies);

                // Create constructor argument and assignment.
                // string argumentName = property.Name.ToCamelCase ();
                // ParameterSyntax x = SyntaxFactory.Parameter (SyntaxFactory.Identifier (argumentName)).WithType (property.Info.Type);
                // constructorDeclaration = constructorDeclaration.AddParameterListParameters (x);

                // ExpressionSyntax left = SyntaxFactory.IdentifierName (property.Name);
                // ExpressionSyntax right = SyntaxFactory.IdentifierName (argumentName);
                // AssignmentExpressionSyntax assignmentExpression = SyntaxFactory.AssignmentExpression (SyntaxKind.SimpleAssignmentExpression, left, right);
                //
                // constructorDeclaration = constructorDeclaration.AddBodyStatements (SyntaxFactory.ExpressionStatement (assignmentExpression));


                bClass.AddProperty (bProperty);
            }

            // // Set JsonConstructor attribute on constructor
            // AttributeSyntax attribute = SyntaxFactory.Attribute (SyntaxFactory.ParseName ("JsonConstructor"));
            // AttributeListSyntax attributeList = SyntaxFactory.AttributeList (SyntaxFactory.SingletonSeparatedList (attribute));
            // constructorDeclaration = constructorDeclaration.AddAttributeLists (attributeList);
            // codeClass = codeClass.AddMembers (constructorDeclaration);

            ButlerConstructor bConstructor = new ButlerConstructor (bClass);
            bConstructor.AddPropertyParameterRange (bClass.Properties);
            bConstructor.AddAttribute ("JsonConstructor");

            bClass.AddConstructor (bConstructor);


            // ButlerClass bClass = new ButlerClass (className, path, codeClass);
            // bClass.AddDependencyRange (dependencies);
            return bClass;
        }
    }

}