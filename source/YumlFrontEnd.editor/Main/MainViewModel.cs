using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;
using YumlFrontEnd.editor.ViewModel;

namespace YumlFrontEnd.editor
{
    internal class MainViewModel : Screen
    {
        public MainViewModel(
            IListCommandContext<Classifier> classifierCommands,
            ViewModelFactory viewModelFactory)
        {
            ClassifierList = viewModelFactory
                .WithCommand(classifierCommands)
                .CreateViewModelForList<ClassifierListViewModel>();
        }

        public ClassifierListViewModel ClassifierList { get; private set; }
    }
}
