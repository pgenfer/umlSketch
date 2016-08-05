using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// property represents a property of a class or an interface.
    /// Properties will be rendered as attributes in UML.
    /// </summary>
    public class Property : IVisible
    {
        private IVisible _visible = new VisibleMixin();
        private NameMixin _name = new NameMixin();

        public Property(string name, Classifier type)
        {
            Type = type;
            Name = name;
        }
        
        /// <summary>
        /// a property always has a type.
        /// This type is a reference to another classifier
        /// within the system.
        /// </summary>
        public Classifier Type { get; set; }

        public bool IsVisible
        {
            get { return _visible.IsVisible; }
            set { _visible.IsVisible = value; }
        }
        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value; }
        }
        public override string ToString() => $"{Type.Name} {Name}";

        public PropertyWriter WriteTo(PropertyWriter propertyWriter)
        {
            Requires(propertyWriter != null);

            return propertyWriter.WithType(Type.Name).WithName(Name);
        }
    }
}
