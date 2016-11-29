using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Command
{
    public class ChangeBaseClassCommand : DomainObjectBaseCommand<Classifier>, IChangeTypeToNullCommand
    {
        private readonly ClassifierDictionary _classifiers;

        public ChangeBaseClassCommand(
            Classifier domainObject,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem) : base(domainObject,messageSystem)
        {
            _classifiers = classifiers;
        }

        public void ChangeType(string nameOfOldType, string nameOfNewType)
        {
            // change the domain model
            var newBaseType = _classifiers.FindByName(nameOfNewType);
            _domainObject.BaseClass = newBaseType;
            // fire update event
            _messageSystem.Publish(
                _domainObject,
                new BaseClassChangedEvent(
                    nameOfOldType,nameOfNewType));
        }

        public void ClearType(string nameOfOldType)
        {
            _domainObject.BaseClass = null;
            _messageSystem.Publish(_domainObject,new ClearBaseClassEvent());
        }

        public void SetNewType(string nameOfNewType)
        {
            var newBaseClass = _classifiers.FindByName(nameOfNewType);
            _domainObject.BaseClass = newBaseClass;
            _messageSystem.Publish(_domainObject, new BaseClassSetEvent(nameOfNewType));
        }
    }
}
