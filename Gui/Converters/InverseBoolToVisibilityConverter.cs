using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Gui.Converters
{
    public class InverseBoolToVisibilityConverter : IValueConverter
    {
        // Converts true to Visibility.Collapsed and false to Visibility.Visible
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {   
            // TODO: Is this casting or testing for true/false? What happens when the value is false?
            return value is bool boolValue && boolValue ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
