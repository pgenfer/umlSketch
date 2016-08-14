using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Command;

namespace Yuml.Command
{
    public class ClassifierListCommandContext : ListCommandContextBase<Classifier>
    {
        private readonly ClassifierDictionary _classifiers;
        private readonly NotificationServices _notificationService;

        public ClassifierListCommandContext(
            ClassifierDictionary classifiers,
            NotificationServices notificationServices)
        {
            _classifiers = classifiers;
            _notificationService = notificationServices;

            // setup commands
            All = new Query<Classifier>(() => classifiers);
        }

        public override ISingleCommandContext GetCommandsForSingleItem(Classifier domainObject) =>
            new ClassifierSingleCommandContext(
                domainObject, 
                _classifiers,
                _notificationService);
    }
}
