using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Yuml;
using Yuml.Command;



namespace YumlFrontEnd.editor
{
    /// <summary>
    /// base class for view models that represent a list of items.
    /// </summary>
    /// <typeparam name="TDomain">Type of domain objects within the list</typeparam>
    public class ListViewModelBase<TDomain> : PropertyChangedBase
    {
        /// <summary>
        /// every list view model has an expandable button for
        /// the child items. An additional flag controls
        /// whether this button should be visible or not
        /// </summary>
        private readonly ExpandableMixin _expandable = new ExpandableMixin();
        private readonly ChangeVisibilityMixin _visibility;
        
        /// <summary>
        /// sub items in this list
        /// </summary>
        private readonly BindableCollectionMixin<SingleItemViewModelBase<TDomain>> _items =
           new BindableCollectionMixin<SingleItemViewModelBase<TDomain>>();
    
        protected readonly IListCommandContext<TDomain> _commands;

        public BindableCollection<SingleItemViewModelBase<TDomain>> Items => _items.Items;

        public ViewModelContext Context { get; private set; }

        public void New()
        {
            // execute the command
            _commands.New.CreateNew();
            // update the list of items to reflect the changes
            UpdateItemList();
        }

        /// <summary>
        /// updates the list of available items by calling the query which is part of the
        /// command context
        /// </summary>
        protected virtual void UpdateItemList()
        {
            Items.Clear();
           
            // create single view models for every domain object
            foreach (var domainObject in _commands.All.Get())
            {
                // command context (as base interface, must be cast to concrete type)
                var singleCommandContextInstance = _commands.GetCommandsForSingleItem(domainObject);
                // use constructor to create the item and cast it to the correct type
                var singleViewModel = Context.Factory.CreateViewModelForSingleItem<TDomain>(singleCommandContextInstance);
                singleViewModel.Init(domainObject,this);
                Items.Add(singleViewModel);
            }

            // update the expand flag every time the list changes
            NotifyOfPropertyChange(nameof(CanExpand));
        }

        protected ListViewModelBase(IListCommandContext<TDomain> commands)
        {
            _commands = commands;
            // if command is not available, list view does not support visibility
            if (commands.Visibility != null)
               _visibility = new ChangeVisibilityMixin(commands.Visibility);
        }

        internal void Init(ViewModelContext context)
        {
            Context = context;
            _expandable.PropertyChanged += (_, e) => NotifyOfPropertyChange(e.PropertyName);
            UpdateItemList();
        }

        public bool IsExpanded => _expandable.IsExpanded;
        public void ExpandOrCollapse() => _expandable.ExpandOrCollapse();
        /// <summary>
        /// controls whether the expand button is visible
        /// </summary>
        public virtual bool CanExpand => Items.Any();
        public void RemoveItem(SingleItemViewModelBase<TDomain> item)
        {
            _items.RemoveItem(item);
            if (Items.Count == 0) // last item was removed => hide expander
                NotifyOfPropertyChange(nameof(CanExpand));
        } 

        public bool IsVisible => _visibility.IsVisible;

        public void ShowOrHide()
        {
            _visibility.ShowOrHide();
            NotifyOfPropertyChange(nameof(IsVisible));
        }
    }
}