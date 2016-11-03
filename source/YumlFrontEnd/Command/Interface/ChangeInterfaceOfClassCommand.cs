using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Event;

namespace Yuml.Command.Interface
{
    public class ChangeInterfaceOfClassifierCommand : IChangeTypeCommand
    {
        private readonly Classifier _interfaceOwner;
        private readonly Implementation _existingInterface;
        private readonly ClassifierDictionary _classifiers;
        private readonly MessageSystem _messageSystem;

        public ChangeInterfaceOfClassifierCommand(
            Classifier interfaceOwner,
            Implementation existingInterface, 
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            _interfaceOwner = interfaceOwner;
            _existingInterface = existingInterface;
            _classifiers = classifiers;
            _messageSystem = messageSystem;
        }

        public void ChangeType(string nameOfOldType, string nameOfNewInterface)
        {
            var newInterface = _classifiers.FindByName(nameOfNewInterface);
            _interfaceOwner.InterfaceImplementations.ReplaceInterface(_existingInterface, newInterface);
            _messageSystem.Publish(
                _interfaceOwner,
                new ChangeInterfaceOfClassEvent(nameOfOldType, nameOfNewInterface));
        }
    }
}
