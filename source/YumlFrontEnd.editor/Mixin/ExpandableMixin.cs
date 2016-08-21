using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// mixin used to represent expandable behavior.
    /// Fires notification events in case a property was changed.
    /// Child classes should subscribe to these events and act as they 
    /// were the event source
    /// </summary>
    class ExpandableMixin : PropertyChangedBase
    {
        private bool _isExpanded = true;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            private set { _isExpanded = value; NotifyOfPropertyChange(nameof(IsExpanded)); }
        }
        public void ExpandOrCollapse() =>  IsExpanded = !IsExpanded;
    }
}
