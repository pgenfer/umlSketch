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
        private BackgroundColorMixin _backgroundColor;
  
        /// <summary>
        /// name of the base class which is intially set when reading
        /// data from view model. Used to choose the correct item in the classifier itemssource.
        /// Can be null or empty string in case no base class was set
        /// </summary>
        public string InitialBaseClass { get; set; }

        public ClassifierViewModel(
            ISingleClassifierCommands commands) : base(commands)
        {
            Note = new NoteViewModel(_commands.ChangeNoteColor,_commands.ChangeNoteText);
        }

        public PropertyListViewModel Properties { get; private set; }
        public MethodListViewModel Methods { get; private set; }
        public AssociationListViewModel Associations { get; private set; }
        public NoteViewModel Note { get; private set; }

        public bool IsExpanded
        {
            get { return _expanded.IsExpanded; }
            set { _expanded.IsExpanded = value; }
        }

        public void ExpandOrCollapse() => _expanded.ExpandOrCollapse();

        protected override void CustomInit()
        {
            var factory = Context.Factory;
            // TODO: we could use reflection to automatically find the correct type of list view models here
            Properties = factory.CreateListViewModel<Property, PropertyListViewModel>(_commands.CommandsForProperties);
            Methods = factory.CreateListViewModel<Method, MethodListViewModel>(_commands.CommandsForMethods);
            Associations = factory.CreateListViewModel<Relation,AssociationListViewModel>(_commands.CommandsForAssociations);
             
            // list of base classifiers can  have a null item and should not
            // have the class item itself and also no void item
            _selectBaseClass = new SelectClassifierWithNullItemMixin(
                Context.CreateClassifierItemSource(x => x.Name != Name && !x.IsSystemType,true),
                _commands.ChangeBaseClass);

            _backgroundColor = new BackgroundColorMixin(_commands.ChangeClassifierColor)
            {
                InitialColor = InitialColor
            };

            _selectBaseClass.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
            _expanded.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
            _backgroundColor.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);

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
        /// used to read the color from the domain object
        /// after all mixins are initialized, color will be set to mixin,
        /// so this property will not be delegated to the mixin directly
        /// </summary>
        public Color InitialColor { get; set; }

        public Color BackgroundColor
        {
            get { return _backgroundColor.BackgroundColor; }
            set { _backgroundColor.BackgroundColor = value; }
        }
        public void Collapse() => _expanded.Collapse();
    }
}
