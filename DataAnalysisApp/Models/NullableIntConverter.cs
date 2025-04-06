using System;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

public class IntToStringConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text) || string.Equals(text, "N/A", StringComparison.OrdinalIgnoreCase))
        {
            return string.Empty; // Ensure it's never null, return empty string instead
        }

        // If the text can be converted to an integer, return it as a string
        if (int.TryParse(text, out int result))
        {
            return result.ToString(); // Convert the integer to a string
        }

        return text; // Return the original string if it's not an integer
    }
}
