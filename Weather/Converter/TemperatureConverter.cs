using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using ColorHelper = Weather.Common.ColorHelper;

namespace Weather.Converter
{
    public class TemperatureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var doubleValue = double.Parse(value.ToString());
            var c= ColorHelper.ColorFromTemperature(doubleValue);
            return new SolidColorBrush( Color.FromArgb(c.A, c.R, c.G, c.B));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}