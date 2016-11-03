using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    internal class InterfaceListCommandContext : ListCommandContextBase<Classifier>
    {
        private readonly ClassifierDictionary _classifiers;
        private readonly Classifier _interfaceOwner;
        private readonly MessageSystem _messageSystem;

        public InterfaceListCommandContext(
            ClassifierDictionary classifiers,
            Classifier interfaceOwner,
            MessageSystem messageSystem)
        {
            _classifiers = classifiers;
            _interfaceOwner = interfaceOwner;
            _messageSystem = messageSystem;

            All = new Query<Classifier>(() => interfaceOwner.Interfaces);
            New = new NewInterfaceCommand(_interfaceOwner, classifiers, _messageSystem);
            // TODO: check if visibility is also changed if interface was later added to list
            Visibility = new ShowOrHideAllObjectsInListCommand(interfaceOwner.Interfaces, messageSystem);
        }


        public override ISingleCommandContext GetCommandsForSingleItem(Classifier interfaceObject)
        {
            return new SingleInterfaceCommandContext(
                _interfaceOwner,
                interfaceObject,
                _classifiers,
                _messageSystem);
        }
    }
}
