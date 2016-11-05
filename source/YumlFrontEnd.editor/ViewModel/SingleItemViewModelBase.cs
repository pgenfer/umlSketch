using System.Diagnostics.Contracts;
using Caliburn.Micro;
using Yuml;
using Yuml.Command;
using YumlFrontEnd.editor;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// single generic base type for view models.
    /// Used in listview models when concrete type information of the command context
    /// is not necessary.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class SingleItemViewModelBase<TDomain> : PropertyChangedBase
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

        public ViewModelContext Context { get; private set; }

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
            // setup mixins. There can be view models where the commands are not defined
            // (i.g. InterfaceImplementationViewModel does not need a rename command)
            // in that case the mixins should not be created
            if (commands.Rename != null)
            {
                _name = new EditableNameMixin(commands.Rename);
                _name.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
            }
            if (commands.Visibility != null)
            {
                _changeVisibility = new ChangeVisibilityMixin(commands.Visibility);
                _changeVisibility.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
            }
        }

        /// <summary>
        /// called by owning list view model, initializes the single view model
        /// by setting all required properties.
        /// Custom initialization code should be implemented in CustomInit
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="parentViewModel"></param>
        public virtual void Init(
            TDomain domain,
            ListViewModelBase<TDomain> parentViewModel )
        {
            _toViewModel.InitViewModel(domain, this);
            _parentViewModel = parentViewModel;
            // all event handlers in this view model will automatically be
            // registered at the message system
            Context = parentViewModel.Context;
            Context.MessageSystem.Subscribe(domain, this);
            CustomInit();
        }

        /// <summary>
        /// custom initialization code can be implemented here.
        /// If no custom code is required, leave the implementation empty.
        /// </summary>
        protected abstract void CustomInit();
     
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
            Context.MessageSystem.Unsubscribe(this);
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