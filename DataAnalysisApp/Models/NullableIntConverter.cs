using System;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

public class NullableIntConverter : Int32Converter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text) || string.Equals(text, "N/A", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        return base.ConvertFromString(text, row, memberMapData);
    }
}
