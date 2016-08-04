using Caliburn.Micro;
using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Yuml;

namespace YumlFrontEnd.editor
{
    internal class ClassifierViewModel : PropertyChangedBase
    {
        private ExpandableMixin _expanded = new ExpandableMixin();
        private EditableNameMixin _name = new EditableNameMixin();
        private readonly ClassifierValidationService _validationService;
        private bool _hasError = false;
        private string _errorMessage = string.Empty;


        public ClassifierViewModel(ClassifierValidationService validationService)
        {
            Properties = new PropertyListViewModel();
            // also delegate events
            _expanded.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
            _name.PropertyChanged += OnMixinPropertyChanged;

            _validationService = validationService;
        }

        private void OnMixinPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            NotifyOfPropertyChange(args.PropertyName);
            // revalidate the name whenever it changes
            if (args.PropertyName == nameof(Name))
                ValidateName();
        }

        public PropertyListViewModel Properties { get; private set; }

        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value;}
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

        public bool HasError
        {
            get { return _hasError; }
            set { _hasError = value; NotifyOfPropertyChange(nameof(HasError)); }
        }

        public string Error
        {
            get { return _errorMessage; }
            set { _errorMessage = value; NotifyOfPropertyChange(nameof(Error)); }
        }

        private void ValidateName()
        {
            var result = _validationService.CheckName(OriginalName,Name);
            HasError = result.HasError;
            Error = result.Message;           
        }
        
        public void ExpandOrCollapse() => _expanded.ExpandOrCollapse();

        public string OriginalName => _name.OriginalName;
    }
}
