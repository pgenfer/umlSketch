using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Notification;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml.Command
{
    public class RenamePropertyCommand : DomainObjectBaseCommand<Property>, IRenameCommand
    {
        private readonly IValidateNameService _validateNameService;
        private readonly PropertyNotificationService _notificationService;

        public RenamePropertyCommand(
            Property domainObject,
            IValidateNameService validateNameService,
            PropertyNotificationService notificationService) : base(domainObject)
        {
            Requires(validateNameService != null);
            Requires(notificationService != null);

            _validateNameService = validateNameService;
            _notificationService = notificationService;
        }

        public void Rename(string newName)
        {
            var oldName = _domainObject.Name;
            _domainObject.Name = newName;
            _notificationService.FireNameChange(oldName, newName);
        }

        public ValidationResult CanRenameWith(string newName) =>
            _validateNameService.ValidateNameChange(_domainObject.Name, newName);
    }
}
