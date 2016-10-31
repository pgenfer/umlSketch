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
        private readonly Diagram _diagram;
        private readonly ApplicationSettings _applicationSettings;
        private readonly SerializationMixin _serialization;
        private readonly DiagramCommands _commands;

        public MainViewModel(
            ClassifierListCommandContext classifierCommands,
            DiagramCommands commands,
            ViewModelFactory viewModelFactory,
            Diagram diagram,
            ApplicationSettings applicationSettings)
        {
            Requires(commands != null);
            Requires(classifierCommands != null);
            Requires(viewModelFactory != null);
            Requires(diagram != null);
            Requires(applicationSettings != null);

            var messageSystem = viewModelFactory.MessageSystem;

            _classifierCommands = classifierCommands;
            _commands = commands;
            _viewModelFactory = viewModelFactory;
            _diagram = diagram;
            _applicationSettings = applicationSettings;
            _serialization = new SerializationMixin(diagram, messageSystem,applicationSettings);

            // reload the view models when the classifiers are reset after
            // a load operation
            messageSystem.Subscribe<ClassifiersResetEvent>(diagram.Classifiers,_ => UpdateViewModels());

            // try to load the last file that was edited by the user
            LoadLastFile();
        }

        private void UpdateViewModels()
        {
            ClassifierList = _viewModelFactory
                .WithCommand(_classifierCommands)
                .CreateViewModelForList<ClassifierListViewModel>();
            //  by default, collapse all classes
            ClassifierList.Collapse();

            Renderer = new RendererViewModel(
                _diagram,
                _applicationSettings,
                _viewModelFactory.MessageSystem);

            // initialize the note directly through the diagram
            // maybe later we change this to work via Automapper as we did with
            // other viewmodels
            Note = new NoteViewModel(_commands.ChangeNoteColor, _commands.ChangeNoteText)
            {
                InitialColor = _diagram.Note.Color.ToColorFromFriendlyName(),
                Text = _diagram.Note.Text
            };
        }

        private ClassifierListViewModel _classifierList;
        private RendererViewModel _renderer;
        private NoteViewModel _note;

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

        public NoteViewModel Note
        {
            get { return _note; }
            set { _note = value;NotifyOfPropertyChange(); }
        }

        public void Save() => _serialization.Save();
        public void Open() => _serialization.Open();
        public void New() => _serialization.New();
        public void LoadLastFile() => _serialization.LoadLastFile();
    }
}
