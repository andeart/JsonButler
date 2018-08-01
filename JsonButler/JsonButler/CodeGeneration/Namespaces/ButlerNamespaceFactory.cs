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