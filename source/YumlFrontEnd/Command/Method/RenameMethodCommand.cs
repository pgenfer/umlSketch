using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml.Command
{
    public class RenameMethodCommand : DomainObjectBaseCommand<Method> , IRenameCommand
    {
        private readonly IMethodNameValidationService _nameValidation;
        private readonly INameChangedNotificationService _notification;

        public RenameMethodCommand(
            Method domainObject,
            IMethodNameValidationService nameValidation,
            INameChangedNotificationService notification) : base(domainObject)
        {
            Requires(nameValidation != null);
            Requires(notification != null);

            _nameValidation = nameValidation;
            _notification = notification;
        }

        public void Rename(string newName)
        {
            var oldName = _domainObject.Name;
            _domainObject.Name = newName;
            _notification.FireNameChange(oldName, newName);
        }

        public ValidationResult CanRenameWith(string newName)
        {
            return _nameValidation.ValidateNameChange(_domainObject, newName);
        }
    }
}
