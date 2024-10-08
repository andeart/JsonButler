# [![JsonButler logo][logo]](#) JsonButler

[![nuget-release](https://img.shields.io/nuget/v/Andeart.JsonButler.svg?logo=nuget&logoSize=auto)](https://www.nuget.org/packages/Andeart.JsonButler) [![nuget-dls](https://img.shields.io/nuget/dt/Andeart.JsonButler.svg?logo=nuget&logoSize=auto)](https://www.nuget.org/packages/Andeart.JsonButler)<br />
[![github-release](https://img.shields.io/github/release/andeart/Jsonbutler.svg?logo=github&logoSize=auto)](https://github.com/andeart/JsonButler/releases/latest) [![github-dls](https://img.shields.io/github/downloads/andeart/Jsonbutler/total.svg?logo=github&logoSize=auto)](https://github.com/andeart/JsonButler/releases/latest)<br/>

**JsonButler is now available as a Visual Studio extension. [Check it out here][jsonbutler ide]!**

---

**JsonButler** lets you:
- Generate C# types (along with the cs files if needed) from JSON data. Also generates nested custom types if needed.
- Serialize a known C# type to JSON without having to instantiate an instance of said type. It drills down and uses default values as far as possible.

## Code Generation
Example usage with Butler CLI (included in the `cli` directory in the nuget package):
```bash
# JsonButler can parse json text-snippets directly...
butler generate -j {"indices":[2,3,5,7]}

# ...or can instead parse the contents of a file.
butler generate -f response_payload.json

# By default, the generated C# code is copied directly to the clipboard.
# This can be overriden by setting an output file.
butler generate -f response_payload.json -o ResponseModel.cs
```

Example usage with C# library:
```csharp
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
```csharp
// Run JSONButler serialization with no customisation...
string serialized = ButlerSerializer.SerializeType<MyResponsePayload> ();

// ...or pass in specific serialization settings, such as limiting the scope of type generation, ...
ButlerSerializerSettings serializerSettings = new ButlerSerializerSettings (Assembly.GetExecutingAssembly ());

// ...setting constructor priorities for default object creation, ...
serializerSettings.PreferredAttributeTypesOnConstructor = new[] { typeof(MyConstructorAttribute), typeof(ClientsConstructorAttribute) };

// ... and customising the final JSON serialization.
serializerSettings.JsonSerializerSettings = myJsonSerializerSettings;

string serialized = ButlerSerializer.SerializeType<MyResponsePayload> (serializerSettings);
```

## Installation and Usage

`butler.exe` for standalone CLI functionality.<br />
`JsonButler.dll` for library usability with other projects.<br />

Recommended: Install the package via NuGet console.
```powershell
Install-Package Andeart.JsonButler
```
The CLI tool is included in the `cli` directory in the nuget package.

Optionally, you can instead download from [the Github Releases page](https://github.com/andeart/JsonButler/releases/latest), which contains both files.

## Feedback and Contribution
Please feel free to send in a Pull Request, or drop me an email. Cheers!

---

## Known issues with `JsonButler`
1. Code generation: JSON elements that are siblings and have the same property name currently create name-conflicting types.
2. Code generation: Array elements of a custom type are not correctly represented as an array in the owner type.

[logo]: https://user-images.githubusercontent.com/6226493/44009210-0bdfe344-9e5f-11e8-8439-4c7d32b3ce75.png "JsonButler"
[jsonbutler ide]: https://github.com/andeart/JsonButler-IDE "JsonButler-IDE"
