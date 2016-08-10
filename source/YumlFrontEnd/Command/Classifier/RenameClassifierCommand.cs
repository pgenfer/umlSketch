﻿using static System.Diagnostics.Contracts.Contract;

namespace Yuml.Command
{
   

    /// <summary>
    /// command called when a classifier is renamed
    /// </summary>
    public class RenameClassifierCommand : DomainObjectBaseCommand<Classifier>, IRenameCommand
    {
        private readonly ClassifierNotificationService _notificationService;
        private readonly ClassifierDictionary _classifierDictionary;
        private readonly IValidateNameService _validateNameService;

        public RenameClassifierCommand(
            Classifier classifier,
            ClassifierDictionary classifierDictionary,
            IValidateNameService validateNameService,
            ClassifierNotificationService notificationService) : base(classifier)
        {
            Requires(notificationService != null);
            Requires(classifier != null);
            Requires(classifierDictionary != null);
            Requires(validateNameService != null);

            _notificationService = notificationService;
            _classifierDictionary = classifierDictionary;
            _validateNameService = validateNameService;
        }

        public void Rename(string newName)
        {
            var oldName = _domainObject.Name;
            // renaming should only be executed if the name does really change
            if (oldName == newName)
                return;

            _classifierDictionary.RenameClassifier(_domainObject, newName);
            _notificationService.FireNameChange(oldName, newName);
        }

        public ValidationResult CanRenameWith(string newName) => 
            _validateNameService.ValidateNameChange(_domainObject.Name, newName);
    }
}