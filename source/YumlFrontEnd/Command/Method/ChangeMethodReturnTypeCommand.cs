using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    public class ChangeMethodReturnTypeCommand : DomainObjectBaseCommand<Method>, IChangeTypeCommand
    {
        private readonly ClassifierDictionary _availableClassifiers;

        public ChangeMethodReturnTypeCommand(
             ClassifierDictionary availableClassifiers,
            Method domainObject, 
            MessageSystem messageSystem) : 
            base(domainObject, messageSystem)
        {
            _availableClassifiers = availableClassifiers;
        }

        public void ChangeType(string nameOfOldType, string nameOfNewType)
        {
            if (nameOfOldType != nameOfNewType)
            {
                var newType = _availableClassifiers.FindByName(nameOfNewType);
                _domainObject.ReturnType = newType;
                _messageSystem.Publish(
                    _domainObject,
                    new MethodReturnTypeChanged(nameOfOldType, nameOfNewType));
            }
        }
    }
}
