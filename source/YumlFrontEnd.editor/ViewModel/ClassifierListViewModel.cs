using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Commands;


namespace YumlFrontEnd.editor
{
    /// <summary>
    /// view model for handling a complete list of classifiers.
    /// A separate view model is created for every classifier
    /// </summary>
    internal class ClassifierListViewModel : ViewModelBase<IClassifierListCommands>
    {
        private readonly BindableCollectionMixin<ClassifierViewModel> _classifiers =
            new BindableCollectionMixin<ClassifierViewModel>();

        public ClassifierListViewModel(
            IValidateNameService validationService,
            IClassifierListCommands commands) : base(commands)
        {
            // create child view models and add them to the items list of
            // this view model
            Items.AddRange(
                _commands
                    .QueryAllClassifiers
                    .Select(x =>
                        InitViewModel(x,
                            new ClassifierViewModel(
                                validationService,
                                commands.GetCommandsForClassifier(x)))));
        }

        public BindableCollection<ClassifierViewModel> Items => _classifiers.Items;
    }
}
