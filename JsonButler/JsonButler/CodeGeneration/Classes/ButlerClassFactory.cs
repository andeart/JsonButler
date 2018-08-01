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
            ButlerClass bClass = new ButlerClass (className, path);

            foreach (JToken childToken in jToken.Children ())
            {
                ButlerProperty bProperty = ButlerPropertyFactory.Create (childToken);
                bProperty.SetAccessibility (ButlerAccessibility.Public);
                bProperty.AddAccessor (ButlerAccessorType.Get, ButlerAccessibility.Public);
                bProperty.AddAccessor (ButlerAccessorType.Set, ButlerAccessibility.Private);
                bProperty.AddAttribute ("JsonProperty", $"\"{bProperty.Id}\"");

                bClass.AddProperty (bProperty);
            }

            ButlerConstructor bConstructor = new ButlerConstructor (bClass);
            bConstructor.AddPropertyParameterRange (bClass.Properties);
            bConstructor.AddAttribute ("JsonConstructor");

            bClass.AddConstructor (bConstructor);

            return bClass;
        }
    }

}