using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    internal class InterfaceListCommandContext : ListCommandContextBase<Implementation>
    {
        private readonly ClassifierDictionary _classifiers;
        private readonly Classifier _interfaceOwner;
        private readonly MessageSystem _messageSystem;

        public InterfaceListCommandContext(
            Classifier interfaceOwner,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            _classifiers = classifiers;
            _interfaceOwner = interfaceOwner;
            _messageSystem = messageSystem;

            All = new Query<Implementation>(() => interfaceOwner.InterfaceImplementations);
            New = new NewInterfaceCommand(_interfaceOwner, classifiers, _messageSystem);
            // TODO: check if visibility is also changed if interface was later added to list
            Visibility = new ShowOrHideAllObjectsInListCommand(interfaceOwner.InterfaceImplementations, messageSystem);
        }


        public override ISingleCommandContext GetCommandsForSingleItem(Implementation interfaceObject)
        {
            return new SingleInterfaceCommandContext(
                _interfaceOwner,
                interfaceObject,
                _classifiers,
                _messageSystem);
        }
    }
}
