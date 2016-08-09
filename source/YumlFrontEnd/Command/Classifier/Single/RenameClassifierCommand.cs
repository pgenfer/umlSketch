using static System.Diagnostics.Contracts.Contract;

namespace Yuml.Commands
{
   

    /// <summary>
    /// command called when a classifier is renamed
    /// </summary>
    public class RenameClassifierCommand : SingleClassifierCommandBase, IRenameCommand
    {
        private readonly ClassifierNotificationService _notificationService;
        private readonly ClassifierDictionary _classifierDictionary;

        public RenameClassifierCommand(
            Classifier classifier,
            ClassifierDictionary classifierDictionary,
            ClassifierNotificationService notificationService) : base(classifier)
        {
            Requires(notificationService != null);
            Requires(classifier != null);
            Requires(classifierDictionary != null);

            _notificationService = notificationService;
            _classifierDictionary = classifierDictionary;
        }

        public void Do(string newName)
        {
            var oldName = _classifier.Name;
            // renaming should only be executed if the name does really change
            if (oldName == newName)
                return;

            _classifierDictionary.RenameClassifier(_classifier, newName);
            _notificationService.FireNameChange(oldName, newName);
        }
    }
}