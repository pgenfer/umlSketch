using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    public abstract class NewCommandBase : INewCommand
    {
        protected readonly Classifier _parentClassifier;
        protected readonly ClassifierDictionary _availableClassifiers;
        protected readonly MessageSystem _messageSystem;

        protected NewCommandBase(
            Classifier parentClassifier,
            ClassifierDictionary availableClassifiers,
            MessageSystem messageSystem)
        {
            _parentClassifier = parentClassifier;
            _availableClassifiers = availableClassifiers;
            _messageSystem = messageSystem;
        }

        public abstract void CreateNew();
    }
}
