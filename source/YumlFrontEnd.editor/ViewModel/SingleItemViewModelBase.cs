using System;
using System.Collections.Generic;
using System.Linq;
using Yuml;
using Yuml.Command;
using YumlFrontEnd.editor.ViewModel;
using static System.Diagnostics.Contracts.Contract;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// Base class for view models that represent a single domain object.
    /// The type information of the available commands is a generic parameter,
    /// in that way the CustomInit method can always use the correct type of command parameters
    /// </summary>
    /// <typeparam name="TDomain">type of the domain entitiy</typeparam>
    /// <typeparam name="TSingleCommandContext">type of command context which is
    /// available for this single domain object</typeparam>
    internal abstract class SingleItemViewModelBase<TDomain,TSingleCommandContext> : SingleItemViewModelBase<TDomain> 
        where TSingleCommandContext : ISingleCommandContext
    {
        protected readonly TSingleCommandContext _commands;
     
        protected SingleItemViewModelBase(TSingleCommandContext commands) :base(commands)
        {
            _commands = commands;
        }
    }

    /// <summary>
    /// single generic base type for view models.
    /// Used in listview models when concrete type information of the command context
    /// is not necessary.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    internal abstract class SingleItemViewModelBase<TDomain> : AutoPropertyChange 
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

        protected SingleItemViewModelBase(ISingleCommandContext commands)
        {
            _commands = commands;
            Requires(commands != null);

            _name = new EditableNameMixin(commands.Rename);
            // delegate events
            _name.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
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
            _isRemoved = true;
        }

        private bool _isRemoved = false;

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
    }
}