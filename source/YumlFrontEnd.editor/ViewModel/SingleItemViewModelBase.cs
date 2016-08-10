using Yuml;
using Yuml.Command;
using static System.Diagnostics.Contracts.Contract;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// base class for viewmodels used to interact with a single domain entity.
    /// </summary>
    /// <typeparam name="TDomain">type of the domain entitiy</typeparam>
    internal abstract class SingleItemViewModelBase<TDomain> : AutoPropertyChange
    {
        /// <summary>
        /// view model converter is used to
        /// convert any domain objects to view models.
        /// </summary>
        private readonly ViewModelConverter _toViewModel = new ViewModelConverter();

        protected readonly ISingleCommandContext _commands;
        private readonly EditableNameMixin _name;

        protected SingleItemViewModelBase(ISingleCommandContext commands)
        {
            _commands = commands;

            Requires(commands != null);

            _name = new EditableNameMixin(commands.Rename);

            // delegate events
            _name.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
         }

        public SingleItemViewModelBase<TDomain> Init(TDomain domain) => _toViewModel.InitViewModel(domain, this);

        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value; }
        }

        public bool IsEditable
        {
            get { return _name.IsEditable; }
            set { _name.IsEditable = value; }
        }

        public void StartEditing() => _name.StartEditing();
        public void StopEditing(Confirmation configuration) => _name.StopEditing(configuration);
        public override string ToString() => _name.ToString();

        public bool HasNameError
        {
            get { return _name.HasNameError; }
            set { _name.HasNameError = value; }
        }

        public string NameErrorMessage
        {
            get { return _name.NameErrorMessage; }
            set { _name.NameErrorMessage = value; }
        }
    }
}