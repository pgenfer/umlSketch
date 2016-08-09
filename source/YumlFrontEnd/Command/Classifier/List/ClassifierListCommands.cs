using System.Collections.Generic;

namespace Yuml.Commands
{
    public class ClassifierListCommands : IClassifierListCommands
    {
        private readonly ClassifierDictionary _classifiers;
        private readonly ClassifierNotificationService _notificationService;
        private readonly QueryClassifiersCommand _queryAllClassifiersCommand;

        public ClassifierListCommands(
            ClassifierDictionary classifiers,
            ClassifierNotificationService notificationService)
        {
            _classifiers = classifiers;
            _notificationService = notificationService;
            _queryAllClassifiersCommand = new QueryClassifiersCommand(() => classifiers);
        }

        public IEnumerable<Classifier> QueryAllClassifiers => _queryAllClassifiersCommand.Do();
        public IClassiferCommands GetCommandsForClassifier(Classifier classifier) => 
            new ClassifierCommands(_classifiers,classifier, _notificationService);
      
    }
}