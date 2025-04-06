using System.Windows.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Linq;

namespace DataAnalysisApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private GameManager _gameManager = new();

    // Add the missing chart properties
    public ISeries[] GlobalSalesSeries { get; set; } = new ISeries[0];
    public ISeries[] YearlyCountSeries { get; set; } = new ISeries[0];
    public ISeries[] GenrePercentageSeries { get; set; } = new ISeries[0];
    public ISeries[] PlatformPercentageSeries { get; set; } = new ISeries[0];
    public ISeries[] PublisherPercentageSeries { get; set; } = new ISeries[0];
    public Axis[] XAxes { get; set; } = new Axis[0];

    public bool IsGlobalSalesVisible { get; set; } = false;
    public bool IsYearlyCountVisible { get; set; } = false;
    public bool IsGenrePercentageVisible { get; set; } = false;
    public bool IsPlatformPercentageVisible { get; set; } = false;
    public bool IsPublisherPercentageVisible { get; set; } = false;

    public ICommand ShowGlobalSalesCommand { get; }
    public ICommand ShowYearlyCountCommand { get; }
    public ICommand ShowGenrePercentageCommand { get; }
    public ICommand ShowPlatformPercentageCommand { get; }
    public ICommand ShowPublisherPercentageCommand { get; }

    public MainWindowViewModel()
    {
        // Initialize chart data
        InitializeChartData();

        // Initialize commands
        ShowGlobalSalesCommand = new RelayCommand(() => ShowGraph("GlobalSales"));
        ShowYearlyCountCommand = new RelayCommand(() => ShowGraph("YearlyCount"));
        ShowGenrePercentageCommand = new RelayCommand(() => ShowGraph("GenrePercentage"));
        ShowPlatformPercentageCommand = new RelayCommand(() => ShowGraph("PlatformPercentage"));
        ShowPublisherPercentageCommand = new RelayCommand(() => ShowGraph("PublisherPercentage"));
    }

    private void InitializeChartData()
    {
        var topGames = _gameManager.GetTopGamesByGlobalSales(10);
        var yearlyGameCount = _gameManager.GetYearlyGameCount();
        var genrePercentages = _gameManager.GetGenrePercentages();
        var platformPercentages = _gameManager.GetPlatformPercentages();
        var publisherPercentages = _gameManager.GetTop10PublisherPercentages();

        // Create global sales chart
        GlobalSalesSeries = new ISeries[]
        {
            new ColumnSeries<double>
            {
                Name = "Global Sales (millions)",
                Values = topGames.Select(g => g.GlobalSales).ToArray()
            }
        };

        // Create yearly count chart
        YearlyCountSeries = new ISeries[]
        {
            new ColumnSeries<int>
            {
                Name = "Game Count by Year",
                Values = yearlyGameCount.Select(y => y.Count).ToArray()
            }
        };

        // Create genre percentages chart
        GenrePercentageSeries = genrePercentages.Select(g => new PieSeries<double>
        {
            Name = g.Genre,
            Values = new double[] { g.Percentage }
        }).ToArray();

        // Create platform percentages chart
        PlatformPercentageSeries = platformPercentages.Select(p => new PieSeries<double>
        {
            Name = p.Platform,
            Values = new double[] { p.Percentage }
        }).ToArray();

        // Create publisher percentages chart
        PublisherPercentageSeries = publisherPercentages.Select(p => new PieSeries<double>
        {
            Name = p.Publisher,
            Values = new double[] { p.Percentage }
        }).ToArray();

        // Create X-axis labels
        XAxes = new Axis[]
        {
            new Axis
            {
                Labels = topGames.Select(g => g.Name).ToArray(),
                LabelsRotation = 45
            }
        };
    }

    private void ShowGraph(string graphName)
    {
        IsGlobalSalesVisible = graphName == "GlobalSales";
        IsYearlyCountVisible = graphName == "YearlyCount";
        IsGenrePercentageVisible = graphName == "GenrePercentage";
        IsPlatformPercentageVisible = graphName == "PlatformPercentage";
        IsPublisherPercentageVisible = graphName == "PublisherPercentage";

        OnPropertyChanged(nameof(IsGlobalSalesVisible));
        OnPropertyChanged(nameof(IsYearlyCountVisible));
        OnPropertyChanged(nameof(IsGenrePercentageVisible));
        OnPropertyChanged(nameof(IsPlatformPercentageVisible));
        OnPropertyChanged(nameof(IsPublisherPercentageVisible));
    }
}