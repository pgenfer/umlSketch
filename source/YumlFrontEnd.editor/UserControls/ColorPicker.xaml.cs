using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace UmlSketch.Editor
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker
    {
        public ColorPicker()
        {
            InitializeComponent();
            var colors = typeof(Colors)
                .GetProperties()
                .Where(x => x.PropertyType == typeof(Color))
                .Select(x => (Color)x.GetValue(null))
                .ToList();
            var orderedColors = colors.OrderBy(x => x.GetHue()).ThenBy(x => x.GetSaturation());
            ComboBoxWithColor.ItemsSource = orderedColors;
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color", typeof(Color), typeof(ColorPicker), new PropertyMetadata(default(Color)));

        public Color Color
        {
            get { return (Color) GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            ComboBoxWithColor.IsDropDownOpen = !ComboBoxWithColor.IsDropDownOpen;
        }
    }
}
