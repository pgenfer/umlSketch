namespace YumlFrontEnd.editor
{
    /// <summary>
    /// base class for view models.
    /// </summary>
    /// <typeparam name="T">type of the command list that is available for
    /// this view model</typeparam>
    internal class ViewModelBase<T> : AutoPropertyChange
    {
        protected ViewModelBase(T commands)
        {
            _commands = commands;
        }

        /// <summary>
        /// commands that can be used by this view model
        /// </summary>
        protected readonly T _commands;

        /// <summary>
        /// view model converter is used to
        /// convert any domain objects to view models.
        /// </summary>
        private readonly ViewModelConverter _toViewModel = new ViewModelConverter();

        /// <summary>
        /// helper method to hide converter interaction.
        /// Can be called by derived classes whenever a domain object
        /// needs to be converted
        /// </summary>
        /// <typeparam name="TDomain">type of domain object</typeparam>
        /// <typeparam name="TViewModel">type of view model</typeparam>
        /// <param name="domain">domain object with the source data</param>
        /// <param name="viewModel">view model object which will receive the data</param>
        /// <returns></returns>
        protected TViewModel InitViewModel<TDomain, TViewModel>(TDomain domain, TViewModel viewModel) =>
            _toViewModel.InitViewModel(domain, viewModel);
    }
}