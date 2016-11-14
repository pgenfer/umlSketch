using Yuml.Event;

namespace Yuml.Command.Interface
{
    public class RemoveInterfaceFromClassifierCommand : IDeleteCommand
    {
        private readonly ImplementationList _implementations;
        private readonly Implementation _existingInterface;
        private readonly MessageSystem _messageSystem;

        public RemoveInterfaceFromClassifierCommand(
            ImplementationList implementations,
            Implementation existingInterface,
            MessageSystem messageSystem)
        {
            _implementations = implementations;
            _existingInterface = existingInterface;
            _messageSystem = messageSystem;
        }

        public void DeleteItem() =>
           _implementations.RemoveImplementationForInterface(_existingInterface,_messageSystem);
    }
}