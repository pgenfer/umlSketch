using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace YumlFrontEnd.editor.Converter
{
    public class IsVisibleToColorConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty VisibleBrushProperty = DependencyProperty.Register(
            "VisibleBrush", typeof(Brush), typeof(IsVisibleToColorConverter), new PropertyMetadata(default(Brush)));

        public Brush VisibleBrush
        {
            get { return (Brush) GetValue(VisibleBrushProperty); }
            set { SetValue(VisibleBrushProperty, value); }
        }

        public static readonly DependencyProperty InvisibleBrushProperty = DependencyProperty.Register(
            "InvisibleBrush", typeof(Brush), typeof(IsVisibleToColorConverter), new PropertyMetadata(default(Brush)));

        public Brush InvisibleBrush
        {
            get { return (Brush) GetValue(InvisibleBrushProperty); }
            set { SetValue(InvisibleBrushProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? VisibleBrush : InvisibleBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
