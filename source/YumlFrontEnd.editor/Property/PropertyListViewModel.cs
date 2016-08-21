using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// view model that handles access to a complete list of properties
    /// </summary>
    internal class PropertyListViewModel : ListViewModelBase<Property>
    {
        private readonly ExpandableMixin _expandable = new ExpandableMixin();
         public bool IsExpanded => _expandable.IsExpanded;

        public void ExpandOrCollapse() => _expandable.ExpandOrCollapse();

        public PropertyListViewModel(
            IListCommandContext<Property> listCommands,
            ClassifierSelectionItemsSource classifierItemsSource) : 
            base(listCommands, classifierItemsSource)
        {
            _expandable.PropertyChanged += (_, e) => NotifyOfPropertyChange(e.PropertyName);
        }
    }
}
