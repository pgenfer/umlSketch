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
        private readonly NotificationServices _notificationService;
        private readonly DeletionService _deletionService;

        public ClassifierListCommandContext(
            ClassifierDictionary classifiers,
            NotificationServices notificationServices,
            DeletionService deletionService)
        {
            _classifiers = classifiers;
            _notificationService = notificationServices;
            _deletionService = deletionService;

            // setup commands
            All = new Query<Classifier>(() => classifiers);
        }

        public override ISingleCommandContext GetCommandsForSingleItem(Classifier domainObject) =>
            new ClassifierSingleCommandContext(
                domainObject, 
                _classifiers,
                _notificationService,
                _deletionService);
    }
}
