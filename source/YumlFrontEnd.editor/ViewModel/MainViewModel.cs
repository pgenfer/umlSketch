using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Commands;

namespace YumlFrontEnd.editor
{
    internal class MainViewModel : Screen
    {
        public MainViewModel(
            IValidateNameService validationService,
            ClassifierListCommands classifierCommands)
        {
            ClassifierList = new ClassifierListViewModel(validationService, classifierCommands);
        }

        public ClassifierListViewModel ClassifierList { get; private set; }
    }
}
