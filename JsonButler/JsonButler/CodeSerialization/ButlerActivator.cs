using System;
using System.Reflection;
using Andeart.JsonButler.Utilities;



namespace Andeart.JsonButler.CodeSerialization
{

    public class ButlerActivator
    {
        public static object CreateInstance<T> ()
        {
            return CreateInstance (typeof(T));
        }

        public static object CreateInstance<T> (ButlerSerializerSettings settings)
        {
            return CreateInstance (typeof(T), settings);
        }

        public static object CreateInstance (Type type)
        {
            return CreateInstance (type, new ButlerSerializerSettings (type.Assembly));
        }

        public static object CreateInstance (Type type, ButlerSerializerSettings settings)
        {
            // Bail/simplify external objects first.
            if (type.Assembly != settings.RootCallingAssembly)
            {
                return CreateDefaultInstance (type);
            }

            ConstructorInfo constructorInfo = ReflectionUtilities.GetPreferredConstructor (type, settings.PreferredAttributeTypesOnConstructor);
            if (type.IsValueType && constructorInfo == null)
            {
                return Activator.CreateInstance (type);
            }

            return CreateInstanceWithConstructor (type, settings, constructorInfo);
        }

        private static object CreateInstanceWithConstructor (Type type, ButlerSerializerSettings settings, ConstructorInfo constructorInfo)
        {
            ParameterInfo[] parameterInfos = constructorInfo.GetParameters ();
            if (parameterInfos.Length == 0)
            {
                return Activator.CreateInstance (type);
            }

            object[] parameterObjects = new object[parameterInfos.Length];
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                Type parameterType = parameterInfos[i].ParameterType;
                parameterObjects[i] = CreateInstance (parameterType, settings);
            }

            return Activator.CreateInstance (type, parameterObjects);
        }

        private static object CreateDefaultInstance (Type type)
        {
            if (type.IsValueType || type.IsPrimitive)
            {
                return Activator.CreateInstance (type);
            }

            return type.IsArray ? new object[0] : null;
        }
    }

}