using Yuml.Event;

namespace Yuml.Command.Interface
{
    public class RemoveInterfaceFromClassifierCommand : IDeleteCommand
    {
        private readonly Classifier _interfaceOwner;
        private readonly Implementation _existingInterface;
        private readonly MessageSystem _messageSystem;

        public RemoveInterfaceFromClassifierCommand(
            Classifier interfaceOwner,
            Implementation existingInterface,
            MessageSystem messageSystem)
        {
            _interfaceOwner = interfaceOwner;
            _existingInterface = existingInterface;
            _messageSystem = messageSystem;
        }

        public void DeleteItem()
        {
            _interfaceOwner.InterfaceImplementations.RemoveInterfaceFromList(_existingInterface);
            _messageSystem.Publish(_interfaceOwner,new DomainObjectDeletedEvent<Implementation>(_existingInterface));
        }
    }
}