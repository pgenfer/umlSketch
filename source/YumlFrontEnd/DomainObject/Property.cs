using System.Diagnostics.Contracts;
using Common;
using UmlSketch.DiagramWriter;

namespace UmlSketch.DomainObject
{
    /// <summary>
    /// property represents a property of a class or an interface.
    /// Properties will be rendered as attributes in UML.
    /// </summary>
    public class Property : IVisible, INamed
    {
        private readonly IVisible _visible = new VisibleMixin();
        private readonly NameMixin _name = new NameMixin();

        /// <summary>
        /// only for test stubs, should not be used in production
        /// </summary>
        public Property() { }

        public Property(string name, Classifier type, bool isVisible = true)
        {
            Type = type;
            Name = name;
            IsVisible = isVisible;
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
            Contract.Requires(propertyWriter != null);

            return propertyWriter.WithType(Type.Name).WithName(Name);
        }

        /// <summary>
        /// a property can be connected with a relation.
        /// In that case, changing the property would also change
        /// the relation and vice versa.
        /// </summary>
        public Relation Relation { get; set; }
    }
}
