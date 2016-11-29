using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Command
{
    public class ChangeInterfaceOfClassifierCommand : IChangeTypeCommand
    {
        private readonly Implementation _existingInterface;
        private readonly ClassifierDictionary _classifiers;
        private readonly MessageSystem _messageSystem;

        public ChangeInterfaceOfClassifierCommand(
            Implementation existingInterface, 
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            _existingInterface = existingInterface;
            _classifiers = classifiers;
            _messageSystem = messageSystem;
        }

        public void ChangeType(string nameOfOldType, string nameOfNewInterface)
        {
            var newInterface = _classifiers.FindByName(nameOfNewInterface);
            _existingInterface.ReplaceInterface(newInterface);
            _messageSystem.Publish(
                _existingInterface,
                new ChangeInterfaceOfClassEvent(nameOfOldType, nameOfNewInterface));
        }
    }
}
