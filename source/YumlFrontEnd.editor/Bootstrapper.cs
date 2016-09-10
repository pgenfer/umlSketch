using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Yuml;
using Yuml.Command;
using Yuml.Notification;
using Yuml.Service;
using YumlFrontEnd.editor.ViewModel;

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

        private void CreateDummyData(ClassifierDictionary classifierDictionary)
        {
            // just for debug
            var @string = classifierDictionary.CreateNewClass("String");
            var integer = classifierDictionary.CreateNewClass("Integer");
            integer.CreateProperty("Size", integer);
            integer.CreateProperty("TypeName", @string);
            var codeProvider = classifierDictionary.CreateNewClass("CodeProvider");
            codeProvider.CreateMethod("DoSomething", integer);
            codeProvider.BaseClass = @string;
        }

        private void ConfigureDomainModel()
        {
            var classifierDictionary = new ClassifierDictionary();
            CreateDummyData(classifierDictionary);
            // register classifier dictionary
            _container.Instance(classifierDictionary);

            _container.Singleton<ClassifierNotificationService>();
            _container.Singleton<PropertyNotificationService>();
            _container.Singleton<MethodNotificationService>();
            _container.Singleton<NotificationServices>();

            _container.Singleton<ClassifierSelectionItemsSource>();

            _container.PerRequest<ClassifierListCommandContext>();
          
            _container.Singleton<DeletionService>();
            _container.Singleton<MessageSystem>();
            _container.Singleton<ViewModelFactory>();
        }

        protected override object GetInstance(Type service, string key) => _container.GetInstance(service, key);
        protected override IEnumerable<object> GetAllInstances(Type service) => _container.GetAllInstances(service);
        protected override void BuildUp(object instance) => _container.BuildUp(instance);
    }
}
