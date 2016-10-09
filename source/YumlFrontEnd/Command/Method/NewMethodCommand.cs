using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    public class NewMethodCommand : NewCommandBase
    {
        public NewMethodCommand(
            Classifier parentClassifier, 
            ClassifierDictionary availableClassifiers, 
            MessageSystem messageSystem) : 
            base(parentClassifier, availableClassifiers, messageSystem)
        {
        }

        public override void CreateNew()
        {
            var method = _parentClassifier.CreateNewMethodWithBestInitialValues(_availableClassifiers);
            _messageSystem.PublishCreated(_parentClassifier, method);
        }
    }
}
