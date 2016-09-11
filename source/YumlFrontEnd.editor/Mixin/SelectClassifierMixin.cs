using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yuml.Command;
using static System.Diagnostics.Contracts.Contract;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// mixin handles the interaction logic when selecting
    /// a classifier from a list of available classifiers
    /// </summary>
    public class SelectClassifierMixin : AutoPropertyChange
    {
        /// <summary>
        /// itemsource that contains the list of classifiers
        /// </summary>
        private readonly ClassifierSelectionItemsSource _itemsSource;
        /// <summary>
        /// command that will execute the domain logic when the user
        /// chooses a new classifier
        /// </summary>
        private readonly IChangeTypeCommand _command;

        /// <summary>
        /// the currently selected classifier
        /// </summary>
        protected ClassifierItemViewModel _selectedClassifier;

        public SelectClassifierMixin(
            ClassifierSelectionItemsSource itemsSource,
            IChangeTypeCommand command)
        {
            _itemsSource = itemsSource;
            _command = command;
        }

        public virtual void SelectClassifierByName(string classifierName)
        {
            SelectedClassifier = _itemsSource.ByName(classifierName);
        }

        public IEnumerable<ClassifierItemViewModel> Classifiers => _itemsSource;

        public virtual ClassifierItemViewModel SelectedClassifier
        {
            get { return _selectedClassifier; }
            set
            {
                // Problem:
                // 1. Classifier deleted
                // 2. All dependent properties / methods (and their view models) deleted
                // 3. ItemsSource receives event, removes classifier from list
                // 4. Classifier list is updated, ViewModel also gets update (although it is already removed from its parent)
                // 5. ViewModel sets classifier to null (if it was selected)
                // 6. Selected classifier would be null now.
                // 7. TODO: ensure that viewmodel does not receive any events after it was already disposed
                if (value == null)
                    return;

                var oldClassifier = _selectedClassifier;
                _selectedClassifier = value;
                // execute the command only if selected item was not set initially
                if(oldClassifier != null)
                    _command.ChangeType(oldClassifier.Name, value.Name);
                RaisePropertyChanged();
            }
        }
    }
}
