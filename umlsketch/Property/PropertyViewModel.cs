using System.Collections.Generic;
using UmlSketch.Command;
using UmlSketch.DomainObject;

namespace UmlSketch.Editor
{
    internal class PropertyViewModel : SingleItemViewModelBase<Property,ISinglePropertyCommands>
    {
        private SelectClassifierMixin _selectClassifier;

        /// <summary>
        /// name of the property type which is intially set when reading
        /// data from view model. Used to choose the correct item in the classifier itemssource
        /// </summary>
        public string InitialPropertyType { get; set; }

        protected override void CustomInit()
        {
            _selectClassifier = new SelectClassifierMixin(
                Context.CreateClassifierItemSource(x => x.Name != SystemTypes.Void),
                _commands.ChangeTypeOfProperty);
            _selectClassifier.PropertyChanged += (_, e) => NotifyOfPropertyChange(e.PropertyName);
            SelectClassifierByName(InitialPropertyType);
        }

        public IEnumerable<ClassifierItemViewModel> Classifiers => _selectClassifier.Classifiers;
        public ClassifierItemViewModel SelectedClassifier
        {
            get { return _selectClassifier.SelectedClassifier; }
            set { _selectClassifier.SelectedClassifier = value; }
        }
        private void SelectClassifierByName(string classifierName) => _selectClassifier.SelectClassifierByName(classifierName);
    }
}
