using Caliburn.Micro;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    internal class PropertyViewModel : SingleItemViewModelBase<Property,ISinglePropertyCommands>
    {
        private SelectClassifierMixin _selectClassifier;

        /// <summary>
        /// name of the property type which is intially set when reading
        /// data from view model. Used to choose the correct item in the classifier itemssource
        /// </summary>
        public string InitialPropertyType { get; set; }

        public PropertyViewModel(ISinglePropertyCommands commands):base(commands){}

        protected override void CustomInit()
        {
            _selectClassifier = new SelectClassifierMixin(
                ClassifiersToSelect,
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
