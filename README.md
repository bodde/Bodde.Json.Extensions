# Bodde.Json.Extensions

[![.NET Standard](https://img.shields.io/badge/.NET%20Standard-2.0-512BD4)](https://dotnet.microsoft.com/platform/dotnet-standard)
[![.NET](https://github.com/bodde/Bodde.Json.Extensions/actions/workflows/dotnet.yml/badge.svg)](https://github.com/bodde/Bodde.Json.Extensions/actions/workflows/dotnet.yml)
[![Code coverage](https://img.shields.io/badge/code%20coverage-100%25-brightgreen)](https://github.com/bodde/Bodde.Json.Extensions/tree/main/Bodde.Json.Extensions.Test)

This package contains lightweight extension methods for serializing and deserializing JSON using `System.Text.Json`.
It targets .NET Standard 2.0 and supports configurable formatting, enum conversion, default value handling, and JSON files.


## Getting Started

Install the package using the .NET CLI:

```bash
dotnet add package Bodde.Json.Extensions
```

Then add the following using statement to your C# code:

```csharp
using Bodde.Json.Extensions;
```

## Projects reference

| Project | Description | GitHub |
| --- | --- | --- |
| `Bodde.Json.Extensions` | Main library containing the JSON extension methods. | [View project](https://github.com/bodde/Bodde.Json.Extensions/tree/main/Bodde.Json.Extensions) |
| `Bodde.Json.Extensions.Test` | Automated tests for the library. | [View project](https://github.com/bodde/Bodde.Json.Extensions/tree/main/Bodde.Json.Extensions.Test) |

## API Reference

| Class | Method | Description |
| --- | --- | --- |
| `T` | [`ToJson`](#ttojson) | Serializes an object to a JSON string. |
| `T` | [`ToFormattedJson`](#ttoformattedjson) | Serializes an object to a formatted JSON string. |
| `T` | [`ToJsonFile`](#ttojsonfile) | Serializes an object and writes the JSON to a file. |
| `T` | [`ToFormattedJsonFile`](#ttoformattedjsonfile) | Serializes an object with formatting and writes the JSON to a file. |
| `string` | [`FromJson<T>`](#stringfromjsont) | Deserializes a JSON string to an object. |
| `string` | [`FromFormattedJson<T>`](#stringfromformattedjsont) | Deserializes formatted JSON using configurable options. |
| `string` | [`FromJsonFile<T>`](#stringfromjsonfilet) | Reads a JSON file and deserializes its content. |
| `string` | [`FromFormattedJsonFile<T>`](#stringfromformattedjsonfilet) | Reads a formatted JSON file and deserializes its content. |

### T.ToJson

Serializes an object to a JSON string using `System.Text.Json.JsonSerializer`.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `options` | `JsonSerializerOptions?` | `null` | The options used for serialization. |

**Return type:** `string` - The serialized JSON.

```csharp
var json = employee.ToJson();
```

### T.ToFormattedJson

Serializes an object to a formatted JSON string. By default, output is indented, enums are represented as strings, and properties with default values are omitted.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `indented` | `bool` | `true` | Whether to format the JSON with indentation. |
| `enumsAsStrings` | `bool` | `true` | Whether to serialize enums as strings. |
| `skipDefaultValues` | `bool` | `true` | Whether to omit properties with default values. |

**Return type:** `string` - The formatted JSON.

```csharp
var json = employee.ToFormattedJson();
var compactJson = employee.ToFormattedJson(indented: false);
```

### T.ToJsonFile

Serializes an object to JSON and writes the result to a file.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `filename` | `string` | Required | The path of the file to write. |
| `options` | `JsonSerializerOptions?` | `null` | The options used for serialization. |

```csharp
employee.ToJsonFile("employee.json");
```

### T.ToFormattedJsonFile

Serializes an object with formatting options and writes the result to a file.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `filename` | `string` | Required | The path of the file to write. |
| `indented` | `bool` | `true` | Whether to format the JSON with indentation. |
| `enumsAsStrings` | `bool` | `true` | Whether to serialize enums as strings. |
| `skipDefaultValues` | `bool` | `true` | Whether to omit properties with default values. |

```csharp
employee.ToFormattedJsonFile("employee.json");
```

### string.FromJson\<T\>

Deserializes a JSON string to an object of type `T` using `System.Text.Json.JsonSerializer`.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `options` | `JsonSerializerOptions?` | `null` | The options used for deserialization. |

**Return type:** `T` - The deserialized object.

```csharp
var employee = json.FromJson<Employee>();
```

### string.FromFormattedJson\<T\>

Deserializes formatted JSON using configurable enum conversion and case-insensitive property matching.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `enumsAsStrings` | `bool` | `true` | Whether enum values are represented as strings. |
| `ignoreCase` | `bool` | `true` | Whether property name matching ignores case. |

**Return type:** `T` - The deserialized object.

```csharp
var employee = json.FromFormattedJson<Employee>();
```

### string.FromJsonFile\<T\>

Reads a JSON file and deserializes its content to an object of type `T`.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `options` | `JsonSerializerOptions?` | `null` | The options used for deserialization. |

**Return type:** `T` - The deserialized object.

```csharp
var employee = "employee.json".FromJsonFile<Employee>();
```

### string.FromFormattedJsonFile\<T\>

Reads a formatted JSON file and deserializes its content using configurable enum conversion and case-insensitive property matching.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `enumsAsStrings` | `bool` | `true` | Whether enum values are represented as strings. |
| `ignoreCase` | `bool` | `true` | Whether property name matching ignores case. |

**Return type:** `T` - The deserialized object.

```csharp
var employee = "employee.json".FromFormattedJsonFile<Employee>();
```