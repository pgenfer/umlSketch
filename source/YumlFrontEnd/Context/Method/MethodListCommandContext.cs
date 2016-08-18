using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Notification;

namespace Yuml.Command
{
    internal class MethodListCommandContext : ListCommandContextBase<Method>
    {
        private readonly IMethodNameValidationService _nameValidation;
        private readonly MethodNotificationService _notificationService;

        public MethodListCommandContext(
            Classifier classifier,
            IMethodNameValidationService nameValidation,
            MethodNotificationService notificationService)
        {
            _nameValidation = nameValidation;
            _notificationService = notificationService;
            All = new Query<Method>(() => classifier.Methods);
        }

        public override ISingleCommandContext GetCommandsForSingleItem(Method domainObject) =>
            new MethodSingleCommandContext(domainObject, _nameValidation, _notificationService);
       
    }
}
