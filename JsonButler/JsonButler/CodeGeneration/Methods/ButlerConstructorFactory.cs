using Andeart.JsonButler.CodeGeneration.Classes;



namespace Andeart.JsonButler.CodeGeneration.Methods
{

    internal static class ButlerConstructorFactory
    {
        public static ButlerConstructor Create (ButlerClass bClass)
        {
            return new ButlerConstructor (bClass);
        }
    }

}