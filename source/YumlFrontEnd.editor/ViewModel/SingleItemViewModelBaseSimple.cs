using Caliburn.Micro;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// single generic base type for view models.
    /// Used in listview models when concrete type information of the command context
    /// is not necessary.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class SingleItemViewModelBaseSimple<TDomain> : PropertyChangedBase
    {
        public ViewModelContext Context { get; private set; }
        /// <summary>
        /// view model converter is used to
        /// convert any domain objects to view models.
        /// </summary>
        protected readonly ViewModelConverter _toViewModel = new ViewModelConverter();
        /// <summary>
        /// needed to fire notifications for the parent view model (e.g. when all items are invisible)
        /// </summary>
        protected PropertyChangedBase _parentViewModel;
        /// <summary>
        /// called by owning list view model, initializes the single view model
        /// by setting all required properties.
        /// Custom initialization code should be implemented in CustomInit
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="parentViewModel"></param>
        /// <param name="context"></param>
        public virtual void Init(
            TDomain domain,
            PropertyChangedBase parentViewModel,
            ViewModelContext context)
        {
            _toViewModel.InitViewModel(domain, this);
            _parentViewModel = parentViewModel;
            Context = context;
            Context.MessageSystem.Subscribe(domain, this);
            CustomInit();
        }
        /// <summary>
        /// custom initialization code can be implemented here.
        /// If no custom code is required, leave the implementation empty.
        /// </summary>
        protected abstract void CustomInit();
    }
}