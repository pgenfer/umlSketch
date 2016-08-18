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
        private readonly ExpandableMixin _expandable = new ExpandableMixin();

        public MethodListViewModel(IListCommandContext<Method> commands) : base(commands)
        {
            _expandable.PropertyChanged += (_, e) => NotifyOfPropertyChange(e.PropertyName);
        }

        public bool IsExpanded => _expandable.IsExpanded;

        public void ExpandOrCollapse() => _expandable.ExpandOrCollapse();
    }
}
