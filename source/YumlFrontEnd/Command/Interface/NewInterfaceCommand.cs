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
            var addedInterface = _interfaceOwner.AddNewInterfaceEntryToList(_classifiers);
            if(addedInterface != null)
                _messageSystem.Publish(_interfaceOwner,new AddInterfaceToClassEvent(addedInterface.Name));
        }
    }
}
