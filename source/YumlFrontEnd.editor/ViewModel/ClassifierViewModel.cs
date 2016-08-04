using Caliburn.Micro;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace YumlFrontEnd.editor
{
    internal class ClassifierViewModel : PropertyChangedBase
    {
        private ExpandableMixin _expanded = new ExpandableMixin();
        private EditableNameMixin _name = new EditableNameMixin();
        

        public ClassifierViewModel()
        {
            Properties = new PropertyListViewModel();
            // also delegate events
            _expanded.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
            _name.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
        }

        public PropertyListViewModel Properties { get; private set; }

        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value; }
        }

        public bool IsEditable
        {
            get { return _name.IsEditable; }
            set { _name.IsEditable = value; }
        }

        public void StartEditing() => _name.StartEditing();
        public void StopEditing(EventArgs args) => _name.StopEditing(args);
        public override string ToString() => _name.ToString();

        public bool IsExpanded
        {
            get { return _expanded.IsExpanded; }
            set { _expanded.IsExpanded = value; }
        }

        public void ExpandOrCollapse() => _expanded.ExpandOrCollapse();
    }
}
