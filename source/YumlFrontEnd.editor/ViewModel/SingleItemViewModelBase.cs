using System.Diagnostics.Contracts;
using Caliburn.Micro;
using Yuml;
using Yuml.Command;
using YumlFrontEnd.editor.ViewModel;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// single generic base type for view models.
    /// Used in listview models when concrete type information of the command context
    /// is not necessary.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    internal abstract class SingleItemViewModelBase<TDomain> : PropertyChangedBase
    {
        /// <summary>
        /// commands are stored in base class. Derived class of command is
        /// stored in derived class.
        /// TODO: would it make sense to separate all command relevant code into
        /// a separate class?
        /// </summary>
        private readonly ISingleCommandContext _commands;

        /// <summary>
        /// the view model that is used to host this single item view model.
        /// This reference is needed in case the single view model must notify
        /// its parent about any updates
        /// </summary>
        protected ListViewModelBase<TDomain> _parentViewModel;

        /// <summary>
        /// factory can be used to create other view models
        /// </summary>
        private ViewModelFactory _viewModelFactory;

        /// <summary>
        /// view model converter is used to
        /// convert any domain objects to view models.
        /// </summary>
        protected readonly ViewModelConverter _toViewModel = new ViewModelConverter();
        private readonly EditableNameMixin _name;
        private readonly ChangeVisibilityMixin _changeVisibility;

        protected SingleItemViewModelBase(ISingleCommandContext commands)
        {
            Contract.Requires(commands != null);

            _commands = commands;
            // setup mixins
            _name = new EditableNameMixin(commands.Rename);
            _changeVisibility = new ChangeVisibilityMixin(commands.Visibility);
            // forward mixin events
            _name.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
            _changeVisibility.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
        }

        /// <summary>
        /// called by owning list view model, initializes the single view model
        /// by setting all required properties.
        /// Custom initialization code should be implemented in CustomInit
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="viewModelFactory"></param>
        /// <param name="parentViewModel"></param>
        public virtual void Init(
            TDomain domain, 
            ViewModelFactory viewModelFactory,
            ListViewModelBase<TDomain> parentViewModel )
        {
            _viewModelFactory = viewModelFactory;
            _toViewModel.InitViewModel(domain, this);
            _parentViewModel = parentViewModel;
            // all event handlers in this view model will automatically be
            // registered at the message system
            viewModelFactory.MessageSystem.Subscribe(domain, this);
            CustomInit();
        }

        /// <summary>
        /// factory method to create other view models
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        public ViewModelFactory<TOtherDomain> WithCommand<TOtherDomain>(IListCommandContext<TOtherDomain> commands) =>
            _viewModelFactory.WithCommand(commands);

        /// <summary>
        /// custom initialization code can be implemented here.
        /// If no custom code is required, leave the implementation empty.
        /// </summary>
        protected abstract void CustomInit();
        public ClassifierSelectionItemsSource ClassifiersToSelect => _viewModelFactory.ClassifiersToSelect;

        public void Delete()
        {
            _commands.Delete.DeleteItem();
        }

        /// <summary>
        /// handler that reacts on the delete domain event of this item. When the event is raised,
        /// the parent view model will be notified so that is can delete this single view model from its list.
        /// </summary>
        /// <param name="domainEvent"></param>
        public void OnSingleItemDeleted(DomainObjectDeletedEvent<TDomain> domainEvent)
        {
            _parentViewModel.RemoveItem(this);
            _viewModelFactory.MessageSystem.Unsubscribe(this);
        }

        /// <summary>
        /// reacts on changes of the visibility of the domain object.
        /// </summary>
        /// <param name="domainEvent"></param>
        public void OnVisibilityChanged(VisibilityChangedEvent domainEvent)
        {
            NotifyOfPropertyChange(nameof(IsVisible));
        }

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
        public void StopEditing(Confirmation configuration) => _name.StopEditing(configuration);
        public override string ToString() => _name.ToString();

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

        public bool IsVisible => _changeVisibility.IsVisible;
        public void ShowOrHide()
        {
            _changeVisibility.ShowOrHide();   
            // since one of the child element has changed,
            // also update the visible state of the parent view model
            _parentViewModel.NotifyOfPropertyChange(nameof(IsVisible));
        }
    }
}