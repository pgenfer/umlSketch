using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Yuml.Command;
using Yuml.Notification;
using static NSubstitute.Substitute;

namespace Yuml.Test.Command
{
    public class TypeChangedPropertyCommandTest : SimpleTestBase
    {
        private Property _property;
        private ClassifierDictionary _classifiers;
        private PropertyNotificationService _notification;
        private Classifier _oldType, _newType;
        private const string Old = "Old";
        private const string New = "New";

        protected override void Init()
        {
            _property = For<Property>();
            _classifiers = For<ClassifierDictionary>();
            _notification = For<PropertyNotificationService>();
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
                _notification);

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
                _notification);

            changeType.ChangeType(Old, New);

            _notification.Received().FireTypeChanged(Old,New);
        }
    }
}
