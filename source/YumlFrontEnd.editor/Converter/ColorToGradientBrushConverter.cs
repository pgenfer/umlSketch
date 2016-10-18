using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace YumlFrontEnd.editor
{
    public class ColorToGradientBrushConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty EndColorProperty = DependencyProperty.Register(
            "EndColor", typeof(Color), typeof(ColorToGradientBrushConverter), new PropertyMetadata(default(Color)));

        public Color EndColor
        {
            get { return (Color) GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var startColor = (Color) value;
            return new LinearGradientBrush(
                new GradientStopCollection
                {
                    new GradientStop(startColor, 0),
                    new GradientStop(EndColor, 1)
                },
                new Point(0, 0),
                new Point(1, 1));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
