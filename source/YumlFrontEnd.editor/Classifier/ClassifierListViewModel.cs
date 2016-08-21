using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;


namespace YumlFrontEnd.editor
{
    /// <summary>
    /// view model for handling a complete list of classifiers.
    /// A separate view model is created for every classifier
    /// </summary>
    internal class ClassifierListViewModel : ListViewModelBase<Classifier>
    {
        public ClassifierListViewModel(
            IListCommandContext<Classifier> commands,
            ClassifierSelectionItemsSource classifierItemsSource) : base(commands,classifierItemsSource)
        {
        }
    }
}
