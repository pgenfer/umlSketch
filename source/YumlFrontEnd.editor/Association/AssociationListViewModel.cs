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
    /// stores the list of associations that are related to a class
    /// </summary>
    internal class AssociationListViewModel : ListViewModelBase<Relation>
    {
        public AssociationListViewModel(IListCommandContext<Relation> commands) : 
            base(commands)
        {
        }
    }
}
