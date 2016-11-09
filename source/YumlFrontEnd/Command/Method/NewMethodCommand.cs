using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    public class NewMethodCommand : INewCommand
    {
        private readonly MethodList _methods;
        private readonly ClassifierDictionary _availableClassifiers;
        private readonly MessageSystem _messageSystem;

        public NewMethodCommand(
            MethodList methods, 
            ClassifierDictionary availableClassifiers, 
            MessageSystem messageSystem)
        {
            _methods = methods;
            _availableClassifiers = availableClassifiers;
            _messageSystem = messageSystem;
        }

        public void CreateNew()
        {
            var method = _methods.CreateNewMethodWithBestInitialValues(_availableClassifiers);
            _messageSystem.PublishCreated(_methods, method);
        }
    }
}
