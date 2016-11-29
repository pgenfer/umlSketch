using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Caliburn.Micro;
using UmlSketch.Command;
using UmlSketch.DomainObject;
using UmlSketch.Event;
using UmlSketch.Service;
using UmlSketch.Settings;

namespace UmlSketch.Editor
{
    internal class Bootstrapper : BootstrapperBase
    {
        /// <summary>
        /// container used for dependency injection
        /// </summary>
        private SimpleContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e) => 
            DisplayRootViewFor<MainViewModel>();

        protected override void Configure()
        {
            _container = new SimpleContainer();
            _container.Singleton<IWindowManager, WindowManager>();

            ConfigureViewModel();
            ConfigureDomainModel();

            // register handler to convert the keyboard events
            // to correct confirmation result
            var confirmEditNameAction = new ConfirmEditAction();
            MessageBinder.SpecialValues.Add(
                confirmEditNameAction.ParameterName,
                confirmEditNameAction.ConvertKeyToConfirmation);

            // update all bindings when the property changes
            ConventionManager.ApplyUpdateSourceTrigger = (propert, dependecyObject, binding,propertyInfo) => 
                binding.UpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged;
        }

        private void ConfigureViewModel()
        {
            _container.PerRequest<MainViewModel>();
        }

        private void ConfigureDomainModel()
        {
            var diagram = new Diagram();
            var classifierDictionary = diagram.Classifiers;
       
            _container.Instance(diagram);
            _container.Instance(classifierDictionary);

            _container.Singleton<MessageSystem>();
            _container.Singleton<DeletionService>();
            _container.Singleton<IRelationService,RelationService>();
            _container.Singleton<IAskUserBeforeDeletionService, AskUserBeforeDeletionService>();
            _container.Singleton<ViewModelContext>();
            _container.Singleton<CommandFactory>();
            _container.PerRequest<DiagramCommands>();

            // load application settings
            var applicationSettings = new ApplicationSettings();
            var applicationSettingsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "umlsketch");
            if (!Directory.Exists(applicationSettingsPath))
            {
                try
                {
                    Directory.CreateDirectory(applicationSettingsPath);
                }
                catch (Exception)
                {
                    // could not create the application folder, so 
                    // use the appdata folder directly
                    applicationSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                }
            }
            var settingsFilePath = Path.Combine(applicationSettingsPath,"umlsketch.settings");
            applicationSettings.Load(settingsFilePath);
           
            _container.Instance(applicationSettings);
        }

        protected override object GetInstance(Type service, string key) => _container.GetInstance(service, key);
        protected override IEnumerable<object> GetAllInstances(Type service) => _container.GetAllInstances(service);
        protected override void BuildUp(object instance) => _container.BuildUp(instance);
    }
}
