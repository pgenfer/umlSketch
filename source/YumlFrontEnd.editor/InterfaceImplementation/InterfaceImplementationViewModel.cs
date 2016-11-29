using System.Collections.Generic;
using UmlSketch.Command;
using UmlSketch.DomainObject;

namespace UmlSketch.Editor
{
    public class InterfaceImplementationViewModel : SingleItemViewModelBase<Implementation, SingleInterfaceCommandContext>
    {
        protected override void CustomInit()
        {
            _selectClassifier = new SelectClassifierMixin(
                new InterfaceSelectionItemsSource(
                    Context.Classifiers,
                    _commands.AvailableInterfaces,
                    Context.MessageSystem),
                _commands.ChangeInterface);
            _selectClassifier.PropertyChanged += (_, e) => NotifyOfPropertyChange(e.PropertyName);
            SelectClassifierByName(InitialInterfaceImplementation);
        }

        private SelectClassifierMixin _selectClassifier;

        /// <summary>
        /// name of the property type which is intially set when reading
        /// data from view model. Used to choose the correct item in the classifier itemssource
        /// </summary>
        public string InitialInterfaceImplementation { get; set; }

        public IEnumerable<ClassifierItemViewModel> Classifiers => _selectClassifier.Classifiers;
        public ClassifierItemViewModel SelectedClassifier
        {
            get { return _selectClassifier.SelectedClassifier; }
            set { _selectClassifier.SelectedClassifier = value; }
        }
        private void SelectClassifierByName(string classifierName) => _selectClassifier.SelectClassifierByName(classifierName);
    }
}
