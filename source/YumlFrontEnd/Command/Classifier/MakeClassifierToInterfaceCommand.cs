using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    public class MakeClassifierToInterfaceCommand
    {
        private readonly Classifier _classifier;
        private readonly MessageSystem _messageSystem;

        public MakeClassifierToInterfaceCommand(Classifier classifier, MessageSystem messageSystem)
        {
            _classifier = classifier;
            _messageSystem = messageSystem;
        }

        public void ToggleInterfaceFlag()
        {
            var isInterface = _classifier.IsInterface;
            _classifier.IsInterface = !isInterface;
            // changed from interface => class
            if(isInterface)
                _messageSystem.Publish(_classifier,new InterfaceToClassEvent(_classifier.Name));
            // changed from class => interface
            if (!isInterface)
                _messageSystem.Publish(_classifier, new ClassToInterfaceEvent(_classifier.Name));
        }
    }
}
