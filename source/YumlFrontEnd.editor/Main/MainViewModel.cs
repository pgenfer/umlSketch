using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    internal class MainViewModel : Screen
    {
        public MainViewModel(
            IListCommandContext<Classifier> classifierCommands,
            ClassifierSelectionItemsSource classifierItemSource)
        {
            ClassifierList = new ClassifierListViewModel(classifierCommands,classifierItemSource);
        }

        public ClassifierListViewModel ClassifierList { get; private set; }
    }
}
