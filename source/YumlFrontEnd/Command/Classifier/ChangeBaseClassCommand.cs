using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Notification;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml.Command
{
    public class ChangeBaseClassCommand : DomainObjectBaseCommand<Classifier>, IChangeTypeToNullCommand
    {
        private readonly ClassifierDictionary _classifiers;
        private readonly RelationNotificationService _notification;

        public ChangeBaseClassCommand(
            Classifier domainObject,
            ClassifierDictionary classifiers,
            RelationNotificationService notification) : base(domainObject)
        {
            _classifiers = classifiers;
            _notification = notification;
        }

        public void ChangeType(string nameOfOldType, string nameOfNewType)
        {
            Requires(nameOfOldType != nameOfNewType);

            // change the domain model
            var newBaseType = _classifiers.FindByName(nameOfNewType);
            _domainObject.BaseClass = newBaseType;
            // fire update event
            _notification.FireBaseClassChanged(
                _domainObject.Name,
                nameOfOldType,
                nameOfNewType);

        }

        public void ClearType(string nameOfOldType)
        {
            Requires(!string.IsNullOrEmpty(nameOfOldType));

            _domainObject.BaseClass = null;
            _notification.FireBaseClassRemoved(_domainObject.Name);
        }

        public void SetNewType(string nameOfNewType)
        {
            Requires(!string.IsNullOrEmpty(nameOfNewType));

            var newBaseClass = _classifiers.FindByName(nameOfNewType);
            _domainObject.BaseClass = newBaseClass;
            _notification.FireBaseClassAdded(_domainObject.Name, nameOfNewType);
        }
    }
}
