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
        public PropertyListViewModel(
            IListCommandContext<Property> listCommands,
            ClassifierSelectionItemsSource classifierItemsSource) : 
            base(listCommands, classifierItemsSource)
        {
        }
    }
}
