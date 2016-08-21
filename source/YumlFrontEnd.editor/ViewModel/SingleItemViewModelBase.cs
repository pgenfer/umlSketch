using Yuml;
using Yuml.Command;
using static System.Diagnostics.Contracts.Contract;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// Base class for view models that represent a single domain object.
    /// The type information of the available commands is a generic parameter,
    /// in that way the CustomInit method can always be called with the correct type of command parameters
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

        /// <summary>
        /// called by owning list view model, initializes the single view model
        /// by setting all required properties.
        /// Custom initialization code should be implemented in CustomInit
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="classifierItemsSource"></param>
        public override void Init(TDomain domain, ClassifierSelectionItemsSource classifierItemsSource)
        {
            _toViewModel.InitViewModel(domain, this);
            CustomInit(_commands, classifierItemsSource);
        }

        /// <summary>
        /// custom initialization code can be implemented here.
        /// If no custom code is required, leave the implementation empty.
        /// </summary>
        /// <param name="commandContext"></param>
        /// <param name="classifierItemSource"></param>
        protected abstract void CustomInit(
            TSingleCommandContext commandContext,
            ClassifierSelectionItemsSource classifierItemSource);
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
        /// view model converter is used to
        /// convert any domain objects to view models.
        /// </summary>
        protected readonly ViewModelConverter _toViewModel = new ViewModelConverter();
        private readonly EditableNameMixin _name;

        protected SingleItemViewModelBase(ISingleCommandContext commands)
        {
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
        /// <param name="classifierItemsSource"></param>
        public abstract void Init(TDomain domain, ClassifierSelectionItemsSource classifierItemsSource);

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