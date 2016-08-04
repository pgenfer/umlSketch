using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;

namespace YumlFrontEnd.editor
{
    internal class ClassifierListViewModel : PropertyChangedBase
    {
        private BindableCollectionMixin<ClassifierViewModel> _classifiers =
            new BindableCollectionMixin<ClassifierViewModel>();
       

        public ClassifierListViewModel(ClassifierValidationService validationService)
        {
            Items.Add(new ClassifierViewModel(validationService) { Name = "String" });               
            Items.Add(new ClassifierViewModel(validationService) { Name = "Integer"});
            Items.Add(new ClassifierViewModel(validationService) { Name = "CodeProvider"});
        }

        public BindableCollection<ClassifierViewModel> Items => _classifiers.Items;
    }
}
