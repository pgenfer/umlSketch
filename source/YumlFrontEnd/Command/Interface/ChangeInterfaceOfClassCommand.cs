using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Event;

namespace Yuml.Command.Interface
{
    public class ChangeInterfaceOfClassifierCommand
    {
        private readonly Classifier _interfaceOwner;
        private readonly Classifier _existingInterface;
        private readonly ClassifierDictionary _classifiers;
        private readonly MessageSystem _messageSystem;

        public ChangeInterfaceOfClassifierCommand(
            Classifier interfaceOwner,
            Classifier existingInterface, 
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            _interfaceOwner = interfaceOwner;
            _existingInterface = existingInterface;
            _classifiers = classifiers;
            _messageSystem = messageSystem;
        }

        public void Change(string nameOfNewInterface)
        {
            var newInterface = _classifiers.FindByName(nameOfNewInterface);
            _interfaceOwner.Interfaces.ReplaceInterface(_existingInterface, newInterface);
            _messageSystem.Publish(
                _interfaceOwner,
                new ChangeInterfaceOfClassEvent(_existingInterface.Name,nameOfNewInterface));
        }
    }
}
