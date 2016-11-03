using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    public class InterfaceImplementationViewModel : SingleItemViewModelBase<Implementation>
    {
        private readonly SingleInterfaceCommandContext _commands;

        public InterfaceImplementationViewModel(SingleInterfaceCommandContext commands) : base(commands)
        {
            _commands = commands;
        }

        protected override void CustomInit()
        {
            _selectClassifier = new SelectClassifierMixin(
            Context.CreateClassifierItemSource(x => x.IsInterface),
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
