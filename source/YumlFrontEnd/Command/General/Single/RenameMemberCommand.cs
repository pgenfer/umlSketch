using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml.Command
{
    /// <summary>
    /// base implementation for renaming any members that have a name.
    /// The command can be configured by injecting the correct services
    /// or if necessary provide their own implementation 
    /// (e.g. MethodRename needs a different ValidationService signature)
    /// </summary>
    public class RenameMemberCommand : IRenameCommand
    {
        private readonly IValidateNameService _validateNameService;
        private readonly INameChangedNotificationService _notificationService;
        private readonly INamed _namedObject;

        public RenameMemberCommand(
            INamed namedObject,
            IValidateNameService validateNameService,
            INameChangedNotificationService notificationService)
        {
            _namedObject = namedObject;
            Requires(validateNameService != null);
            Requires(notificationService != null);

            _validateNameService = validateNameService;
            _notificationService = notificationService;
        }

        public void Rename(string newName)
        {
            var oldName = _namedObject.Name;
            _namedObject.Name = newName;
            _notificationService.FireNameChange(oldName, newName);
        }

        public ValidationResult CanRenameWith(string newName) =>
            _validateNameService.ValidateNameChange(_namedObject.Name, newName);
    }
}
