using Yuml.Event;

namespace Yuml.Command.Interface
{
    public class RemoveInterfaceFromClassifierCommand : IDeleteCommand
    {
        private readonly ImplementationList _interfaceImplementationList;
        private readonly Implementation _existingInterface;
        private readonly MessageSystem _messageSystem;

        public RemoveInterfaceFromClassifierCommand(
            ImplementationList interfaceImplementationList,
            Implementation existingInterface,
            MessageSystem messageSystem)
        {
            _existingInterface = existingInterface;
            _messageSystem = messageSystem;
            _interfaceImplementationList = interfaceImplementationList;
        }

        public void DeleteItem()
        {
            _interfaceImplementationList.RemoveInterfaceFromList(_existingInterface);
            _messageSystem.Publish(
                _existingInterface, 
                new DomainObjectDeletedEvent<Implementation>(_existingInterface));
        }
    }
}