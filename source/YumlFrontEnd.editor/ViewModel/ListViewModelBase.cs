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
        private readonly BindableCollectionMixin<SingleItemViewModelBase<TDomain>> _items =
           new BindableCollectionMixin<SingleItemViewModelBase<TDomain>>();

        protected readonly IListCommandContext<TDomain> _commands;

        public BindableCollection<SingleItemViewModelBase<TDomain>> Items => _items.Items;

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
                // create single view models for every domain object
                foreach (var domainObject in _commands.All.Get())
                {
                    // command context (as base interface, must be cast to concrete type)
                    var singleCommandContextInstance = _commands.GetCommandsForSingleItem(domainObject);
                    // use constructor to create the item and cast it to the correct type
                    var singleViewModel = (SingleItemViewModelBase<TDomain>) constructor.Invoke(
                        new object[] {singleCommandContextInstance});
                    singleViewModel.Init(domainObject);
                    Items.Add(singleViewModel);
                }
            }
        }

        protected ListViewModelBase(IListCommandContext<TDomain> commands)
        {
            _commands = commands;
            InitPerReflection();
        }
    }
}