using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// view model that handles access to a complete list of properties
    /// </summary>
    internal class PropertyListViewModel : PropertyChangedBase
    {
        BindableCollectionMixin<PropertyViewModel> _properties = 
            new BindableCollectionMixin<PropertyViewModel>();
        private ExpandableMixin _expandable = new ExpandableMixin();

        public PropertyListViewModel()
        {
            Items.Add(new PropertyViewModel { Name = "Length" });
            _expandable.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
        }

        public BindableCollection<PropertyViewModel> Items => _properties.Items;

        public bool IsExpanded
        {
            get { return _expandable.IsExpanded; }
            set { _expandable.IsExpanded = value; }
        }

        public void ExpandOrCollapse() => _expandable.ExpandOrCollapse();
    }
}
