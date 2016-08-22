using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    internal class MethodListViewModel : ListViewModelBase<Method>
    {
        public MethodListViewModel(
            IListCommandContext<Method> commands,
            ClassifierSelectionItemsSource classifierItemsSource) : 
            base(commands, classifierItemsSource)
        {
        }
    }
}
