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

        public bool IsExpanded
        {
            get { return _expandable.IsExpanded; }
            set { _expandable.IsExpanded = value; }
        }

        public void ExpandOrCollapse() => _expandable.ExpandOrCollapse();

        public PropertyListViewModel(IListCommandContext<Property> listCommands):base(listCommands)
        {
        }
    }
}
