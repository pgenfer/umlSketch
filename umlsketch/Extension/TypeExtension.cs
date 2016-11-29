using System;
using System.Linq;

namespace UmlSketch.Editor
{
    public static class TypeExtension
    {
        public static string ToFriendlyName(this Type t)
        {
            if (t.GenericTypeArguments.Any())
            {
                var generics = string.Join(",", t.GenericTypeArguments.Select(x => x.ToFriendlyName()).ToArray());
                return $"{t.Name}<{generics}>";
            }
            return t.Name;
        }
    }
}
