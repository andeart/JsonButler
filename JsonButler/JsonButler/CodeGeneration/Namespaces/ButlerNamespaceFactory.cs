using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;



namespace Andeart.JsonButler.CodeGeneration.Namespaces
{

    internal class ButlerNamespaceFactory
    {
        public static ButlerNamespace Create (string name)
        {
            return new ButlerNamespace (name);
        }
    }

}