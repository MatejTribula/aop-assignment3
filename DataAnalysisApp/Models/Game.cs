using CsvHelper.Configuration.Attributes;
public class Game
{
    [Name("Rank")]
    public int Rank { get; set; }

    [Name("Name")]
    public required string Name { get; set; }

    [Name("Platform")]
    public required string Platform { get; set; }

    [Name("Year")]
    [TypeConverter(typeof(IntToStringConverter))]
    public string? Year { get; set; }

    [Name("Genre")]
    public required string Genre { get; set; }

    [Name("Publisher")]
    public required string Publisher { get; set; }

    [Name("NA_Sales")]
    public double NASales { get; set; }

    [Name("EU_Sales")]
    public double EUSales { get; set; }

    [Name("JP_Sales")]
    public double JPSales { get; set; }

    [Name("Other_Sales")]
    public double OtherSales { get; set; }

    [Name("Global_Sales")]
    public double GlobalSales { get; set; }

    public Game() { }

    public Game(int rank, string name, string platform, string year, string genre, string publisher,
        double naSales, double euSales, double jpSales, double otherSales, double globalSales)
    {
        Rank = rank;
        Name = name;
        Platform = platform;
        Year = year;
        Genre = genre;
        Publisher = publisher;
        NASales = naSales;
        EUSales = euSales;
        JPSales = jpSales;
        OtherSales = otherSales;
        GlobalSales = globalSales;
    }
}
