using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Notification;

namespace Yuml.Command
{
    internal class ChangeTypeOfPropertyCommand : 
        DomainObjectBaseCommand<Property>, IChangeTypeCommand
    {
        private readonly ClassifierDictionary _availableClassifiers;
        private readonly PropertyNotificationService _notification;

        public ChangeTypeOfPropertyCommand(
            ClassifierDictionary availableClassifiers,
            Property domainObject,
            PropertyNotificationService notification) : base(domainObject)
        {
            _availableClassifiers = availableClassifiers;
            _notification = notification;
        }

        public void ChangeType(string nameOfOldType, string nameOfNewType)
        {
            if (nameOfOldType != nameOfNewType)
            {
                var newType = _availableClassifiers.FindByName(nameOfNewType);
                _domainObject.Type = newType;
                _notification.FireTypeChanged(nameOfOldType, nameOfNewType);
            }
        }
    }
}
