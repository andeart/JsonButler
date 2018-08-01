using System.Collections.Generic;
using Andeart.CaseConversion;
using Newtonsoft.Json.Linq;



namespace Andeart.JsonButler.Utilities
{

    internal static class JsonUtilities
    {
        private static readonly Dictionary<JTokenType, string> _tokenTypesInKnownSyntax;

        static JsonUtilities ()
        {
            _tokenTypesInKnownSyntax = new Dictionary<JTokenType, string>
                                       {
                                           { JTokenType.Integer, "int" },
                                           { JTokenType.Float, "float" },
                                           { JTokenType.String, "string" },
                                           { JTokenType.Boolean, "bool" },
                                           { JTokenType.Date, "DateTime" },
                                           { JTokenType.Bytes, "byte[]" },
                                           { JTokenType.Guid, "Guid" }
                                       };
        }

        // bool indicates if a sibling class is involved
        public static bool GetTypeFrom (JToken jToken, out string type)
        {
            JTokenType tokenType = jToken.Type;
            if (_tokenTypesInKnownSyntax.TryGetValue (tokenType, out type))
            {
                return false;
            }

            if (tokenType == JTokenType.Array)
            {
                type = GetTypeFromArrayToken (jToken);
                return false;
            }

            if (tokenType == JTokenType.Object)
            {
                type = jToken.Path.ToPascalCase ();
                return true;
            }

            // TODO: Other JTokenType may have associated C# types.

            type = "object";
            return false;
        }

        private static string GetTypeFromArrayToken (JToken jToken)
        {
            JArray jArray = jToken as JArray;
            if (jArray == null || jArray.Count <= 0)
            {
                return "object[]";
            }

            GetTypeFrom (jToken.First, out string type);
            return $"{type}[]";
        }
    }

}