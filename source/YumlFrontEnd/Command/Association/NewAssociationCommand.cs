using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    public class NewAssociationCommand : INewCommand
    {
        private readonly ClassifierDictionary _availableClassifiers;
        private readonly Classifier _sourceClassifier;
        private readonly MessageSystem _messageSystem;

        public NewAssociationCommand(
            ClassifierDictionary availableClassifiers,
            Classifier sourceClassifier,
            MessageSystem messageSystem)
        {
            _availableClassifiers = availableClassifiers;
            _sourceClassifier = sourceClassifier;
            _messageSystem = messageSystem;
        }

        public void CreateNew()
        {
            var relation = _sourceClassifier.CreateNewAssociationWithBestInitialValues(_availableClassifiers);
            _messageSystem.PublishCreated(_sourceClassifier, relation);
        }
    }
}
