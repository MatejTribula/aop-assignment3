using System.IO; // For StreamReader
using CsvHelper; // For CsvReader
using CsvHelper.Configuration; // For CultureInfo.InvariantCulture
using System.Globalization;
using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameManager
{
    public List<Game> Games { get; set; }
    // public List<dynamic> GenreCounts { get; set; } // Store genre counts and percentages

    private FileManager fileManager = new FileManager();

    public GameManager()
    {
        Games = fileManager.ReadCsvFile<Game>("Data/VideoGamesSales.csv");

        // var result = JoinMoviesAndRatings();
        // GenreCounts = result.GenreCounts;
    }

    public List<Game> GetTopGamesByGlobalSales(int topN)
    {
        return Games
            .Where(game => game.GlobalSales > 0) // Filter out games with 0 global sales
            .OrderByDescending(game => game.GlobalSales)
            .Take(topN)
            .ToList();
    }

    public List<YearCount> GetYearlyGameCount()
    {
        var yearCounts = Games
            .Where(game => game.Year != null) // Filter out null years
            .Where(game => game.Year != "") // Filter out null years
            .Where(game => game.Year != "N/A") // Filter out null years
            .GroupBy(game => game.Year)
            .Select(group => new YearCount
            {
                Year = Convert.ToInt32(group.Key),
                Count = group.Count()
            })
            .OrderByDescending(yearCount => yearCount.Count)
            .ToList();

        return yearCounts;
    }


    public List<PlatformPercentage> GetPlatformPercentages()
    {
        var totalGames = Games.Count;

        var platformCounts = Games
            .GroupBy(game => game.Platform)
            .Select(group => new PlatformPercentage
            {
                Platform = group.Key,
                Count = group.Count(),
                Percentage = group.Count() / (double)totalGames * 100
            })
            .OrderByDescending(platform => platform.Percentage)
            .ToList();

        return platformCounts;
    }

    public List<GenrePercentage> GetGenrePercentages()
    {
        var totalGames = Games.Count;

        var genreCounts = Games
            .GroupBy(game => game.Genre)
            .Select(group => new GenrePercentage
            {
                Genre = group.Key,
                Count = group.Count(),
                Percentage = group.Count() / (double)totalGames * 100
            })
            .OrderByDescending(genre => genre.Percentage)
            .ToList();

        return genreCounts;
    }

    public List<PublisherPercentage> GetTop10PublisherPercentages()
    {
        var totalGames = Games.Count;

        var publisherCounts = Games
            .GroupBy(game => game.Publisher)
            .Select(group => new PublisherPercentage
            {
                Publisher = group.Key,
                Count = group.Count(),
                Percentage = group.Count() / (double)totalGames * 100
            })
            .OrderByDescending(publisher => publisher.Percentage)
            .Take(10) // Limit to top 10 publishers
            .ToList();

        return publisherCounts;
    }




    // public (List<MovieWithAggregatedRatings> MovieRatings, List<dynamic> GenreCounts) JoinMoviesAndRatings()
    // {
    //     var movie_ratings = Movies.Join(MovieRatings,
    //             movie => movie.MovieId,
    //             rating => rating.MovieId,
    //             (movie, rating) => new
    //             {
    //                 movie.MovieId,
    //                 movie.Title,
    //                 Genres = movie.Genres.Split('|').ToList(), // Splitting here
    //                 rating.Rating,
    //             })
    //             .GroupBy(movie_rating => movie_rating.MovieId)
    //             .Select(group => new MovieWithAggregatedRatings(
    //                 group.Key,
    //                 group.First().Title,
    //                 string.Join("|", group.First().Genres), // Convert List<string> back to string
    //                 group.Average(mr => mr.Rating),
    //                 group.Count()
    //             ))
    //             .Where(movie_rating => movie_rating.ReviewCount > 200)
    //             .OrderByDescending(movie_rating => movie_rating.AverageRating)
    //             .Take(100)
    //             .ToList();

    //     var totalGenreCount = movie_ratings.Sum(movie_rating => movie_rating.Genres.Split('|').Length);

    //     var genreCounts = movie_ratings.SelectMany(movie_rating => movie_rating.Genres.Split('|'))
    //                            .GroupBy(genre => genre)
    //                            .Select(group => (dynamic)new
    //                            {
    //                                Genre = group.Key,
    //                                Count = group.Count(),
    //                                Percentage = (group.Count() / (double)totalGenreCount) * 100
    //                            })
    //                            .OrderByDescending(genre => genre.Percentage)
    //                            .ToList();

    //     return (movie_ratings, genreCounts);
    // }
}