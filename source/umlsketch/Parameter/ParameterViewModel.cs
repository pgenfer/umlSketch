using System.Collections.Generic;
using UmlSketch.Command;
using UmlSketch.DomainObject;

namespace UmlSketch.Editor
{
    public class ParameterViewModel : SingleItemViewModelBase<Parameter,SingleParameterCommandContext>
    {
        private SelectClassifierMixin _selectClassifier;

        /// <summary>
        /// name of the parameter type which is intially set when reading
        /// data from view model. Used to choose the correct item in the classifier itemssource
        /// </summary>
        public string InitialParameterType { get; set; }

        protected override void CustomInit()
        {
            _selectClassifier = new SelectClassifierMixin(
                Context.CreateClassifierItemSource(x => x.Name != SystemTypes.Void),
                _commands.ChangeType);
            _selectClassifier.PropertyChanged += (_, e) => NotifyOfPropertyChange(e.PropertyName);
            SelectClassifierByName(InitialParameterType);
        }

        public IEnumerable<ClassifierItemViewModel> Classifiers => _selectClassifier.Classifiers;
        public ClassifierItemViewModel SelectedClassifier
        {
            get { return _selectClassifier.SelectedClassifier; }
            set { _selectClassifier.SelectedClassifier = value; }
        }
        private void SelectClassifierByName(string classifierName) => 
            _selectClassifier.SelectClassifierByName(classifierName);
    }
}