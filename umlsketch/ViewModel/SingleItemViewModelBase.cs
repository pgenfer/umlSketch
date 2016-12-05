using Common;
using UmlSketch.Command;
using UmlSketch.Event;
using static System.Diagnostics.Contracts.Contract;

namespace UmlSketch.Editor
{
    /// <summary>
    /// single generic base type for view models.
    /// Used in listview models when concrete type information of the command context
    /// is not necessary.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    public abstract class SingleItemViewModelBase<TDomain,TCommand> : 
        SingleItemViewModelBaseSimple<TDomain>, IEditableName
        where TDomain : IVisible
        where TCommand : ISingleCommandContext<TDomain>
    {
        protected TCommand _commands;

        /// <summary>
        /// view model converter is used to
        /// convert any domain objects to view models.
        /// </summary>
        private EditableNameMixin _name;
        private ChangeVisibilityMixin _changeVisibility;
       
        /// <summary>
        /// initialization method will be called when view model is created by the factory.
        /// Additional command initialization code can be added here.
        /// Don't forget to call base method when overriding.
        /// </summary>
        /// <param name="commands"></param>
        internal virtual void InitCommands(TCommand commands)
        {
            // command initialization can only
            // be called after context is set
            Requires(Context != null);

            _commands = commands;
            // setup mixins. There can be view models where the commands are not defined
            // (i.g. InterfaceImplementationViewModel does not need a rename command)
            // in that case the mixins should not be created
            if (commands.Rename != null)
            {
                _name = new EditableNameMixin(commands.Rename);
                _name.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
            }
            if (commands.Visibility != null)
            {
                _changeVisibility = new ChangeVisibilityMixin(commands.Visibility);
                _changeVisibility.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
            }
        }

        public void Delete()
        {
            _commands.Delete.DeleteItem();
        }

        /// <summary>
        /// reacts on changes of the visibility of the domain object.
        /// </summary>
        /// <param name="domainEvent"></param>
        public void OnVisibilityChanged(VisibilityChangedEvent domainEvent)
        {
            NotifyOfPropertyChange(nameof(IsVisible));
        }

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

        public void StartEditing()
        {
            Context.CurrentEdit = this;
            _name.StartEditing();
        }

        public void StopEditing(Confirmation configuration)
        {
            _name.StopEditing(configuration);
            if(!_name.IsEditable) // editing was stopped, so clear the current editable item
                Context.CurrentEdit = null;
        } 
        public override string ToString() => _name.ToString();
        public void ValidateAndClose() => _name.ValidateAndClose();

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

        public bool IsVisible => _changeVisibility.IsVisible;
        public void ShowOrHide()
        {
            _changeVisibility.ShowOrHide();   
            // since one of the child element has changed,
            // also update the visible state of the parent view model
            _parentViewModel.NotifyOfPropertyChange(nameof(IsVisible));
        }
    }
}