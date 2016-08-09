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
using Yuml.Commands;
using static System.Diagnostics.Contracts.Contract;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// view model for interaction with a single classifier object
    /// </summary>
    internal class ClassifierViewModel : 
        ViewModelBase<IClassiferCommands>
    {
        private readonly ExpandableMixin _expanded = new ExpandableMixin();
        private readonly EditableNameMixin _name;
        
        public ClassifierViewModel(
            IValidateNameService validationService, 
            IClassiferCommands commands):base(commands)
        {
            Requires(validationService != null);
            Requires(commands != null);

            _name = new EditableNameMixin(validationService,commands.RenameCommand);

            Properties = new PropertyListViewModel();
            // delegate events
            _name.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
            _expanded.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
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
        public void StopEditing(Confirmation configuration) => _name.StopEditing(configuration);
        public override string ToString() => _name.ToString();

        public bool IsExpanded
        {
            get { return _expanded.IsExpanded; }
            set { _expanded.IsExpanded = value; }
        }
        public void ExpandOrCollapse() => _expanded.ExpandOrCollapse();

        public bool HasNameError
        {
            get { return _name.HasNameError; }
            set { _name.HasNameError = value; }
        }

        public string NameErrorMessage
        {
            get { return _name.NameErrorMessage; }
            set { _name.NameErrorMessage = value; }
        }
    }
}
