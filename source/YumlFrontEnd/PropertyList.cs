using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// handles the interaction with a list of properties
    /// </summary>
    public class PropertyList : IEnumerable<Property>
    {
        private readonly List<Property> _list = new List<Property>();

        public PropertyList() { }

        /// <summary>
        /// sets visible flag to all properties of the list
        /// </summary>
        /// <param name="showOrHide"></param>
        public void ShowOrHideAllProperties(bool showOrHide) => _list.ForEach(x => x.IsVisible = showOrHide);

        /// <summary>
        /// adds a new property to the property list.
        /// Currently there is no restriction about duplicate properties
        /// </summary>
        /// <param name="name">name of property</param>
        /// <param name="type">classifier of the property</param>
        /// <returns>the newly added property</returns>
        public Property CreateProperty(string name, Classifier type)
        {
            Requires(!string.IsNullOrEmpty(name));
            Requires(type != null);
            Ensures(_list.Count == OldValue(_list.Count) + 1);

            var property = new Property(name, type);
            _list.Add(property);
            return property;
        }

        /// <summary>
        /// adds a property to this property list.
        /// Should only be used by serializer.
        /// </summary>
        /// <param name="p">Property that should be added to the list</param>
        internal void AddExistingProperty(Property p)
        {
            Requires(p != null);

            _list.Add(p);
        }

        public IEnumerator<Property> GetEnumerator() =>  _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

        public override string ToString() => string.Join(Environment.NewLine, _list.Select(x => x.ToString()));      
    }
}
