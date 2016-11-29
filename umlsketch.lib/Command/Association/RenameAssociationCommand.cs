using Common;
using UmlSketch.Event;
using UmlSketch.Validation;

namespace UmlSketch.Command
{
    class RenameAssociationCommand : IRenameCommand
    {
        private readonly INamed _namedObject;
        private readonly MessageSystem _messageSystem;

        public RenameAssociationCommand(
            INamed namedObject, 
            MessageSystem messageSystem)
        {
            _namedObject = namedObject;
            _messageSystem = messageSystem;
        }


        public void Rename(string newName)
        {
            var oldName = _namedObject.Name;
            _namedObject.Name = newName;
            _messageSystem.Publish(_namedObject, new NameChangedEvent(oldName,newName));
        }

        /// <summary>
        /// an association can always be renamed (even when no name is defined)
        /// </summary>
        /// <param name="newName"></param>
        /// <returns></returns>
        public ValidationResult CanRenameWith(string newName) => new Success();
    }
}
