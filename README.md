
[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)
![Nuget](https://img.shields.io/nuget/dt/ShortSharp?color=grn)
![Maintained - yes](https://img.shields.io/badge/Maintained-yes-green)
[![dotnet package](https://github.com/sankdeveloper/short-sharp/actions/workflows/main.yml/badge.svg)](https://github.com/sankdeveloper/short-sharp/actions/workflows/main.yml)
![Contributions - welcome](https://img.shields.io/badge/Contributions-welcome-blueviolet)
![docs.rs](https://img.shields.io/docsrs/s)
![GitHub issues](https://img.shields.io/github/issues-raw/sankdeveloper/short-sharp)
![GitHub last commit (branch)](https://img.shields.io/github/last-commit/sankdeveloper/short-sharp/main)

# <img align="left" width="100" height="100" src="https://github.com/sankdeveloper/short-sharp/blob/main/ShortSharp/src/ShortSharp/logo.png?raw=true"/>ShortSharp

ShortSharp(Your short code helper) is a day to day used C# Helper utility around the most common wrappers !!!

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

### ☞ Utilities
##### **Simple In-memory job scheduler**
```csharp
BackgroundCronJobScheduler.Instance.ScheduleNew(
		jobFunction: () => System.WriteLine("Task exeecuted"),
		crownIntervalInMinutes: 1);
```

### ☞ Helpers
##### **Reflections**
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Reflection.GetPublicPropertyNames<TClass>()` | `IEnumerable<string>` | Read all properties of TClass |
| `Reflection.GetPublicPropertyValues<TClass>(object)` | `IReadOnlyDictionary<string, object?>` | Read all properties and values of TClass reference |

### ☞ Json
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


### ☞ Extensions
##### **String** Extensions
```csharp
var str = "Hello, Blah blah blah...";
```
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `str.EqualsCaseSensitive(string compareTo)` | `bool` | Case Sensitive comparison |
| `str.EqualsCaseIgnore(string compareTo)` | `bool` | Case In-sensitive comparison |
| `str.GetMD5Hash(bool toBase64 = false, bool unicode = false)` | `string?` | Get MD hash |
| `str.UrlEncode()` | `string` | Encodes a URL string. |
| `str.UrlEncode(Encoding encoding)` | `string` | Encodes a URL string to specific encoding. |
| `str.UrlDecode()` | `string` |  Converts a string that has been encoded for transmission in a URL into a decoded string. |
| `str.UrlDecode(Encoding encoding)` | `string` |  Converts a string that has been encoded for transmission in a URL into a decoded string. |
| `str.HtmlEncode()` | `string` | Converts a string to an HTML-encoded string.  |
| `str.HtmlDecode()` | `string` |  Converts a string that has been HTML-encoded for HTTP transmission into a decoded string. |
| `str.ToMemoryStream(Encoding encoding)` | `string` |  Convert value to a MemoryStream, using a default Unicode encoding. |
| `str.IsInteger()` | `bool` |  Check if string is an Integer number. |
| `str.IsDouble()` | `bool` |  Check if string is an Double number. |


##### **Boolean** Extensions
```csharp
bool str = true;
```
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `str.AsYOrN()` | `string` |  Returns Char 'Y' for true, 'N' for false. |
| `str.AsYesOrNo()` | `string` |  Returns string 'Yes' for true, 'No' for false. |
| `str.As0Or1()` | `string` |  Returns int '1' for true, '0' for false. |
| `str.AsZeroOrOne()` | `string` |  Returns Char 'Zero' for true, 'One' for false. |


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



## License
[MIT](https://choosealicense.com/licenses/mit/)

## Authors
- [@SanketNaik](https://github.com/sankdeveloper)

## Feedback
If you have any feedback, please reach out to us at sankdeveloper@gmail.com


## Roadmap
- [x] CI/CD pipeline for Nuget.org push.
- [ ] More features will be still in progress to add.
- [ ] Test cases and code coverage.

