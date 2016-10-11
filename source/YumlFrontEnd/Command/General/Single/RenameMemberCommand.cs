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
        private readonly INamed _namedObject;
        private readonly MessageSystem _messageSystem;

        public RenameMemberCommand(
            INamed namedObject,
            IValidateNameService validateNameService,
            MessageSystem messageSystem)
        {
            _namedObject = namedObject;
            Requires(validateNameService != null);
            Requires(messageSystem != null);

            _validateNameService = validateNameService;
            _messageSystem = messageSystem;
        }

        public void Rename(string newName)
        {
            var oldName = _namedObject.Name;
            _namedObject.Name = newName;
            _messageSystem.Publish(_namedObject,new NameChangedEvent(oldName,newName));
        }

        public ValidationResult CanRenameWith(string newName) =>
            _validateNameService.ValidateNameChange(_namedObject.Name, newName);
    }
}
