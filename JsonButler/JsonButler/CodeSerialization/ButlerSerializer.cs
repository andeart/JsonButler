using System;
using Newtonsoft.Json;



namespace Andeart.JsonButler.CodeSerialization
{

    public class ButlerSerializer
    {
        public static string SerializeType<T> ()
        {
            return SerializeType (typeof(T));
        }

        public static string SerializeType<T> (ButlerSerializerSettings settings)
        {
            return SerializeType (typeof(T), settings);
        }

        public static string SerializeType (Type type)
        {
            return SerializeType (type, new ButlerSerializerSettings (type.Assembly));
        }

        public static string SerializeType (Type type, ButlerSerializerSettings settings)
        {
            object instance = ButlerActivator.CreateInstance (type, settings);
            string instanceSerialized = JsonConvert.SerializeObject (instance, settings.JsonSerializerSettings);
            return instanceSerialized;
        }
    }

}