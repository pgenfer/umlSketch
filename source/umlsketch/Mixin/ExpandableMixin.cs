using Caliburn.Micro;

namespace UmlSketch.Editor
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
            set { _isExpanded = value; NotifyOfPropertyChange(nameof(IsExpanded)); }
        }
        public void ExpandOrCollapse() =>  IsExpanded = !IsExpanded;
        public void Collapse() => IsExpanded = false;
    }
}
