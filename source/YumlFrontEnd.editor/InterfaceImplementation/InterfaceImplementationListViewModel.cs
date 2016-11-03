using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    public class InterfaceImplementationListViewModel : ListViewModelBase<Implementation>
    {
        public InterfaceImplementationListViewModel(IListCommandContext<Implementation> commands) : base(commands)
        {
        }
    }
}
