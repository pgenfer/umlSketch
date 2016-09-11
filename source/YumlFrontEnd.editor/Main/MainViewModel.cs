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
            ClassifierListCommandContext classifierCommands,
            RelationList relations,
            ViewModelFactory viewModelFactory,
            ClassifierDictionary classifiers)
        {
            ClassifierList = viewModelFactory
                .WithCommand(classifierCommands)
                .CreateViewModelForList<ClassifierListViewModel>();
            Renderer = new RendererViewModel(
                classifiers, 
                relations,
                new ApplicationSettings(),
                viewModelFactory.MessageSystem);
        }

        public ClassifierListViewModel ClassifierList { get; private set; }
        public RendererViewModel Renderer { get; private set; }
    }
}
