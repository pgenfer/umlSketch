using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace YumlFrontEnd.editor
{
    public class InverseBooleanConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => this;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => !(bool) value;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => !(bool) value;
    }
}
