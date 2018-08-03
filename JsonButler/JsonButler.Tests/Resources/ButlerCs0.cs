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

        [JsonProperty("new_type")]
        public NewType NewType
        {
            get;
            private set;
        }

        [JsonConstructor]
        ButlerFoo(string name, string[] lines, int winningNumber, NewType newType)
        {
            Name = name;
            Lines = lines;
            WinningNumber = winningNumber;
            NewType = newType;
        }
    }

    public class NewType
    {
        [JsonProperty("nested_type")]
        public NestedType NestedType
        {
            get;
            private set;
        }

        [JsonConstructor]
        NewType(NestedType nestedType)
        {
            NestedType = nestedType;
        }
    }

    public class NestedType
    {
        [JsonProperty("id")]
        public float Id
        {
            get;
            private set;
        }

        [JsonConstructor]
        NestedType(float id)
        {
            Id = id;
        }
    }
}