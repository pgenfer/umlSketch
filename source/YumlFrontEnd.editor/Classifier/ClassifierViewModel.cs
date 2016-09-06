using Caliburn.Micro;
using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Yuml;
using Yuml.Command;
using static System.Diagnostics.Contracts.Contract;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// view model for interaction with a single classifier object
    /// </summary>
    internal class ClassifierViewModel : SingleItemViewModelBase<Classifier,ISingleClassifierCommands>
    {
        private readonly ExpandableMixin _expanded = new ExpandableMixin();
        private SelectClassifierMixin _selectBaseClass;

        /// <summary>
        /// name of the base class which is intially set when reading
        /// data from view model. Used to choose the correct item in the classifier itemssource.
        /// Can be null or empty string in case no base class was set
        /// </summary>
        public string InitialBaseClass { get; set; }

        public ClassifierViewModel(
            ISingleClassifierCommands commands):base(commands){}

        public PropertyListViewModel Properties { get; private set; }
        public MethodListViewModel Methods { get; private set; }

        public bool IsExpanded => _expanded.IsExpanded;
        public void ExpandOrCollapse() => _expanded.ExpandOrCollapse();

        protected override void CustomInit(
            ISingleClassifierCommands commandContext, 
            ClassifierSelectionItemsSource classifierItemSource)
        {
            Properties = new PropertyListViewModel(
                commandContext.CommandsForProperties, 
                classifierItemSource);
            Methods = new MethodListViewModel(
                commandContext.CommandsForMethods, 
                classifierItemSource);

            // list of base classifiers can  have a null item and should not
            // have the class item itself
            _selectBaseClass = new SelectClassifierWithNullItemMixin(
                classifierItemSource.Exclude(Name),
                commandContext.ChangeBaseClass);

            _selectBaseClass.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
            _expanded.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);

            SelectClassifierByName(InitialBaseClass);
        }

        public IEnumerable<ClassifierItemViewModel> Classifiers => _selectBaseClass.Classifiers;
        public ClassifierItemViewModel SelectedClassifier
        {
            get { return _selectBaseClass.SelectedClassifier; }
            set { _selectBaseClass.SelectedClassifier = value; }
        }
        private void SelectClassifierByName(string classifierName) => 
            _selectBaseClass.SelectClassifierByName(classifierName);
    }
}
