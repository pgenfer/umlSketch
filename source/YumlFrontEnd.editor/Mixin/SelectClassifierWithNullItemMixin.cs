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
            ClassifierSelectionItemsSource itemsSource,IChangeTypeToNullCommand command):
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

        public override ClassifierItemViewModel SelectedClassifier
        {
            get { return base.SelectedClassifier; }
            set
            {
                var oldClassifier = SelectedClassifier;
                _selectedClassifier = value;
                if (oldClassifier != null)
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
                
                RaisePropertyChanged();
            }
        }
    }
}