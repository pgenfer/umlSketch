using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Command
{
    internal class ChangeTypeOfPropertyCommand : 
        DomainObjectBaseCommand<Property>, IChangeTypeCommand
    {
        private readonly ClassifierDictionary _availableClassifiers;
        
        public ChangeTypeOfPropertyCommand(
            ClassifierDictionary availableClassifiers,
            Property domainObject,
            MessageSystem messageSystem) : base(domainObject,messageSystem)
        {
            _availableClassifiers = availableClassifiers;
        }

        public void ChangeType(string nameOfOldType, string nameOfNewType)
        {
            if (nameOfOldType != nameOfNewType)
            {
                var newType = _availableClassifiers.FindByName(nameOfNewType);
                _domainObject.Type = newType;
                _messageSystem.Publish(
                    _domainObject,
                    new PropertyTypeChangedEvent(nameOfOldType,nameOfNewType));
            }
        }
    }
}
