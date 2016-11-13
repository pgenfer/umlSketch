using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Common;
using Yuml;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// ViewModelFactory used to create other view models
    /// </summary>
    public class ViewModelFactory
    {
        protected ViewModelContext Context { get; }
        /// <summary>
        /// dictionary stores functions that are used to create single view models.
        /// Since the factory function use reflection to find the correct view model and its constructor,
        /// the constructors are saved once they are created.
        /// Return type of functions is stored as object, because they depend on the TDomain parameter later,
        /// but the result will be casted explicitly.
        /// </summary>
        private readonly Dictionary<Type, Func<object>> _singleViewModelCreationFunctions =
                     new Dictionary<Type, Func<object>>();

        public ViewModelFactory(ViewModelContext context)
        {
            Contract.Requires(context != null);
            Context = context;
        }

        /// <summary>
        /// creates a list view model for the given domain object with the given available commands.
        /// </summary>
        /// <typeparam name="TDomain">type of the domain object for which the list view model should be created.</typeparam>
        /// <param name="domainList">the list of domain objects that are represented by this view model</param>
        /// <returns>the generated view model</returns>
        public ListViewModelBaseSimple<TDomain> CreateListViewModel<TDomain>(BaseList<TDomain> domainList)
            where TDomain : IVisible
        {
            dynamic viewModel = CreateListViewModelWithCommand<TDomain>();
            dynamic commands = Context.CommandFactory.GetListCommands(domainList);
            viewModel.InitCommands(commands);
            viewModel.Init(domainList, Context);
            return viewModel;
        }

        public SingleItemViewModelBaseSimple<TDomain> CreateSingleViewModel<TDomain>(
            TDomain domainObject,BaseList<TDomain> parentList) where TDomain : IVisible
        {
            Func<object> createFunc;
            if (!_singleViewModelCreationFunctions.TryGetValue(typeof(TDomain), out createFunc))
            {
                createFunc = CreateSingleViewModelFunction<TDomain>();
                _singleViewModelCreationFunctions.Add(typeof(TDomain), createFunc);
            }
            dynamic singleViewModel = (SingleItemViewModelBaseSimple<TDomain>)createFunc();
            dynamic commands = Context.CommandFactory.GetSingleCommands(domainObject,parentList);
            singleViewModel.InitCommands(commands);
            return singleViewModel;
        }

        /// <summary>
        /// constructs the required base type of the view model and searches an implementation of the type.
        /// Creates an instance of the view model type and returns it.
        /// </summary>
        /// <typeparam name="TDomain"></typeparam>
        /// <returns></returns>
        private ListViewModelBaseSimple<TDomain> CreateListViewModelWithCommand<TDomain>()
            where TDomain : IVisible =>
            // we execute the function immediately and return the resulting view model
            (ListViewModelBaseSimple<TDomain>)CreateViewModelFactoryFunctionForDomain(
                typeof(ListViewModelBase<,>)
                .MakeGenericType(typeof(TDomain), FindListDomainCommandsType<TDomain>()))();

        /// <summary>
        /// returns a function that can be used to create a new view model for a single domain item
        /// </summary>
        /// <typeparam name="TDomain"></typeparam>
        /// <returns></returns>
        private Func<object> CreateSingleViewModelFunction<TDomain>() =>
            CreateViewModelFactoryFunctionForDomain(typeof(SingleItemViewModelBase<,>)
                .MakeGenericType(typeof(TDomain), FindSingleDomainCommandsType<TDomain>()));

        /// <summary>
        /// given the type of a domain object, 
        /// this method returns the type of a single command context
        /// that can operate on domain objects of these types.
        /// Example:
        /// TDomain: Classifier
        /// Result: ClassifierSingleCommandContext
        /// </summary>
        /// <typeparam name="TDomain"></typeparam>
        /// <returns></returns>
        private static Type FindSingleDomainCommandsType<TDomain>() =>
            FindDomainCommandsType(typeof(ISingleCommandContext<TDomain>));

        private static Type FindListDomainCommandsType<TDomain>() =>
            FindDomainCommandsType(typeof(IListCommandContext<TDomain>));

        private static Type FindDomainCommandsType(Type commandInterface)
        {
            // find a command context that is suitable for this domain object.
            // search in the following order:
            // 1. if there is an interface, take the interface first
            // 2. if there is an specific implementation class without generics, take it 
            // 3. otherwise, take the generic interface
            var bestTypeGuess = commandInterface
                .Assembly
                .GetTypes()
                .Where(commandInterface.IsAssignableFrom)
                .FirstOrDefault(x => x.IsInterface) ?? 
                commandInterface
                    .Assembly
                    .GetTypes()
                    .Where(commandInterface.IsAssignableFrom)
                    .FirstOrDefault(x => !x.IsAbstract && x.GenericTypeArguments.Length == 0);
            return bestTypeGuess ?? commandInterface;
        }

        /// <summary>
        /// returns a function that can be called to create a view model.
        /// Can be used to obtain the factory function once and execute it several times
        /// (i.e. when a list of view models for single domain items must be created)
        /// </summary>
        /// <param name="viewModelBaseType"></param>
        /// <returns></returns>
        private Func<object> CreateViewModelFactoryFunctionForDomain(Type viewModelBaseType)
        {
            // get the first concrete type we could use for instantiating this view model
            var concreteViewModelType = GetType()
                .Assembly
                .GetTypes()
                .Where(viewModelBaseType.IsAssignableFrom)
                .FirstOrDefault(x => !x.IsAbstract);
            if (concreteViewModelType == null)
                throw new NotImplementedException(
                    $"There is no viewmodel implemented for base type {viewModelBaseType.ToFriendlyName()}");
            return () => Activator.CreateInstance(concreteViewModelType);
        }
    }
}