using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using UmlSketch.DomainObject;

namespace UmlSketch.Editor
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker
    {
        private readonly Dictionary<Color, PredefinedColor> _colorsByColorValue;

        public ColorPicker()
        {
            InitializeComponent();
            var colors = typeof(Colors)
                .GetProperties()
                .Where(x => x.PropertyType == typeof(Color))
                .Select(x => new PredefinedColor(x.Name,(Color)x.GetValue(null)))
                .ToList();
            var orderedColors = colors.OrderBy(x => x.Value.GetHue()).ThenBy(x => x.Value.GetSaturation());
            _colorsByColorValue = orderedColors.Distinct(new ColorByValueComparer())
                .ToDictionary(x => x.Value, y => y);
            ComboBoxWithColor.ItemsSource = orderedColors;
        }

        public static readonly DependencyProperty PredefinedColorProperty = DependencyProperty.Register(
            "PredefinedColor", typeof(PredefinedColor), typeof(ColorPicker), new PropertyMetadata(OnPredefinedColorChanged));

        private static void OnPredefinedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorPicker = (ColorPicker)d;
            var predefinedColor = (PredefinedColor)e.NewValue;
            var color = predefinedColor.Value;
            colorPicker.Color = color;
        }

        public PredefinedColor PredefinedColor
        {
            get { return (PredefinedColor) GetValue(PredefinedColorProperty); }
            set { SetValue(PredefinedColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color", typeof(Color), typeof(ColorPicker), new PropertyMetadata(OnColorChanged));

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorPicker = (ColorPicker) d;
            var color = (Color)e.NewValue;
            var predefinedColor = colorPicker._colorsByColorValue[color];
            colorPicker.PredefinedColor = predefinedColor;
        }

        public Color Color
        {
            get { return (Color) GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty DiagramColorPaletteProperty = DependencyProperty.Register(
            "DiagramColorPalette", typeof(DiagramColorPalette), typeof(ColorPicker), new PropertyMetadata(default(DiagramColorPalette)));

        public DiagramColorPalette DiagramColorPalette
        {
            get { return (DiagramColorPalette) GetValue(DiagramColorPaletteProperty); }
            set { SetValue(DiagramColorPaletteProperty, value); }
        }


        public static readonly DependencyProperty RecentlyUsedColorsProperty = DependencyProperty.Register(
            "RecentlyUsedColors", typeof(IEnumerable<PredefinedColor>), typeof(ColorPicker), new PropertyMetadata(default(IEnumerable<PredefinedColor>)));

        public IEnumerable<PredefinedColor> RecentlyUsedColors
        {
            get { return (IEnumerable<PredefinedColor>) GetValue(RecentlyUsedColorsProperty); }
            set { SetValue(RecentlyUsedColorsProperty, value); }
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            ComboBoxWithColor.IsDropDownOpen = !ComboBoxWithColor.IsDropDownOpen;
        }

        private void ComboBoxWithColor_OnDropDownOpened(object sender, EventArgs e)
        {
            RecentlyUsedColors = DiagramColorPalette
                .CollectColorsUsedInDiagram()
                .Select(x => x.ToColorFromFriendlyName())
                .Select(x => _colorsByColorValue[x]);
        }

        private void Button_OnRecentColorClicked(object sender, RoutedEventArgs e)
        {
            var predefinedColor = (PredefinedColor)((FrameworkElement) sender).DataContext;
            PredefinedColor = predefinedColor;
            ComboBoxWithColor.IsDropDownOpen = !ComboBoxWithColor.IsDropDownOpen;
        }
    }
}
