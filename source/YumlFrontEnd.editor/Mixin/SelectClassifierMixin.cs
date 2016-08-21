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
    internal class SelectClassifierMixin : AutoPropertyChange
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
        private ClassifierItemViewModel _selectedClassifier;

        public SelectClassifierMixin(
            ClassifierSelectionItemsSource itemsSource,
            IChangeTypeCommand command)
        {
            _itemsSource = itemsSource;
            _command = command;
        }

        public void SelectClassifierByName(string classifierName) => SelectedClassifier = _itemsSource.ByName(classifierName);

        public IEnumerable<ClassifierItemViewModel> Classifiers => _itemsSource;

        public ClassifierItemViewModel SelectedClassifier
        {
            get { return _selectedClassifier; }
            set
            {
                Requires(value != null);

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
