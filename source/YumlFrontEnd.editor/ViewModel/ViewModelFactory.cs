using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;
using static System.Diagnostics.Contracts.Contract;

namespace YumlFrontEnd.editor.ViewModel
{
    // Problem is the following:
    // We need something like ViewModelFactory.Create<TViewModel,TDomain>(IListCommandContext<TDomain> commands) ...
    // So the method gets a commandlist and should return the view model.
    // The domain object type could be infered by the command object, but C# does not support partial type inference,
    // so we would have to type 
    // ViewModelFactory.Create<ClassifierViewModel,Classifier(commands)
    // which is not very pretty.
    // A suggestion is to split the type parameters into two calls, so that at least one could be infered,
    // See an example here:
    // http://stackoverflow.com/questions/16479623/working-around-lack-of-partial-generic-type-inference-with-constraints
    //
    // That's why we split the construction in
    // Factory.WithCommand(command).Create<TViewModel>();

    /// <summary>
    /// Factory used to create other view models
    /// </summary>
    internal class ViewModelFactory
    {
        /// <summary>
        /// list of classifiers that will be injected in all other view models
        /// </summary>
        public IClassifierSelectionItemsSource ClassifiersToSelect { get; }
        public MessageSystem MessageSystem { get; }

        /// <summary>
        /// first step of view model creation. The caller has to provide the commands
        /// for the view model, the domain type can be infered from the usage
        /// </summary>
        /// <typeparam name="TDomain"></typeparam>
        /// <param name="commands"></param>
        /// <returns>A factory that can be used to create the view model.</returns>
        public ViewModelFactory<TDomain> WithCommand<TDomain>(IListCommandContext<TDomain> commands) =>
            new ViewModelFactory<TDomain>(ClassifiersToSelect, commands,MessageSystem);
        

        public ViewModelFactory(
            IClassifierSelectionItemsSource classifiersToSelect,
            MessageSystem messageSystem)
        {
            Requires(classifiersToSelect != null);
            Requires(messageSystem != null);

            MessageSystem = messageSystem;
            ClassifiersToSelect = classifiersToSelect;
        }
    }

    /// <summary>
    /// factory that stores already the command information.
    /// Used for the second step where the view model will be created.
    /// Derives also from base viewmodel factory, so this factory can still
    /// be used to create other types of view models.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    internal class ViewModelFactory<TDomain> : ViewModelFactory
    {
        private readonly IListCommandContext<TDomain> _commands;
        /// <summary>
        /// factory function that is used to create single view model items for 
        /// the given domain type.
        /// Will be created on startup and can be used later to create view models for single items
        /// </summary>
        private Func<ISingleCommandContext, SingleItemViewModelBase<TDomain>>  _singleItemViewModelFactoryFunction;

        public ViewModelFactory(
            IClassifierSelectionItemsSource classifiersToSelect,
            IListCommandContext<TDomain> commands,
            MessageSystem messageSystem):base(classifiersToSelect,messageSystem)
        {
            _commands = commands;
            CreateFactoryFunctionForSingleViewModel();
        }

        /// <summary>
        /// creates a new instance of the given view model type
        /// by using the commands that are stored in this factory object.
        /// Currently, the viewmodel must have a constructor of the type
        /// ViewModel(IListCommandContext, FactoryWithCommand)
        /// </summary>
        /// <typeparam name="TViewModel">type of view model to create</typeparam>
        /// <returns>new view model instance</returns>
        public TViewModel CreateViewModelForList<TViewModel>() where TViewModel : ListViewModelBase<TDomain>
        {
            var viewModel = (TViewModel) Activator.CreateInstance(typeof(TViewModel), _commands);
            viewModel.Init(this);
            return viewModel;
        }

        /// <summary>
        /// creates a view model for a single domain object.
        /// The view model uses the given commands.
        /// </summary>
        /// <param name="commands">Commands which can be executed by this view model.</param>
        /// <returns></returns>
        public SingleItemViewModelBase<TDomain> CreateViewModelForSingleItem(ISingleCommandContext commands) =>
            _singleItemViewModelFactoryFunction(commands);

        private void CreateFactoryFunctionForSingleViewModel() 
        {
            // construct the base type of the view model by using the type of the domain object.
            var singleViewModelBaseType = typeof(SingleItemViewModelBase<>).MakeGenericType(typeof(TDomain));
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

            // if we have this constructor, get the concrete type of the command context and retrieve it
            if (constructor != null)
            {
                // store the constructor for later use
                _singleItemViewModelFactoryFunction = x =>
                    (SingleItemViewModelBase<TDomain>) constructor.Invoke(new object[] {x});
            }
            else
                throw new NotImplementedException(
                    $"There is no constructor for a single viewmodel for type {typeof(TDomain)} that takes a command context as parameter");
        }
    }
}
