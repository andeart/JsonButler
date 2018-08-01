using System.Collections.Generic;
using Andeart.JsonButler.CodeGeneration.Core;
using Microsoft.CodeAnalysis.CSharp;



namespace Andeart.JsonButler.Utilities
{

    internal static class RoslynUtilities
    {
        private static readonly Dictionary<ButlerAccessibility, SyntaxKind> _accessibilitiesMap;
        private static readonly Dictionary<ButlerAccessorType, SyntaxKind> _accessorTypesMap;

        static RoslynUtilities ()
        {
            _accessibilitiesMap = new Dictionary<ButlerAccessibility, SyntaxKind>
                                  {
                                      { ButlerAccessibility.Private, SyntaxKind.PrivateKeyword },
                                      { ButlerAccessibility.Protected, SyntaxKind.ProtectedKeyword },
                                      { ButlerAccessibility.Public, SyntaxKind.PublicKeyword }
                                  };

            _accessorTypesMap = new Dictionary<ButlerAccessorType, SyntaxKind>
                                {
                                    { ButlerAccessorType.Get, SyntaxKind.GetAccessorDeclaration },
                                    { ButlerAccessorType.Set, SyntaxKind.SetAccessorDeclaration }
                                };
        }

        public static SyntaxKind FromAccessibility (ButlerAccessibility accessibility)
        {
            return _accessibilitiesMap[accessibility];
        }

        public static SyntaxKind FromAccessorType (ButlerAccessorType accessorType)
        {
            return _accessorTypesMap[accessorType];
        }
    }

}