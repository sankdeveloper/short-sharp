
[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)

# <img align="left" width="100" height="100" src="https://github.com/sankdeveloper/short-sharp/blob/main/ShortSharp/src/ShortSharp/logo.png?raw=true"/>ShortSharp

ShortSharp(Your short code helper) is a day to day used C# Helper utility around the most common wrappers!!!

## Installation

```bash
  Install-Package ShortSharp
```

```bash
  dotnet add package ShortSharp --version 1.0.0
```

```bash
  <PackageReference Include="ShortSharp" Version="1.0.0" />
```
    
## API Reference

### ☞ Advance **Looping**
```csharp
foreach (var i in 5..10)
{
  // var index = 5; index < result.Count; index++
}
```
```csharp
foreach (var i in ..10)
{
  // iterate 0 to x
}
```
```csharp
foreach (var i in 10)
{
  // iterate 0 to x
}
```

### ☞ Extension Helpers
##### **String** Extensions
```csharp
var str = "Hello, Blah blah blah...";
```
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `str.IsCaseSensitiveEquals(string compareTo)` | `bool` | Case Sensitive comparison |
| `str.IsCaseInsensitiveEquals(string compareTo)` | `bool` | Case In-sensitive comparison |
| `str.GetMD5Hash(bool toBase64 = false, bool unicode = false)` | `string?` | Get MD hash |
| `str.UrlEncode()` | `string` | Encodes a URL string. |
| `str.UrlEncode(Encoding encoding)` | `string` | Encodes a URL string to specific encoding. |
| `str.UrlDecode()` | `string` |  Converts a string that has been encoded for transmission in a URL into a decoded string. |
| `str.UrlDecode(Encoding encoding)` | `string` |  Converts a string that has been encoded for transmission in a URL into a decoded string. |
| `str.HtmlEncode()` | `string` | Converts a string to an HTML-encoded string.  |
| `str.HtmlDecode()` | `string` |  Converts a string that has been HTML-encoded for HTTP transmission into a decoded string. |



##### **DateTime** Extensions
```csharp
DateTime dt = new DateTime();
```
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `dt.IsValid()` | `bool` | Is falling in valid date time range |
| `dt.AssumeUniversalTime()` | `DateTime` | Assumes to DateTimeKind.Utc for current date-time |
| `dt.ToJavaScriptTicks()` | `long` | Gets javascript date-time. |


##### **Enum** Extensions
```csharp
enum Level 
{
  [Description("Low Level description")] 
  Low,
  
  [Description("Medium Level description")] 
  Medium,
  
  [Description("High Level description")] 
  High
}

Level @enum = Level.Medium;
```
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `@enum.GetDescription()` | `string` |Get description attribute value |
| `@enum.GetDescriptions()` | `IEnumerable<string>` | Get multiple description attribute value |
| `EnumExtensions.ToDictionary(Level)` | `Dictionary<string, string>` | Converts to dictionary |


##### **QueryString** Extensions
```csharp
var urlLink = "http://www.my-url/users?type=xyz";
Uri uri = new Uri(urlLink);
```
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `uri.QueryString()` | `NameValueCollection` | get entire querystring name/value collection |
| `urlLink.QueryString()` | `NameValueCollection` | get entire querystring name/value collection |
| `uri.TryGetQueryStringParam(paramKey)` | `string?` | get single querystring value with specified key |
| `urlLink.TryGetQueryStringParam()` | `string?` | get single querystring value with specified key |


### ☞ Json Helpers
##### **Json Converters**
| Parameter | Description                |
| :-------- | :------------------------- |
| `DateTimeStringConverter` | datetime-in-string converter |
| `IntConverter` | int-in-string converter |
| `DynamicNestedObjectConverter` | dynamic object to Dictionary converter |

```csharp
// String format: "2022-10-21" (YYYY-mm-dd)
[JsonPropertyName("date")]
[JsonConverter(typeof(DateTimeStringConverter))]
public DateTime Date { get; set; }

// String format: "123654789"
[JsonPropertyName("id")]
[JsonConverter(typeof(IntConverter))]
public int IntegerId { get; set; }

// String format: {'prop1': {'one': 1}', 'prop2': {'two': 2}'.......}
[JsonPropertyName("object")]
[JsonConverter(typeof(DynamicNestedObjectConverter))]
public IReadOnlyDictionary<string, dynamic>> DynamicObject { get; set; }
```

### ☞ Utilities
##### **Simple In-memory job scheduler**
```csharp
BackgroundCronJobScheduler.Instance.ScheduleNew(
		jobFunction: () => System.WriteLine("Task exeecuted"),
		crownIntervalInMinutes: 1);
```





## License

[MIT](https://choosealicense.com/licenses/mit/)


## Authors

- [@SanketNaik](https://github.com/sankdeveloper)


## Roadmap

- Additional Test Cases

