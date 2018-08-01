using Andeart.JsonButler.CodeGeneration;
using Andeart.JsonButler.CodeGeneration.Classes;
using Andeart.JsonButler.CodeGeneration.Methods;



namespace Andeart.JsonButler.CodeGeneration.Methods
{

    internal class ButlerConstructorFactory
    {
        public static ButlerConstructor Create (ButlerClass bClass)
        {
            return new ButlerConstructor (bClass);
        }
    }

}