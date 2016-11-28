using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// the view model representation of an association type.
    /// Contains also the translated name of the association.
    /// </summary>
    public class AssociationItemViewModel
    {
        public AssociationItemViewModel(string name, RelationType association)
        {
            Name = name;
            AssociationType = association;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 
        /// </summary>
        public RelationType AssociationType { get; }
    }

    /// <summary>
    /// stores a list of associations and their corresponding translations
    /// </summary>
    public class AssociationItemList : IEnumerable<AssociationItemViewModel>
    {
        private readonly List<AssociationItemViewModel> _items = new List<AssociationItemViewModel>();

        /// <summary>
        /// creates a predefined list of available associations and their translations
        /// </summary>
        public AssociationItemList()
        {
            _items.AddRange(new[]
            {
                new AssociationItemViewModel(
                    EditorStrings.AggregateAssociation,
                    RelationType.Aggregation),
                new AssociationItemViewModel(
                    EditorStrings.CompositeAssociation,
                    RelationType.Composition),
                new AssociationItemViewModel(
                    EditorStrings.SimpleAssociation,
                    RelationType.Association),
                new AssociationItemViewModel(
                    EditorStrings.UsesAssociation,
                    RelationType.Uses)
            });
        }

        public AssociationItemViewModel this[RelationType association] => _items.Single(x => x.AssociationType == association);
        public IEnumerator<AssociationItemViewModel> GetEnumerator() => _items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
    }
}
