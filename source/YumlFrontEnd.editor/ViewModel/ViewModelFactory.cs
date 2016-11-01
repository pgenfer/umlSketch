using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// Factory used to create other view models
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
        private readonly Dictionary<Type, Func<ISingleCommandContext, object>> _singleViewModelCreationFunctions =
                     new Dictionary<Type, Func<ISingleCommandContext, object>>();

        public ViewModelFactory(ViewModelContext context)
        {
            Contract.Requires(context != null);
            Context = context;
        }

        public TViewModel CreateListViewModel<TDomain, TViewModel>(IListCommandContext<TDomain> commands)
            where TViewModel : ListViewModelBase<TDomain>
        {
            var viewModel = (TViewModel)Activator.CreateInstance(typeof(TViewModel), commands);
            viewModel.Init(Context);
            return viewModel;
        }

        public SingleItemViewModelBase<TDomain> CreateViewModelForSingleItem<TDomain>(ISingleCommandContext commands)
        {
            var domainType = typeof(TDomain);
            // first check if we already have a factory for this type of domain object
            Func<ISingleCommandContext, object> factoryFunc;
            if (!_singleViewModelCreationFunctions.TryGetValue(domainType, out factoryFunc))
            {
                factoryFunc = CreateFactoryFunctionForSingleViewModel(domainType);
                _singleViewModelCreationFunctions.Add(domainType, factoryFunc);
            }

            var viewModel = (SingleItemViewModelBase<TDomain>) factoryFunc(commands);
            return viewModel;
        }

        private Func<ISingleCommandContext, object> CreateFactoryFunctionForSingleViewModel(Type domainType)
        {
            // construct the base type of the view model by using the type of the domain object.
            var singleViewModelBaseType = typeof(SingleItemViewModelBase<>).MakeGenericType(domainType);
            // get the first concrete type we could use for instantiating this view model
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

            // if we have the constructor, return it for later use
            if (constructor != null)
                return x => constructor.Invoke(new object[] { x });
            throw new NotImplementedException(
                    $"There is no constructor for a single viewmodel for type {domainType.Name} that takes a command context as parameter");
        }
    }
}