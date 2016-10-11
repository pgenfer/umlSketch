using NSubstitute;
using NUnit.Framework;
using Yuml.Command;
using static NSubstitute.Substitute;

namespace Yuml.Test.Command
{
    public class TypeChangedPropertyCommandTest : SimpleTestBase
    {
        private Property _property;
        private ClassifierDictionary _classifiers;
        private MessageSystem _messageSystem;
        private Classifier _oldType, _newType;
        private const string Old = "Old";
        private const string New = "New";

        protected override void Init()
        {
            _property = For<Property>();
            _classifiers = CreateDictionaryWithoutSystemTypes();
            _messageSystem = For<MessageSystem>();
            _oldType = new Classifier(Old);
            _newType = new Classifier(New);
            _classifiers.FindByName(Old).Returns(_oldType);
            _classifiers.FindByName(New).Returns(_newType);
        }

        [TestDescription("Checks that the command changes the type of the property")]
        public void TypeChanged_DomainObject_Edited()
        {
            var changeType = new ChangeTypeOfPropertyCommand(
                _classifiers, 
                _property, 
                _messageSystem);

            changeType.ChangeType(Old, New);

            var newType = _property.Received().Type;

            Assert.AreEqual(_newType,newType);
        }

        [TestDescription("Ensures that notification is fired")]
        public void TypeChanged_NotificationFired()
        {
            var changeType = new ChangeTypeOfPropertyCommand(
                _classifiers,
                _property,
                _messageSystem);

            changeType.ChangeType(Old, New);

            _messageSystem.Received().Publish(
                _property,
                Arg.Is<PropertyTypeChangedEvent>(
                    x => x.NameOfOldType == Old && x.NameOfNewType == New));
        }
    }
}
