using Yuml.Event;

namespace Yuml.Command.Interface
{
    public class RemoveInterfaceFromClassifierCommand : IDeleteCommand
    {
        private readonly Implementation _existingInterface;
        private readonly MessageSystem _messageSystem;

        public RemoveInterfaceFromClassifierCommand(
            Implementation existingInterface,
            MessageSystem messageSystem)
        {
            _existingInterface = existingInterface;
            _messageSystem = messageSystem;
        }

        public void DeleteItem()
        {
            // TODO: delete from parent
            _messageSystem.PublishDeleted(_existingInterface);
        }
    }
}