using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;
using Yuml;
using Yuml.Command;
using Yuml.Serializer;
using Yuml.Service;

namespace YumlFrontEnd.editor
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
            //ConventionManager.ApplyUpdateSourceTrigger = (propert, dependecyObject, binding,propertyInfo) => 
            //    binding.UpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged;
        }

        private void ConfigureViewModel()
        {
            _container.PerRequest<MainViewModel, MainViewModel>();
        }

        private static void CreateDummyData(
            ClassifierDictionary classifierDictionary,
            RelationList relations)
        {
            // just for debug
            var car = classifierDictionary.CreateNewClass("Car");
            var airplane = classifierDictionary.CreateNewClass("Airplane");
            var vehicle = classifierDictionary.CreateNewClass("Vehicle");
            var color = classifierDictionary.CreateNewClass("color");

            car.CreateProperty("Color", color);
            airplane.CreateProperty("Length", classifierDictionary.FindByName("int"));

            car.CreateMethod("Drive", classifierDictionary.FindByName("void"));
            airplane.CreateMethod("Fly", classifierDictionary.FindByName("void"));

            car.BaseClass = vehicle;
            var relation = car.AddNewRelation(color,RelationType.Aggregation);
            relations.AddRelation(relation);
        }

        private void ConfigureDomainModel()
        {
            var diagram = new Diagram();
            var classifierDictionary = diagram.Classifiers;
       
            _container.Instance(diagram);
            _container.Instance(classifierDictionary);

            _container.Singleton<MessageSystem>();
            _container.PerRequest<ClassifierListCommandContext>();
            _container.Singleton<DeletionService>();
            _container.Singleton<ViewModelContext>();
            _container.PerRequest<DiagramCommands>();

            // load application settings
            var applicationSettings = new ApplicationSettings();
            var settingsFilePath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "umlsketch.settings");
            applicationSettings.Load(settingsFilePath);
            _container.Instance(applicationSettings);
        }

        protected override object GetInstance(Type service, string key) => _container.GetInstance(service, key);
        protected override IEnumerable<object> GetAllInstances(Type service) => _container.GetAllInstances(service);
        protected override void BuildUp(object instance) => _container.BuildUp(instance);
    }
}
