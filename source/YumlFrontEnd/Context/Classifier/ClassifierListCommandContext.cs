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
        private readonly ValidationServices _validationServices;
        private readonly ClassifierNotificationService _notificationService;

        public ClassifierListCommandContext(
            ClassifierDictionary classifiers,
            ValidationServices validationServices,
            ClassifierNotificationService notificationService)
        {
            _classifiers = classifiers;
            _validationServices = validationServices;
            _notificationService = notificationService;

            // setup commands
            All = new Query<Classifier>(() => classifiers);
        }

        public override ISingleCommandContext GetCommandsForSingleItem(Classifier domainObject) =>
            new ClassifierSingleCommandContext(
                domainObject, 
                _classifiers,
                _validationServices,
                _notificationService);
    }
}
