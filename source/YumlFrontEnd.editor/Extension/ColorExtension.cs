using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static System.Convert;
using WinFormsColor = System.Drawing.Color;

namespace YumlFrontEnd.editor
{
    public static class ColorExtension
    {
        private static readonly Dictionary<Color,string> _FriendlyNamesByColor;
        private static readonly Dictionary<string, Color> _ColorsByFriendlyName;


        static ColorExtension()
        {
            var colors = typeof(Colors)
                .GetProperties()
                .Where(x => x.PropertyType == typeof(Color))
                .GroupBy(x => (Color) x.GetValue(null));
            // there are some colors with different names but same RGB values,
            // in that case we only store them with the first name we get
            _FriendlyNamesByColor = colors.ToDictionary(x => x.Key,y => y.First().Name.ToLower());
            _ColorsByFriendlyName = _FriendlyNamesByColor.ToDictionary(x => x.Value, y => y.Key);
        }

        public static Color ToColorFromFriendlyName(this string colorString)
        {
            // default case: no color defined
            if (string.IsNullOrEmpty(colorString))
                return Colors.Transparent;
            Color resultColor;
            _ColorsByFriendlyName.TryGetValue(colorString.ToLower(), out resultColor);
            return resultColor;
        }

        public static double GetHue(this Color color)
        {
            var winformsColor = WinFormsColor.FromArgb(color.A, color.R, color.G, color.B);
            return winformsColor.GetHue();
        }

        public static double GetSaturation(this Color color)
        {
            var winformsColor = WinFormsColor.FromArgb(color.A, color.R, color.G, color.B);
            return winformsColor.GetSaturation();
        }

        public static double GetBrightness(this Color color)
        {
            var winformsColor = WinFormsColor.FromArgb(color.A, color.R, color.G, color.B);
            return winformsColor.GetBrightness();
        }

        public static string ToFriendlyName(this Color color) => _FriendlyNamesByColor[color];
    }
}
