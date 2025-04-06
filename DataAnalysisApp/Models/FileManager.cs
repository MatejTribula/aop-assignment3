using System.IO; // For StreamReader
using CsvHelper; // For CsvReader
using CsvHelper.Configuration; // For CultureInfo.InvariantCulture
using System.Globalization;
using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;
using System.Linq;


public class FileManager
{
    public List<T> ReadCsvFile<T>(string path)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower(),
        };

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);

        return csv.GetRecords<T>().ToList();
    }
}