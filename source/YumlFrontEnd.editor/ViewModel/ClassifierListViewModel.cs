using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YumlFrontEnd.editor
{
    internal class ClassifierListViewModel : PropertyChangedBase
    {
        private BindableCollectionMixin<ClassifierViewModel> _classifiers =
            new BindableCollectionMixin<ClassifierViewModel>();

        public ClassifierListViewModel()
        {
            Items.Add(new ClassifierViewModel { Name = "String" });               
            Items.Add(new ClassifierViewModel { Name = "Integer"});
            Items.Add(new ClassifierViewModel { Name = "CodeProvider"});
        }

        public BindableCollection<ClassifierViewModel> Items => _classifiers.Items;
    }
}
