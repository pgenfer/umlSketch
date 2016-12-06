using System.Windows.Media;

namespace UmlSketch.Editor
{
    /// <summary>
    /// stores a predefined color value and its corresponding system
    /// name
    /// </summary>
    public class PredefinedColor
    {
        public PredefinedColor(string name, Color value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// the real color value of this predefined color
        /// </summary>
        public Color Value { get; }
        /// <summary>
        /// the name that is assigned to this color (black, white, aliceblue etc...)
        /// </summary>
        public string Name { get; }

        public override string ToString() => Name;
    }
}