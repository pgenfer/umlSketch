using Yuml.Event;

namespace Yuml.Command.Interface
{
    public class RemoveInterfaceFromClassifierCommand : IDeleteCommand
    {
        private readonly Classifier _interfaceOwner;
        private readonly Classifier _existingInterface;
        private readonly MessageSystem _messageSystem;

        public RemoveInterfaceFromClassifierCommand(
            Classifier interfaceOwner,
            Classifier existingInterface,
            MessageSystem messageSystem)
        {
            _interfaceOwner = interfaceOwner;
            _existingInterface = existingInterface;
            _messageSystem = messageSystem;
        }

        public void DeleteItem()
        {
            _interfaceOwner.Interfaces.RemoveInterfaceFromList(_existingInterface);
            _messageSystem.Publish(_interfaceOwner,new RemoveInterfaceFromClassEvent(_existingInterface.Name));
        }
    }
}