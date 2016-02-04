using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuml.mixins
{
    /// <summary>
    /// mixin that adds name functionality
    /// to another class
    /// </summary>
    public class NameMixin
    {
        public string Name { get; set; }
        public override string ToString() => Name;
        public override int GetHashCode() => Name.GetHashCode();
        public override bool Equals(object obj) => (obj as NameMixin)?.Name == Name;
    }
}
