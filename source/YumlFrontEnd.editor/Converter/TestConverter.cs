using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace UmlSketch.Editor
{
    public class Test : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => this;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
