using System;
using Newtonsoft.Json;

namespace JsonButler.Creations
{
    public class ButlerFoo
    {
        [JsonProperty("name")]
        public string Name
        {
            get;
            private set;
        }

        [JsonProperty("lines")]
        public string[] Lines
        {
            get;
            private set;
        }

        [JsonProperty("winning_number")]
        public int WinningNumber
        {
            get;
            private set;
        }

        [JsonProperty("nested_type")]
        public NestedType NestedType
        {
            get;
            private set;
        }

        [JsonConstructor]
        ButlerFoo(string name, string[] lines, int winningNumber, NestedType nestedType)
        {
            Name = name;
            Lines = lines;
            WinningNumber = winningNumber;
            NestedType = nestedType;
        }
    }

    public class NestedType
    {
        [JsonProperty("super_nested_type")]
        public SuperNestedType SuperNestedType
        {
            get;
            private set;
        }

        [JsonConstructor]
        NestedType(SuperNestedType superNestedType)
        {
            SuperNestedType = superNestedType;
        }
    }

    public class SuperNestedType
    {
        [JsonProperty("id")]
        public float Id
        {
            get;
            private set;
        }

        [JsonConstructor]
        SuperNestedType(float id)
        {
            Id = id;
        }
    }
}