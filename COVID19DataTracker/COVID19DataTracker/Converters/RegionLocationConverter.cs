using System;
using System.Globalization;
using Xamarin.Forms;
using Region = COVID19DataTracker.Models.COVID19_Statistics.Region;

namespace COVID19DataTracker.Converters
{
    class RegionLocationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var r = (Region)value;
            return $"{r.Iso}, {r.Province}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
