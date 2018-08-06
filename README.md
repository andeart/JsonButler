# JsonButler
JsonButler lets you generate C# types (along with the cs files if needed) from JSON data. Nested custom types are supported.
It also lets you serialize a known C# type to JSON (with default values as much as possible) without having to instantiate an instance of said type.



## Code Generation
Example usage with Butler executable:
```bash
# JsonButler can parse json text-snippets directly...
butler generate -j {"indices":[2,3,5,7]}

# ...or can instead parse the contents of a file.
butler generate -f "response_payload.json"

# By default, the generated C# code is copied directly to the clipboard.
# This can be overriden by setting an output file.
butler generate -f "response_payload.json" -o "ResponseModel.cs"
```

Example usage with C# library:
```cs
// JsonButler can parse json text-snippets directly...
string input = myJsonText;

// ...or can instead parse the contents of a file.
input = ButlerReader.ReadAllText (myJsonFilePath);

// Generates C# code (i.e. contents of potential C# file).
string generatedCsCode = ButlerCodeGenerator.GenerateCodeFile (input);

// JsonButler can either copy this text to the clipboard...
ButlerWriter.SetClipboardText (generatedCode);

// ...or write it to a file.
ButlerWriter.WriteAllText (outputFile, generatedCode);
```


## Serialization
Example usage with C# library:
```cs
// Simplest usage. Settings available.
string serialized = ButlerSerializer.SerializeType<ButlerTestClass0> ();
```

---

## Known issues with `JsonButler`
1. Code generation: JSON elements that are siblings and have the same property name currently create name-conflicting types.
2. Code generation: Array elements of a custom type are not correctly represented as an array in the owner type.
