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
        private readonly ImplementationList _implementations;
        private readonly ClassifierDictionary _classifiers;
        private readonly MessageSystem _messageSystem;

        public NewInterfaceCommand(
            ImplementationList implementations,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            _implementations = implementations;
            _classifiers = classifiers;
            _messageSystem = messageSystem;
        }

        public void CreateNew()
        {
            var addedInterface = _implementations.AddNewImplementation(_classifiers);
            if (addedInterface != null) // fire the event on the interface list (so the correct view model can react)
                _messageSystem.PublishCreated(_implementations, addedInterface);
        }
    }
}
