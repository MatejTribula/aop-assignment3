using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace DataAnalysisApp.Models;

public class StringToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string visibleGraph && parameter is string targetGraph)
        {
            return visibleGraph == targetGraph ? true : false;
        }
        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}