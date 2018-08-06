namespace Andeart.JsonButler.CodeGeneration.Namespaces
{

    internal static class ButlerNamespaceFactory
    {
        public static ButlerNamespace Create (string name)
        {
            return new ButlerNamespace (name);
        }
    }

}