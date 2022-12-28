using System.Text;

namespace ShortSharp.Csv;

public static class CsvConverter
{
    public static string ToCsv<T>(this IEnumerable<T> items) where T : class
    {
        var csvBuilder = new StringBuilder();
        var properties = typeof(T).GetProperties();

        // Add the header line
        var headerLine = string.Join(",", properties.Select(p => p.Name));
        csvBuilder.AppendLine(headerLine);

        // Add the values for each object
        foreach (var item in items)
        {
            var dataLine = string.Join(",", properties.Select(p => p.GetValue(item, null)));
            csvBuilder.AppendLine(dataLine);
        }

        return csvBuilder.ToString();
    }
}