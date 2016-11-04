using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Event;

namespace Yuml.Command
{
    public class NewInterfaceCommand : INewCommand
    {
        private readonly Classifier _interfaceOwner;
        private readonly ClassifierDictionary _classifiers;
        private readonly MessageSystem _messageSystem;

        public NewInterfaceCommand(
            Classifier interfaceOwner,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            _interfaceOwner = interfaceOwner;
            _classifiers = classifiers;
            _messageSystem = messageSystem;
        }

        public void CreateNew()
        {
            var addedInterface = _interfaceOwner.AddNewImplementation(_classifiers);
            if (addedInterface != null) // fire the event on the interface list (so the correct view model can react)
                _messageSystem.PublishCreated(_interfaceOwner.InterfaceImplementations, addedInterface);
        }
    }
}
