using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    public class ChangeParameterTypeCommand : DomainObjectBaseCommand<Parameter>, IChangeTypeCommand
    {
        private readonly ClassifierDictionary _classifiers;
       
        public ChangeParameterTypeCommand(
            ClassifierDictionary classifiers,
            Parameter domainObject, 
            MessageSystem messageSystem) : 
            base(domainObject, messageSystem)
        {
            _classifiers = classifiers;
        }

        public void ChangeType(string nameOfOldType, string nameOfNewType)
        {
            var newType = _classifiers.FindByName(nameOfNewType);
            _domainObject.Type = newType;
            _messageSystem.Publish(_domainObject,new ChangeParameterTypeEvent(nameOfOldType, nameOfNewType));
        }
    }
}
