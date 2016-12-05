using System.Collections.Generic;
using System.Windows.Media;
using Caliburn.Micro;
using UmlSketch.Command;
using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Editor
{
    /// <summary>
    /// view model for interaction with a single classifier object
    /// </summary>
    internal class ClassifierViewModel : SingleItemViewModelBase<Classifier,ISingleClassifierCommands>
    {
        private readonly ExpandableMixin _expanded = new ExpandableMixin();
        private SelectClassifierWithNullItemMixin _selectBaseClass;
        private BackgroundColorMixin _backgroundColor;
        private bool _isInterface;

        /// <summary>
        /// name of the base class which is intially set when reading
        /// data from view model. Used to choose the correct item in the classifier itemssource.
        /// Can be null or empty string in case no base class was set
        /// </summary>
        public string InitialBaseClass { get; set; }

        public InterfaceImplementationListViewModel Interfaces { get; private set; }
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

        /// <summary>
        /// we need to override Init here because we need to access the domain object
        /// directly, otherwise we would only override CustomInit
        /// </summary>
        /// <param name="domainObject"></param>
        /// <param name="parentViewModel"></param>
        public override void Init(
            Classifier domainObject, 
            PropertyChangedBase parentViewModel)
        {
            Note = new NoteViewModel(_commands.ChangeNoteColor, _commands.ChangeNoteText) { IsExpanded = false };

            base.Init(domainObject, parentViewModel);

            var factory = Context.ViewModelFactory;

            Properties = (PropertyListViewModel)factory.CreateListViewModel(domainObject.Properties);
            Methods = (MethodListViewModel)factory.CreateListViewModel(domainObject.Methods);
            Associations = (AssociationListViewModel)factory.CreateListViewModel(domainObject.Associations);
            Interfaces = (InterfaceImplementationListViewModel)factory.CreateListViewModel(domainObject.InterfaceImplementations);
        }

        protected override void CustomInit()
        {
            // list of base classifiers can  have a null item and should not
            // have the class item itself and also no void item
            _selectBaseClass = new SelectClassifierWithNullItemMixin(
                new BaseClassSelectionItemSource(Context.Classifiers, Name, Context.MessageSystem),
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

        /// <summary>
        /// Reacts on the event that is raised when the base class of this
        /// classifier was set to null
        /// </summary>
        /// <param name="domainEvent"></param>
        public void OnBaseClassCleared(ClearBaseClassEvent domainEvent) => _selectBaseClass.ClearClassifier();
        public void OnBaseClassChanged(BaseClassChangedEvent domainEvent) => _selectBaseClass.SelectClassifierByName(domainEvent.NameOfNewType);
        public void OnBaseClassSet(BaseClassSetEvent domainEvent) => _selectBaseClass.SelectClassifierByName(domainEvent.NameOfNewBaseClass);

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

        /// <summary>
        /// flag that will be set initially 
        /// </summary>
        public bool IsInterfaceInitial
        {
            get { return _isInterface; }
            set { _isInterface = value;NotifyOfPropertyChange(nameof(IsInterface)); }
        }

        public bool IsInterface
        {
            get { return _isInterface; }
            set
            {
                _commands.ChangeIsInterface.ToggleInterfaceFlag();
                _isInterface = value;
                NotifyOfPropertyChange();
            }
        }

        public void Collapse() => _expanded.Collapse();
    }
}
