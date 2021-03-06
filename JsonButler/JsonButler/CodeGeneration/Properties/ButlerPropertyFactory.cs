﻿using Andeart.CaseConversions;
using Andeart.JsonButler.CodeGeneration.Classes;
using Andeart.JsonButler.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;


namespace Andeart.JsonButler.CodeGeneration.Properties
{

    internal static class ButlerPropertyFactory
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
            string propertyName = propertyId.ToPascalCase ();

            // Create additional type if needed.
            var dependencies = new List<ButlerClass> ();
            if (requiresNewClass)
            {
                typeName = propertyName;
                ButlerClass dependency = ButlerClassFactory.Create (typeName, jProperty.Path, jProperty.Value);
                dependencies.Add (dependency);
                dependencies.AddRange (dependency.Dependencies);
            }

            ButlerProperty bProperty = new ButlerProperty (propertyName, propertyId, typeName);
            bProperty.AddDependencyRange (dependencies);

            return bProperty;
        }
    }

}