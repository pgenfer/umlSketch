using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    internal class MethodViewModel : SingleItemViewModelBase<Method,MethodSingleCommandContext>
    {
        private SelectClassifierMixin _selectClassifierForReturnType;

        /// <summary>
        /// name of the property type which is intially set when reading
        /// data from view model. Used to choose the correct item in the classifier itemssource
        /// </summary>
        public string InitialReturnType { get; set; }

        protected override void CustomInit()
        {
            _selectClassifierForReturnType = new SelectClassifierMixin(
                Context.CreateClassifierItemSource(x => true), // all types can be return types
                _commands.ChangeReturnType);
            _selectClassifierForReturnType.PropertyChanged += (_, e) => NotifyOfPropertyChange(e.PropertyName);
            SelectClassifierByName(InitialReturnType);
        }

        public IEnumerable<ClassifierItemViewModel> Classifiers => _selectClassifierForReturnType.Classifiers;
        public ClassifierItemViewModel SelectedClassifier
        {
            get { return _selectClassifierForReturnType.SelectedClassifier; }
            set { _selectClassifierForReturnType.SelectedClassifier = value; }
        }
        private void SelectClassifierByName(string classifierName) => _selectClassifierForReturnType.SelectClassifierByName(classifierName);
    }
}
