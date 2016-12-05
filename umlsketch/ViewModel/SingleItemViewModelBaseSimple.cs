using Caliburn.Micro;
using static System.Diagnostics.Contracts.Contract;

namespace UmlSketch.Editor
{
    /// <summary>
    /// single generic base type for view models.
    /// Used in listview models when concrete type information of the command context
    /// is not necessary.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class SingleItemViewModelBaseSimple<TDomain> : PropertyChangedBase
    {
        /// <summary>
        /// reference to the original domain object.
        /// Needed so that we can map the view model to the domain object later
        /// </summary>
        private TDomain _domainObject;

        public ViewModelContext Context { protected get; set; }
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
        public virtual void Init(
            TDomain domain,
            PropertyChangedBase parentViewModel)
        {
            // ensure that initialization can only be called
            // after the context is set
            Requires(Context != null);

            _domainObject = domain;
            _toViewModel.InitViewModel(domain, this);
            _parentViewModel = parentViewModel;
            Context.MessageSystem.Subscribe(domain, this);
            CustomInit();
        }
        /// <summary>
        /// custom initialization code can be implemented here.
        /// If no custom code is required, leave the implementation empty.
        /// </summary>
        protected abstract void CustomInit();

        /// <summary>
        /// returns true if this view model is a representation of the given domain object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="domainObject"></param>
        /// <returns></returns>
        public bool RepresentsDomainObject<T>(T domainObject) => _domainObject.Equals(domainObject);
    }
}