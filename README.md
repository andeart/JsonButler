# JsonButler
JsonButler lets you generate C# types (along with the cs files if needed) from JSON data. Nested custom types are supported.
It also lets you serialize a known C# type to JSON (with default values as much as possible) without having to instantiate an instance of said type.



## Code Generation
Example:
```cs
// Valid JSON text. This can be from a file, database/server response, etc.
string input = Resources.ButlerJsonContent;

// Generates C# code (i.e. contents of potential C# file).
string generatedCsCode = ButlerCodeGenerator.GenerateCodeFile (input);  // Generate
Console.WriteLine(generatedCsCode);
```


## Serialization
Example:
```cs
// Simplest usage. Settings available.
string serialized = ButlerSerializer.SerializeType<ButlerTestClass0> ();
```

---

## Known issues with `JsonButler`
1. Code generation: JSON elements that are siblings and have the same property name currently create name-conflicting types.
2. Code generation: Array elements of a custom type are not correctly represented as an array in the owner type.
