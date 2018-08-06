using System;
using System.Reflection;



namespace Andeart.JsonButler.Utilities
{

    internal static class ReflectionUtilities
    {
        public static ConstructorInfo GetPreferredConstructor (Type type, Type[] preferredAttributeTypes)
        {
            return GetPreferredConstructor (type.GetConstructors (), preferredAttributeTypes);
        }

        public static ConstructorInfo GetPreferredConstructor (ConstructorInfo[] constructorInfos, Type[] preferredAttributeTypes)
        {
            if (constructorInfos.IsNullOrEmpty ())
            {
                return null;
            }

            if (preferredAttributeTypes.IsNullOrEmpty ())
            {
                return constructorInfos[0];
            }

            if (preferredAttributeTypes.Any (IsNotAnAttribute, out Type failureType))
            {
                throw new Exception ($"Provided preferred-attribute-type {failureType} is not an Attribute type.");
            }

            for (int i = 0; i < preferredAttributeTypes.Length; i++)
            {
                for (int j = 0; j < constructorInfos.Length; j++)
                {
                    if (constructorInfos[j].GetCustomAttribute (preferredAttributeTypes[i]) != null)
                    {
                        return constructorInfos[j];
                    }
                }
            }

            return constructorInfos[0];
        }

        private static bool IsSameOrSubclassOf (this Type derivedType, Type baseType)
        {
            return derivedType.IsSubclassOf (baseType) || derivedType == baseType;
        }

        private static bool IsNotAnAttribute (this Type type)
        {
            return !type.IsSameOrSubclassOf (typeof(Attribute));
        }
    }

}