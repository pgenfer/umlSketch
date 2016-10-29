using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using Yuml;
using Yuml.Command;
using Yuml.Serializer;
using YumlFrontEnd.editor.ViewModel;
using static System.Environment;
using static System.Diagnostics.Contracts.Contract;

namespace YumlFrontEnd.editor
{
    internal class MainViewModel : Screen
    {
        private readonly ClassifierListCommandContext _classifierCommands;
        private readonly ViewModelFactory _viewModelFactory;
        private readonly ClassifierDictionary _classifiers;
        private readonly ApplicationSettings _applicationSettings;
        private readonly SerializationMixin _serialization;

        public MainViewModel(
            ClassifierListCommandContext classifierCommands,
            ViewModelFactory viewModelFactory,
            ClassifierDictionary classifiers,
            ApplicationSettings applicationSettings)
        {
            Requires(classifierCommands != null);
            Requires(viewModelFactory != null);
            Requires(classifiers != null);
            Requires(applicationSettings != null);

            var messageSystem = viewModelFactory.MessageSystem;

            _classifierCommands = classifierCommands;
            _viewModelFactory = viewModelFactory;
            _classifiers = classifiers;
            _applicationSettings = applicationSettings;
            _serialization = new SerializationMixin(classifiers, messageSystem,applicationSettings);

            // reload the view models when the classifiers are reset after
            // a load operation
            messageSystem.Subscribe<ClassifiersResetEvent>(_classifiers,_ => UpdateViewModels());

            // try to load the last file that was edited by the user
            LoadLastFile();
        }

        private void UpdateViewModels()
        {
            ClassifierList = _viewModelFactory
                .WithCommand(_classifierCommands)
                .CreateViewModelForList<ClassifierListViewModel>();
            Renderer = new RendererViewModel(
                _classifiers,
                _applicationSettings,
                _viewModelFactory.MessageSystem);
        }

        private ClassifierListViewModel _classifierList;
        private RendererViewModel _renderer;

        public ClassifierListViewModel ClassifierList
        {
            get { return _classifierList; }
            private set { _classifierList = value;NotifyOfPropertyChange(); }
        }

        public RendererViewModel Renderer
        {
            get { return _renderer; }
            private set { _renderer = value;NotifyOfPropertyChange(); }
        }
        public void Save() => _serialization.Save();
        public void Open() => _serialization.Open();
        public void New() => _serialization.New();
        public void LoadLastFile() => _serialization.LoadLastFile();
    }
}
