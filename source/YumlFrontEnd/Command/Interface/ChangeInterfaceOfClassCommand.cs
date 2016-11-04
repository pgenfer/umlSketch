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
        private readonly ImplementationList _interfaceImplementationList;
        private readonly Implementation _existingInterface;
        private readonly ClassifierDictionary _classifiers;
        private readonly MessageSystem _messageSystem;

        public ChangeInterfaceOfClassifierCommand(
            ImplementationList interfaceImplementationList,
            Implementation existingInterface, 
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            _interfaceImplementationList = interfaceImplementationList;
            _existingInterface = existingInterface;
            _classifiers = classifiers;
            _messageSystem = messageSystem;
        }

        public void ChangeType(string nameOfOldType, string nameOfNewInterface)
        {
            var newInterface = _classifiers.FindByName(nameOfNewInterface);
            _interfaceImplementationList.ReplaceInterface(_existingInterface, newInterface);
            _messageSystem.Publish(
                _interfaceImplementationList,
                new ChangeInterfaceOfClassEvent(nameOfOldType, nameOfNewInterface));
        }
    }
}
