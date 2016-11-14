using System.Linq;
using Caliburn.Micro;
using Common;
using Yuml;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// base class for view models that represent a list of items.
    /// </summary>
    /// <typeparam name="TDomain">Type of domain objects within the list</typeparam>
    public abstract class ListViewModelBaseSimple<TDomain> : PropertyChangedBase
        where TDomain : IVisible // required for BaseList<T>
    {
        /// <summary>
        /// store a reference to the list of domain objects we
        /// host because we must give a reference of the list to the
        /// single domain items later (maybe we should put this dependency in the domain layer?)
        /// </summary>
        private BaseList<TDomain> _domainList;
        
        /// <summary>
        /// every list view model has an expandable button for
        /// the child items. An additional flag controls
        /// whether this button should be visible or not
        /// </summary>
        private readonly ExpandableMixin _expandable = new ExpandableMixin();
   
        /// <summary>
        /// sub items in this list
        /// </summary>
        private readonly BindableCollectionMixin<SingleItemViewModelBaseSimple<TDomain>> _items =
            new BindableCollectionMixin<SingleItemViewModelBaseSimple<TDomain>>();

        public BindableCollection<SingleItemViewModelBaseSimple<TDomain>> Items => _items.Items;

        public ViewModelContext Context { get; private set; }

        /// <summary>
        /// event handler that is fired when a new item was added to the list.
        /// A single view model will be created and added to the list
        /// </summary>
        /// <param name="itemCreated"></param>
        /// <returns></returns>
        protected virtual SingleItemViewModelBaseSimple<TDomain> OnNewItemAdded(
            DomainObjectCreatedEvent<TDomain> itemCreated)
        {
            var singleViewModel = CreateAndAddViewModel(itemCreated.DomainObject);
            // update the expand flag every time the list changes
            NotifyOfPropertyChange(nameof(CanExpand));
            return singleViewModel;
        }

        /// <summary>
        /// handler that reacts on the delete domain event of this item. 
        /// The view model for this item is searched and will be removed from the list
        /// of domain objects.
        /// </summary>
        /// <param name="domainEvent"></param>
        protected void OnSingleItemDeleted(DomainObjectDeletedEvent<TDomain> domainEvent)
        {
            var singleViewModel = Items.FirstOrDefault(x => x.RepresentsDomainObject(domainEvent.DomainObject));
            if (singleViewModel != null)
                RemoveItem(singleViewModel);
        }


        /// <summary>
        /// updates the list of available items by calling the query which is part of the
        /// command context
        /// </summary>
        protected abstract void UpdateItemList();

        protected SingleItemViewModelBaseSimple<TDomain> CreateAndAddViewModel(TDomain domainObject)
        {
            // we need dynamics here because the type of the command is not known
            // during runtime
            var singleViewModel = Context.ViewModelFactory.CreateSingleViewModel(domainObject,_domainList);
            singleViewModel.Init(domainObject, this, Context);
            Items.Add(singleViewModel);
            return singleViewModel;
        }

        internal void Init(BaseList<TDomain> domainList, ViewModelContext context)
        {
            _domainList = domainList;
            Context = context;
            _expandable.PropertyChanged += (_, e) => NotifyOfPropertyChange(e.PropertyName);
            UpdateItemList();
            SubscribeToMessageSystem(domainList);
        }

        protected virtual void SubscribeToMessageSystem(BaseList<TDomain> domainList)
        {
            Context.MessageSystem.Subscribe(domainList, this);
        }

        public bool IsExpanded => _expandable.IsExpanded;
        public void ExpandOrCollapse() => _expandable.ExpandOrCollapse();
        /// <summary>
        /// controls whether the expand button is visible
        /// </summary>
        public virtual bool CanExpand => Items.Any();

        private void RemoveItem(SingleItemViewModelBaseSimple<TDomain> item)
        {
            _items.RemoveItem(item);
            // the single view model should not receive any message any more
            Context.MessageSystem.Unsubscribe(item);
            if (Items.Count == 0) // last item was removed => hide expander
                NotifyOfPropertyChange(nameof(CanExpand));
        }
    }
}