using System.Diagnostics.Contracts;
using Common;

namespace UmlSketch.DomainObject
{
    /// <summary>
    /// a parameter of a method
    /// </summary>
    public class Parameter : IVisible, INamed
    {
        private readonly IVisible _visible = new VisibleMixin();
        private readonly NameMixin _name = new NameMixin();
        private Classifier _type;

        public Parameter(Classifier type, string name, bool isVisible=true)
        {
            _type = type;
            Name = name;
            _visible.IsVisible = isVisible;
        }

        public bool IsVisible
        {
            get { return _visible.IsVisible; }
            set { _visible.IsVisible = value; }
        }

        /// <summary>
        /// type of the parameter
        /// </summary>
        public Classifier Type
        {
            get { return _type; }
            set
            {
                Contract.Requires(value != null);
                _type = value;
                    
            }
        }

        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value; }
        }

        public override string ToString() => $"{Type.Name} {Name}";
    }
}
