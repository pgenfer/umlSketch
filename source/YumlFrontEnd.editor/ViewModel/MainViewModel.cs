using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;

namespace YumlFrontEnd.editor
{
    internal class MainViewModel : Screen
    {
        public MainViewModel(ClassifierValidationService validationService)
        {
            ClassifierList = new ClassifierListViewModel(validationService);
        }

        public ClassifierListViewModel ClassifierList { get; private set; }
    }
}
