using System;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Micro;
using UmlSketch.Command;
using UmlSketch.DomainObject;
using UmlSketch.Event;
using UmlSketch.Settings;

namespace UmlSketch.Editor
{
    internal class MainViewModel : Screen
    {
        private readonly Diagram _diagram;
        private readonly ApplicationSettings _applicationSettings;
        private readonly SerializationMixin _serialization;
        private readonly DiagramCommands _commands;
        private readonly ViewModelContext _context;

        public MainViewModel(
            DiagramCommands commands,
            Diagram diagram,
            ViewModelContext context,
            ApplicationSettings applicationSettings)
        {
            Contract.Requires(commands != null);
            Contract.Requires(diagram != null);
            Contract.Requires(applicationSettings != null);
            Contract.Requires(context != null);

            _context = context;
            _commands = commands;
            _diagram = diagram;
            _applicationSettings = applicationSettings;
            _serialization = new SerializationMixin(diagram, _context.MessageSystem,applicationSettings);

            // reload the view models when the classifiers are reset after
            // a load operation
            _context.MessageSystem.Subscribe<ClassifiersResetEvent>(diagram.Classifiers,_ => UpdateViewModels());

            // try to load the last file that was edited by the user
            LoadLastFile();

            DisplayName = EditorStrings.UmlSketch;
        }

        private void UpdateViewModels()
        {
            ClassifierList = (ClassifierListViewModel)_context.ViewModelFactory.CreateListViewModel<Classifier>(null);
            //  by default, collapse all classes
            ClassifierList.Collapse();

            Renderer = new RendererViewModel(
                _diagram,
                _applicationSettings,
                _context.MessageSystem);

            // initialize the note directly through the diagram
            // maybe later we change this to work via Automapper as we did with
            // other viewmodels
            Note = new NoteViewModel(_commands.ChangeNoteColor, _commands.ChangeNoteText)
            {
                InitialColor = _diagram.Note.Color.ToColorFromFriendlyName(),
                Text = _diagram.Note.Text,
                IsExpanded = false // by default, do not show the note
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
            private set { _note = value;NotifyOfPropertyChange(); }
        }

        public void Save() => _serialization.Save();
        public void Open() => _serialization.Open();
        public void New() => _serialization.New();
        public void LoadLastFile() => _serialization.LoadLastFile();

        public void Settings()
        {
            // create settings for dialog
            // TODO: this should be done by in a more platform independent way
            dynamic settings = new ExpandoObject();
            settings.WindowStartup = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            // open view model
            _context.WindowManager.ShowWindow(
                new ApplicationSettingsViewModel(
                    _applicationSettings,
                    _context.MessageSystem),
                settings:settings);
        }

        public void CheckCurrentEdit(MouseButtonEventArgs eventArgs)
        {
            var originalSource = eventArgs.OriginalSource as FrameworkElement;
            var editableName = originalSource?.DataContext as IEditableName;
            if (editableName != null)
            {
                if (editableName == _context.CurrentEdit)
                    return;
            }
            _context.CurrentEdit?.ValidateAndClose(); // validate and close any edit fields
        } 
    }
}
