﻿using UmlSketch.DomainObject;
using UmlSketch.Event;
using UmlSketch.Validation;
using static System.Diagnostics.Contracts.Contract;

namespace UmlSketch.Command
{
    public class RenameMethodCommand : DomainObjectBaseCommand<Method> , IRenameCommand
    {
        private readonly IMethodNameValidationService _nameValidation;
        

        public RenameMethodCommand(
            Method domainObject,
            IMethodNameValidationService nameValidation,
            MessageSystem messageSystem) : base(domainObject,messageSystem)
        {
            Requires(nameValidation != null);
            Requires(messageSystem != null);

            _nameValidation = nameValidation;
        }

        public void Rename(string newName)
        {
            var oldName = _domainObject.Name;
            _domainObject.Name = newName;
            _messageSystem.Publish(_domainObject, new NameChangedEvent(oldName, newName));
        }

        public ValidationResult CanRenameWith(string newName) =>
            _nameValidation.ValidateNameChange(_domainObject, newName);
    }
}
