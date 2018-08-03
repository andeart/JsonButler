using System;



namespace Andeart.JsonButler.CodeSerialization
{

    public class ButlerSerializer
    {
        public static void SerializeType<T> ()
        {
            SerializeType<T> (new ButlerSerializerSettings (typeof(T).Assembly));
        }

        public static void SerializeType<T> (ButlerSerializerSettings settings) { }

        private static object CreatePrimitiveObject<T> ()
        {
            Type type = typeof(T);
            return CreatePrimitiveObject (type);
        }

        private static object CreatePrimitiveObject (Type type)
        {
            if (type.IsValueType || type.IsPrimitive)
            {
                return Activator.CreateInstance (type);
            }

            return type.IsArray ? new object[0] : null;
        }
    }

}