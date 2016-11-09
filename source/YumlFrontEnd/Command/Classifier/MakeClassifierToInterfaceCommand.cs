using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Service;

namespace Yuml.Command
{
    public class MakeClassifierToInterfaceCommand
    {
        private readonly Classifier _classifier;
        private readonly IRelationService _relationService;

        public MakeClassifierToInterfaceCommand(
            Classifier classifier,
            IRelationService relationService)
        {
            _classifier = classifier;
            _relationService = relationService;
        }

        public void ToggleInterfaceFlag()
        {
            var isInterface = _classifier.IsInterface;
            // changed from interface => class
            if (isInterface)
                _relationService.ChangeFromInterfaceToClass(_classifier);
            else // changed from class => interface
                _relationService.ChangeFromClassToInterface(_classifier);
        }
    }
}
