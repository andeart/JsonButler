using System;
using System.Reflection;
using Newtonsoft.Json;



namespace Andeart.JsonButler.CodeSerialization
{

    public class ButlerSerializerSettings
    {
        public Assembly RootCallingAssembly { get; }

        public Type[] PreferredAttributeTypesOnConstructor { get; set; }

        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public ButlerSerializerSettings (Assembly rootCallingAssembly)
        {
            RootCallingAssembly = rootCallingAssembly;
        }
    }

}