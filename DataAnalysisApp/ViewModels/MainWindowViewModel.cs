using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataAnalysisApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private GameManager gameManager = new GameManager();

    public ISeries[] GlobalSalesSeries { get; set; }
    public ISeries[] YearlyCountSeries { get; set; }
    public ISeries[] GenrePercentageSeries { get; set; }
    public ISeries[] PlatformPercentageSeries { get; set; }
    public ISeries[] PublisherPercentageSeries { get; set; }

    public string[] Labels { get; set; }
    public Axis[] XAxes { get; set; }


    public MainWindowViewModel()
    {
        var topGames = gameManager.GetTopGamesByGlobalSales(10);
        var yearlyCounts = gameManager.GetYearlyGameCount();
        var genreCounts = gameManager.GetGenrePercentages();
        var platformCounts = gameManager.GetPlatformPercentages();


        // Create the bar chart series for global sales
        GlobalSalesSeries = new ISeries[]
        {

            new ColumnSeries<double>
            {
                Name = "Global Sales",
                Values = topGames.Select(g => g.GlobalSales).ToArray()
            }
        };

        YearlyCountSeries = new ISeries[]
        {
            new ColumnSeries<int>
            {
                Name = "Yearly Count",
                Values = yearlyCounts.Select(g => g.Year).ToArray()
            }
        };

        // Create the pie chart series for genre percentages
        GenrePercentageSeries = genreCounts
        .Select(genre => new PieSeries<double>
        {
            Name = genre.Genre,
            Values = new double[] { genre.Percentage },
        })
        .ToArray();

        // Create the pie chart series for platform percentages
        PlatformPercentageSeries = platformCounts
        .Select(platform => new PieSeries<double>
        {
            Name = platform.Platform,
            Values = new double[] { platform.Percentage },
        })
        .ToArray();

        // Create the pie chart series for publisher percentages
        PublisherPercentageSeries = gameManager.GetTop10PublisherPercentages()
        .Select(publisher => new PieSeries<double>
        {
            Name = publisher.Publisher,
            Values = new double[] { publisher.Percentage },
        }).ToArray();



        XAxes = new Axis[]
        {
            new Axis
            {
                Labels = topGames.Select(g => g.Name).ToArray(),
                LabelsRotation = 45,
                TextSize = 10,
            }
        };
    }
}