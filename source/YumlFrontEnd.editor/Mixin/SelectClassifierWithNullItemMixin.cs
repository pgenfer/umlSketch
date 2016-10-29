using Yuml.Command;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// handles selection of classifiers with an additional null item in the list.
    /// User can choose the null item if no classifier should be selected.
    /// </summary>
    public class SelectClassifierWithNullItemMixin : SelectClassifierMixin
    {
        private readonly IChangeTypeToNullCommand _command;

        public SelectClassifierWithNullItemMixin(
            IClassifierSelectionItemsSource itemsSource,
            IChangeTypeToNullCommand command):
                base(itemsSource,command)
        {
            _command = command;
        }

        public override void SelectClassifierByName(string classifierName)
        {
            if(string.IsNullOrEmpty(classifierName))
                SelectedClassifier = ClassifierItemViewModel.None;
            else
                base.SelectClassifierByName(classifierName);
        }

        /// <summary>
        /// call to clear the classifier in the combox box
        /// without executing the command. (e.g. if the base class of a class
        /// is deleted). 
        /// </summary>
        public void ClearClassifierWithoutCommand()
        {
            _selectedClassifier = ClassifierItemViewModel.None;
            // ReSharper disable once ExplicitCallerInfoArgument
            NotifyOfPropertyChange(nameof(SelectedClassifier));
        }

        public override ClassifierItemViewModel SelectedClassifier
        {
            get { return base.SelectedClassifier; }
            set
            {
                // if classifiers are reset, 
                // SelectedClassifer can be called with null
                if (value == null)
                    value = ClassifierItemViewModel.None;

                var oldClassifier = SelectedClassifier;
                _selectedClassifier = value;
                if (oldClassifier != null && oldClassifier != value)
                {
                    // type was changed from null -> new type
                    if (oldClassifier == ClassifierItemViewModel.None &&
                        value != ClassifierItemViewModel.None)
                        _command.SetNewType(value.Name);
                    // type was reset to a new value
                    else if (oldClassifier != ClassifierItemViewModel.None &&
                             value == ClassifierItemViewModel.None)
                        _command.ClearType(oldClassifier.Name);
                    // type was changed
                    else
                        _command.ChangeType(oldClassifier.Name, value.Name);
                }
                
                NotifyOfPropertyChange();
            }
        }
    }
}