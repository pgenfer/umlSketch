using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.DomainObject;

namespace Yuml.Command
{
    public class NewAssociationCommand : INewCommand
    {
        private readonly ClassifierAssociationList _associations;
        private readonly ClassifierDictionary _availableClassifiers;
        private readonly MessageSystem _messageSystem;

        public NewAssociationCommand(
            ClassifierAssociationList associations,
            ClassifierDictionary availableClassifiers,
            MessageSystem messageSystem)
        {
            _associations = associations;
            _availableClassifiers = availableClassifiers;
            _messageSystem = messageSystem;
        }

        public virtual void CreateNew()
        {
            var relation = _associations.CreateNewAssociationWithBestInitialValues(_availableClassifiers);
            _messageSystem.PublishCreated(_associations, relation);
        }
    }
}
