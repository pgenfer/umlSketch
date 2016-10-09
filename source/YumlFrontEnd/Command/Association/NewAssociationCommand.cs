using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    public class NewAssociationCommand : NewCommandBase
    {
        public NewAssociationCommand(
            Classifier sourceClassifier,
            ClassifierDictionary availableClassifiers,
            MessageSystem messageSystem)
            :base(sourceClassifier,availableClassifiers,messageSystem)
        {
        }

        public override void CreateNew()
        {
            var relation = _parentClassifier.CreateNewAssociationWithBestInitialValues(_availableClassifiers);
            _messageSystem.PublishCreated(_parentClassifier, relation);
        }
    }
}
