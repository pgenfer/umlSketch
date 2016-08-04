using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YumlFrontEnd.editor
{
    internal class MainViewModel : PropertyChangedBase
    {
        public MainViewModel()
        {
            ClassifierList = new ClassifierListViewModel();
        }

        public ClassifierListViewModel ClassifierList { get; private set; }
    }
}
