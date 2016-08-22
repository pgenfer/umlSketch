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
    internal class ListViewModelBase<TDomain> : AutoPropertyChange
    {
        /// <summary>
        /// every list view model has an expandable button for
        /// the child items. An additional flag controls
        /// whether this button should be visible or not
        /// </summary>
        private readonly ExpandableMixin _expandable = new ExpandableMixin();
        
        /// <summary>
        /// sub items in this list
        /// </summary>
        private readonly BindableCollectionMixin<SingleItemViewModelBase<TDomain>> _items =
           new BindableCollectionMixin<SingleItemViewModelBase<TDomain>>();
        /// <summary>
        /// source object to retrieve the available classifiers in the system
        /// </summary>
        private readonly ClassifierSelectionItemsSource _classifierItemsSource;

        protected readonly IListCommandContext<TDomain> _commands;

        public BindableCollection<SingleItemViewModelBase<TDomain>> Items => _items.Items;

        /// <summary>
        /// factory function used to create a view model for a single item.
        /// Will be initialized via reflection
        /// </summary>
        private Func<ISingleCommandContext,SingleItemViewModelBase<TDomain>> _singleItemViewFactory; 

        /// <summary>
        /// the view model is able to initialize itself
        /// by using reflection. For this, it does the following:
        /// 1.List commands are provided by constructor parameter
        /// 2. Find the single viewmodel for its domain objects (find by name and domain object type)
        /// 3. Find the commands that are used for this listview model (find by constructor parameter of the single view model)
        /// </summary>
        private void InitPerReflection()
        {
            // if we have no valid command structure,
            // we cannot iterate over the domain objects, so skip here
            if (_commands == null)
                return;

            var singleViewModelBaseType = typeof(SingleItemViewModelBase<>).MakeGenericType(typeof(TDomain));
            var concreteSingleViewModelType = GetType()
                .Assembly
                .GetTypes()
                .Where(x => singleViewModelBaseType.IsAssignableFrom(x))
                .FirstOrDefault(x => !x.IsAbstract);
            // now we have the type of the view model, try to find the type of commands
            // that is required by this view model
            // get the constructor that uses a command context as parameter
            var constructor = concreteSingleViewModelType?.GetConstructors()
                .FirstOrDefault(x => 
                    x.GetParameters().Count(p =>
                        typeof(ISingleCommandContext).IsAssignableFrom(p.ParameterType)) == 1);
            
            // if we have this constructor, get the concrete type of the command context and retrieve it
            if (constructor != null)
            {
                // store the constructor for later use
                _singleItemViewFactory = x => (SingleItemViewModelBase<TDomain>)constructor.Invoke(new object[] { x });
                UpdateItemList();
            }
        }

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
        protected void UpdateItemList()
        {
            Items.Clear();
            if (_singleItemViewFactory == null)
                return;

            // create single view models for every domain object
            foreach (var domainObject in _commands.All.Get())
            {
                // command context (as base interface, must be cast to concrete type)
                var singleCommandContextInstance = _commands.GetCommandsForSingleItem(domainObject);
                // use constructor to create the item and cast it to the correct type
                var singleViewModel = _singleItemViewFactory(singleCommandContextInstance);
                singleViewModel.Init(domainObject, _classifierItemsSource);
                Items.Add(singleViewModel);
            }

            // update the expand flag every time the list changes
            NotifyOfPropertyChange(nameof(CanExpand));
        }

        protected ListViewModelBase(
            IListCommandContext<TDomain> commands,
            ClassifierSelectionItemsSource classifierItemsSource)
        {
            _classifierItemsSource = classifierItemsSource;
            _commands = commands;
            InitPerReflection();
            _expandable.PropertyChanged += (_, e) => NotifyOfPropertyChange(e.PropertyName);
        }

        public bool IsExpanded => _expandable.IsExpanded;
        public void ExpandOrCollapse() => _expandable.ExpandOrCollapse();
        /// <summary>
        /// controls whether the expand button is visible
        /// </summary>
        public virtual bool CanExpand => Items.Any();
    }
}