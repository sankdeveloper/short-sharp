
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
  dotnet add package ShortSharp --version 1.x.x
```

```bash
  <PackageReference Include="ShortSharp" Version="1.x.x" />
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
##### **IEnumerable**
```csharp
// Also applies to all implemented collections
// e.g. List, ICollection, IQuerable etc.
private readonly List<string> _list = new() { "One", "Two", "Three", "Four", "Five" };
private IEnumerable<Task<T>> asyncTasks = new List() { Task1, Task2.............. Task_n};
```
| Parameter | Description                |
| :-------- | :------------------------- |
| `_list.ForEach()` | Like `List.ForEach()` but slightly better in terms of iterations using `IEnumerator`. |
| `_list.ForEachWithReturn()` | Like `List.ForEach()` but with _returning_ back an new `IEnumerable` collection. |
| `_list.PickRandom()` | Gets any one random item. |
| `_list.PickRandom(n)` | Gets random 'n' number of items. |
| `_list.Shuffle()` | Shuffle the list items. |
| `_list.Shuffle(nTimes)` | Shuffle the list items n-times. |
| `_list.Join(saperator: ",")` | Gets back a string with coma saperated words. |
| `asyncTasks.WhenAllAsync()` | Wait till all task finishes. |
| `asyncTasks.WhenAllSequentialAsync()` | Wait till all task finishes 'sequencially'. |
| `asyncTasks.WhenAllByChunkAsync(chunkSize: 2)` | Process tasks by chunk(just like Pagination, e.g process 2 tasks at a time). |


##### **ICollection**
```csharp
// Also applies to all implemented collections
// e.g. List, ICollection, IQuerable etc.
private readonly ICollection<string> _list = new List<string> { "One", "Two", "Three", "Four", "Five" };
```
| Parameter | Type  | Description                |
| :-------- | :---- | :------------------------- |
| `_list.AddIf(predicate, value) / _list.RemoveIf(predicate, value)` | `bool` | Adds/removes only if the value satisfies the predicate. |
| `_list.AddIfNotContains(value)` | `bool` | Add value if the ICollection doesn't contains it already. |
| `_list.AddRange(v1, v2...) / _list.RemoveRange(v1, v2...)` | `void` | Adds/removes a range to 'values'. |
| `_list.AddRangeIf(predicate, v1, v2...) / _list.RemoveRangeIf(predicate, v1, v2...)` | `void` | Adds/ removes a collection of objects to the end of this collection only for value who satisfies the predicate. |
| `_list.RemoveWhere(predicate)` | `void` | Removes value that satisfies the predicate. |


##### **IDictionary**
```csharp
private Dictionary<string, string> _dictionary = new();
```
| Parameter | Type  | Description                |
| :-------- | :---- | :------------------------- |
| `_dictionary.AddIfNotContainsKey(key, value)` | `bool` | Adds if not contains key. |
| `_dictionary.RemoveIfContainsKey(key)` | `bool` | Removes if contains key. |
| `_dictionary.UpsertByKey(key, value)` | `value` | Add if the key does not already exist, or to update a key/value pair in the IDictionary&lt;TKey, TValue&gt;> if the key already exists. |
| `_dictionary.GetOrAdd(predicate, value)` | `value` | Adds a key/value pair if the key does not already exist. |
| `_dictionary.RemoveIfContainsKey(predicate, value)` | `void` | Removes if contains key. |

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

##### **OI** Extensions (_v1.2.1_)
| On | Type  | Returns | Description |
| :-------- | :---- | :------------------------- |:------------------------- |
|`string`|`ToDirectoryInfo()`|`DirectoryInfo`| Converts to directory |
|`DirectoryInfo`|`Clear()`|`void`| A DirectoryInfo extension method that clears all files and directories in this directory.|
|`DirectoryInfo`|`DeleteDirectoriesWhere(predicate)`|`void`| A DirectoryInfo extension method that deletes the directories where. |
|`DirectoryInfo`|`DeleteDirectoriesWhere(predicate, searchOption, searchPattern = "*.*")`|`void`| A DirectoryInfo extension method that deletes the directories where. |
|`DirectoryInfo`|`DeleteFilesWhere(predicate)`|`void`| A DirectoryInfo extension method that deletes the files where. |
|`DirectoryInfo`|`DeleteFilesWhere(predicate, searchOption, searchPattern = "*.*")`|`void`| A DirectoryInfo extension method that deletes the files where. |
|`DirectoryInfo`|`DeleteOlderThan(DateTime minDate, searchOption, searchPattern = "*.*")`|`void`| A DirectoryInfo extension method that deletes the older than. |
|`DirectoryInfo`|`EnsureDirectoryExists()`|`DirectoryInfo`| Creates all directories and subdirectories in the specified @this if the directory doesn't already exists. This methods is the same as FileInfo.CreateDirectory however it's less ambigues about what happen if the directory already exists. |
|`DirectoryInfo`|`EnumerateDirectories(string searchPattern = "*.*", SearchOption searchOption = SearchOption.TopDirectoryOnly)`|`IEnumerable<DirectoryInfo>`| Enumerate directories|
|`DirectoryInfo`|`GetDirectories(string searchPatterns, SearchOption searchOption)`|`DirectoryInfo[]`| Get all directories |
|`DirectoryInfo`|`GetDirectories(string[] searchPatterns, SearchOption searchOption)`|`DirectoryInfo[]`| Get all directories |
|`DirectoryInfo`|`GetFiles(string searchPatterns = "*.*", SearchOption searchOption = SearchOption.TopDirectoryOnly)`|`DirectoryInfo[]`| Get all files |
|`DirectoryInfo`|`GetFiles(string[] searchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly)`|`DirectoryInfo[]`| Get all files |
|`DirectoryInfo`|`GetFilesWhere(Func<FileInfo, bool> predicate, string searchPattern = "*.*", SearchOption searchOption = SearchOption.TopDirectoryOnly)`|`DirectoryInfo[]`| Get all files with Where predicate having true/false. |
|`DirectoryInfo`|`PathCombine(params string[] paths)`|`string`| Combines multiples string into a path. |
|`DirectoryInfo`|`PathCombineFile(params string[] paths)`|`FileInfo`| Combines multiples string into a 'File' path. |
|`DirectoryInfo`|`EnumerateFiles(string searchPattern = "*.*", SearchOption searchOption = SearchOption.TopDirectoryOnly)`|`IEnumerable<FileInfo>`| Enumerate Files. |



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

