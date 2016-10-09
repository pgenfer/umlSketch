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
        private readonly MessageSystem _messageSystem;

        public MethodListCommandContext(
            Classifier classifier,
            IMethodNameValidationService nameValidation,
            MethodNotificationService notificationService,
            MessageSystem messageSystem)
        {
            _nameValidation = nameValidation;
            _notificationService = notificationService;
            _messageSystem = messageSystem;
            All = new Query<Method>(() => classifier.Methods);
            Visibility = new ShowOrHideSingleObjectCommand(classifier.Methods, messageSystem);
        }

        public override ISingleCommandContext GetCommandsForSingleItem(Method domainObject) =>
            new MethodSingleCommandContext(domainObject, _nameValidation, _notificationService,_messageSystem);
       
    }
}
