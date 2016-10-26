using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Command;
using Yuml.Service;

namespace Yuml.Command
{
    public class ClassifierListCommandContext : ListCommandContextBase<Classifier>
    {
        private readonly ClassifierDictionary _classifiers;
        private readonly DeletionService _deletionService;
        private readonly MessageSystem _messageSystem;

        public ClassifierListCommandContext(
            ClassifierDictionary classifiers,
            DeletionService deletionService,
            MessageSystem messageSystem)
        {
            _classifiers = classifiers;
            _deletionService = deletionService;
            _messageSystem = messageSystem;

            Update();
        }

        private void Update()
        {
            // setup commands
            All = new Query<Classifier>(() => _classifiers.NoSystemTypes);
            Visibility = new ShowOrHideAllObjectsInListCommand(_classifiers, _messageSystem);
            New = new NewClassifierCommand(_classifiers, _messageSystem);
        }

        public override ISingleCommandContext GetCommandsForSingleItem(Classifier domainObject) =>
            new ClassifierSingleCommandContext(
                domainObject, 
                _classifiers,
                _deletionService,
                _messageSystem);
    }
}
