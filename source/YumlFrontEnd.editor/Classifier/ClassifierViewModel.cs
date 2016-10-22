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
using System.Windows.Media;
using Yuml;
using Yuml.Command;
using static System.Diagnostics.Contracts.Contract;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// view model for interaction with a single classifier object
    /// </summary>
    internal class ClassifierViewModel : SingleItemViewModel<Classifier,ISingleClassifierCommands>
    {
        private readonly ExpandableMixin _expanded = new ExpandableMixin();
        private SelectClassifierWithNullItemMixin _selectBaseClass;
        private Color _backgroundColor;

        /// <summary>
        /// name of the base class which is intially set when reading
        /// data from view model. Used to choose the correct item in the classifier itemssource.
        /// Can be null or empty string in case no base class was set
        /// </summary>
        public string InitialBaseClass { get; set; }

        public ClassifierViewModel(
            ISingleClassifierCommands commands):base(commands){}

        public PropertyListViewModel Properties { get; private set; }
        public MethodListViewModel Methods { get; private set; }
        public AssociationListViewModel Associations { get; private set; }

        public bool IsExpanded
        {
            get { return _expanded.IsExpanded; }
            set { _expanded.IsExpanded = value; }
        }

        public void ExpandOrCollapse() => _expanded.ExpandOrCollapse();

        protected override void CustomInit()
        {
            Properties = 
                WithCommand(_commands.CommandsForProperties)
                .CreateViewModelForList<PropertyListViewModel>();
            Methods =
                WithCommand(_commands.CommandsForMethods)
                .CreateViewModelForList<MethodListViewModel>();
            Associations =
                WithCommand(_commands.CommandsForAssociations)
                .CreateViewModelForList<AssociationListViewModel>();
            
            // list of base classifiers can  have a null item and should not
            // have the class item itself
            _selectBaseClass = new SelectClassifierWithNullItemMixin(
                ClassifiersToSelect.Exclude(Name),
                _commands.ChangeBaseClass);

            _selectBaseClass.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
            _expanded.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);

            SelectClassifierByName(InitialBaseClass);
        }

        public IEnumerable<ClassifierItemViewModel> Classifiers => _selectBaseClass.Classifiers;
        public ClassifierItemViewModel SelectedClassifier
        {
            get { return _selectBaseClass.SelectedClassifier; }
            set { _selectBaseClass.SelectedClassifier = value; }
        }
        private void SelectClassifierByName(string classifierName) => 
            _selectBaseClass.SelectClassifierByName(classifierName);
        public void ClearClassifierWithoutCommand() => _selectBaseClass.ClearClassifierWithoutCommand();

        /// <summary>
        /// Reacts on the event that is raised when the base class of this
        /// classifier was set to null
        /// </summary>
        /// <param name="domainEvent"></param>
        public void OnBaseClassCleared(ClearBaseClassEvent domainEvent) => ClearClassifierWithoutCommand();

        /// <summary>
        /// background color used for this classifier
        /// </summary>
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                _commands.ChangeColor.ChangeColor(value.ToFriendlyName());
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// only used for initialization, will be used set initially during model => viewmodel mapping.
        /// If mapping would set Backgroundcolor directly, the command would be executed.
        /// </summary>
        public Color InitialColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; } 
        }
    }
}
