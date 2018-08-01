using System;
using System.Collections.Generic;
using Andeart.CaseConversion;
using Andeart.JsonButler.CodeGeneration.Classes;
using Andeart.JsonButler.Utilities;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;



namespace Andeart.JsonButler.CodeGeneration.Properties
{

    internal class ButlerPropertyFactory
    {
        public static ButlerProperty Create (JToken jToken)
        {
            if (!(jToken is JProperty))
            {
                throw new ArgumentException ($"Token {jToken} is not a valid property.", nameof(jToken));
            }

            JProperty jProperty = (JProperty) jToken;

            // Create property from type.
            bool requiresNewClass = JsonUtilities.GetTypeFrom (jProperty.Value, out string typeName);
            string propertyId = jProperty.Name;
            string propertyName = jProperty.Path.ToPascalCase ();
            ButlerProperty bProperty = new ButlerProperty (propertyName, propertyId, typeName);

            // Create additional type if needed.
            List<ButlerClass> dependencies = new List<ButlerClass>();
            if (requiresNewClass)
            {
                ButlerClass dependency = ButlerClassFactory.Create(typeName, jProperty.Path, jProperty.Value);
                dependencies.Add(dependency);
                dependencies.AddRange(dependency.Dependencies);
            }
            bProperty.AddDependencyRange (dependencies);

            return bProperty;
        }
    }

}