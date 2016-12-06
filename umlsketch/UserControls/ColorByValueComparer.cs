using System.Collections.Generic;

namespace UmlSketch.Editor
{
    /// <summary>
    /// compares two colors by their string representation (in hex)
    /// </summary>
    public class ColorByValueComparer : IEqualityComparer<PredefinedColor>
    {
        public bool Equals(PredefinedColor x, PredefinedColor y) => x.Value.ToString().Equals(y.Value.ToString());
        public int GetHashCode(PredefinedColor obj) => obj.Value.ToString().GetHashCode();
    }
}